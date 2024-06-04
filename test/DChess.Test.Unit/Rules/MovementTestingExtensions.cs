﻿using DChess.Core.Board;

namespace DChess.Test.Unit.Rules;

public static class MovementTestingExtensions
{
    public const int LegalPositionValue = 1;

    /// <summary>
    ///     Test that a pieceProperties can move to a given set of offsets from its current position. Offsets resulting in invalid
    ///     coordinates are ignored.
    /// </summary>
    /// <param name="pieceProperties">The pieceProperties that will be evaluated e.g. WhiteKnight</param>
    /// <param name="offsetsFromCurrentPosition">
    ///     An array of offsets that the pieceProperties should be able to move to from its current
    ///     position. For example A knight would have offsets in an L shape i.e. (1, 2), (1, -2), (-1, 2), (-1, -2), (2, 1),
    ///     (2, -1), (-2, 1), (-2, -1)
    /// </param>
    /// <param name="setupBoard">
    ///     An optional action that can be used to setup the board before each test. The action takes in
    ///     the board and the coordinate of the pieceProperties being tested
    /// </param>
    public static void ShouldBeAbleToMoveTo(this PieceProperties pieceProperties,
        IReadOnlyCollection<MoveOffset> offsetsFromCurrentPosition, Action<Board, Coordinate>? setupBoard = null)
        => AbleToMoveWhenOffsetBy(pieceProperties, offsetsFromCurrentPosition, true, setupBoard);

    /// <summary>
    ///     Tests that every offset in invalidOffsetsFromCurrentPosition array results in an invalid move.  Offsets resulting
    ///     in invalid coordinates are ignored.
    /// </summary>
    /// <param name="pieceProperties"></param>
    /// <param name="invalidOffsetsFromCurrentPosition"></param>
    /// <param name="setupBoard"></param>
    public static void ShouldNotBeAbleToMoveTo(this PieceProperties pieceProperties,
        IReadOnlyCollection<MoveOffset> invalidOffsetsFromCurrentPosition, Action<Board, Coordinate>? setupBoard = null)
        => AbleToMoveWhenOffsetBy(pieceProperties, invalidOffsetsFromCurrentPosition, false, setupBoard);

    public static void ShouldOnlyBeAbleToMoveTo(this PieceProperties pieceProperties,
        IReadOnlyCollection<MoveOffset> validOffsetsFromCurrentPosition, Action<Board, Coordinate>? setupBoard = null)
    {
        pieceProperties.ShouldBeAbleToMoveTo(validOffsetsFromCurrentPosition, setupBoard);
        var invalidOffsetsFromCurrentPosition = validOffsetsFromCurrentPosition.Inverse().ToList().AsReadOnly();
        pieceProperties.ShouldNotBeAbleToMoveTo(invalidOffsetsFromCurrentPosition, setupBoard);
    }

    /// <summary>
    ///     Tests if a pieceProperties can move to a given set of offsets from its current position. Offsets resulting in invalid
    ///     coordinates are ignored.
    /// </summary>
    /// <param name="pieceProperties"></param>
    /// <param name="offsetsFromCurrentPosition"></param>
    /// <param name="shouldBeAbleToMove"></param>
    /// <param name="setupBoard"></param>
    private static void AbleToMoveWhenOffsetBy(this PieceProperties pieceProperties,
        IReadOnlyCollection<MoveOffset> offsetsFromCurrentPosition, bool shouldBeAbleToMove,
        Action<Board, Coordinate>? setupBoard = null)
    {
        var board = new Board(new TestInvalidMoveHandler());
        for (byte rank = 1; rank < 8; rank++)
        for (var file = 'a'; file < 'h'; file++)
        {
            var from = new Coordinate(file, rank);

            board.Clear();
            board.PieceAt[from] = pieceProperties;
            setupBoard?.Invoke(board, from);

            var pieceAtFrom = board.Pieces[from];
            foreach (var offset in offsetsFromCurrentPosition)
                if (from.TryApplyOffset(offset, out var to))
                    pieceAtFrom.CheckMove(to!.Value).IsValid
                        .Should().Be(shouldBeAbleToMove,
                            $"{pieceAtFrom.PieceProperties} should {(shouldBeAbleToMove ? "" : "not ")}be able to move from {from} to {to})");
        }
    }

    public static void SetOffsetPositions(this Board board, Coordinate from, IReadOnlyCollection<MoveOffset> offsets,
        PieceProperties pieceProperties)
    {
        foreach (var offset in offsets)
            if (from.TryApplyOffset(offset, out var to))
                board.PieceAt[to!.Value] = pieceProperties;
    }

    /// <summary>
    ///     Sets the positions of the board to be 2 cells away from the given coordinate in all directions.
    /// </summary>
    /// <param name="board"></param>
    /// <param name="coordinate">The coordinate to set the positions around</param>
    /// <param name="pieceProperties"></param>
    public static void Surround2CellsFrom(this Board board, Coordinate coordinate, PieceProperties pieceProperties)
    {
        SetOffsetPositions(board, coordinate, new MoveOffset[]
        {
            (-2, -2), (-1, -2), (0, -2), (1, -2), (2, -2),
            (-2, -1), (2, -1),
            (-2, 0), (2, 0),
            (-2, 1), (2, 1),
            (-2, 2), (-1, 2), (0, 2), (1, 2), (2, 2)
        }, pieceProperties);
    }

    public static void Surround(this Board board, Coordinate coordinate, PieceProperties pieceProperties)
    {
        SetOffsetPositions(board, coordinate, new MoveOffset[]
        {
            (-1, 1),
            (1, 1),
            (1, 1),
            (-1, 0),
            (1, 0),
            (-1, -1),
            (1, -1),
            (1, -1)
        }, pieceProperties);
    }


    /// <summary>
    ///     Returns an "inverse" of the given offsets. The inverse of a set of offsets is the set of all offsets that are not
    ///     in the given set bounded by -8 to 8. Note that not all of these offsets are valid coordinates on a chess board, so
    ///     they should be checked before use.
    /// </summary>
    /// <param name="offsets">a collection of offsets</param>
    /// <returns></returns>
    public static IEnumerable<MoveOffset> Inverse(this IReadOnlyCollection<MoveOffset> offsets)
    {
        for (int df = -8; df <= 8; df++)
        for (int dr = -8; dr <= 8; dr++)
        {
            if (offsets.Contains(new MoveOffset(dr, df)))
                continue;

            yield return new MoveOffset(df, dr);
        }
    }

    /// <summary>
    ///     Converts a 17x17 matrix of bytes to an array of MoveOffsets. The matrix should be centered at the center of the
    ///     matrix i.e. (8, 8). The matrix should be 17x17.
    /// </summary>
    /// <param name="movesFromCenter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static MoveOffset[] ToMoveOffsets(this byte[,] movesFromCenter)
    {
        var offsets = new List<MoveOffset>();
        int files = movesFromCenter.GetLength(0);
        int ranks = movesFromCenter.GetLength(1);
        if (files != 15 || ranks != 15) throw new ArgumentException("Matrix must be 15x15");

        for (var i = 0; i < files; i++)
        for (var j = 0; j < ranks; j++)
            if (movesFromCenter[i, j] == LegalPositionValue)
                offsets.Add((i - 7, j - 7));

        return offsets.ToArray();
    }
}