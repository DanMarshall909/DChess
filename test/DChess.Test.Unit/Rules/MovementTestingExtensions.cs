using DChess.Core.Errors;
using DChess.Core.Game;

namespace DChess.Test.Unit.Rules;

public static class MovementTestingExtensions
{
    public const int LegalPositionValue = 1;

    /// <summary>
    ///     Test that A piece can move to a given set of offsets from its current position. Offsets resulting in invalid
    ///     squares are ignored.
    /// </summary>
    /// <param name="pieceAttributes">The pieceAttributes that will be evaluated e.g. WhiteKnight</param>
    /// <param name="offsetsFromCurrentPosition">
    ///     An array of offsets that the pieceAttributes should be able to move to from its current
    ///     position. For example A knight would have offsets in an L shape i.e. (1, 2), (1, -2), (-1, 2), (-1, -2), (2, 1),
    ///     (2, -1), (-2, 1), (-2, -1)
    /// </param>
    /// <param name="testErrorHandler"></param>
    /// <param name="setupBoard">
    ///     An optional action that can be used to setup the board before each test. The action takes in
    ///     the board and the square of the pieceAttributes being tested
    /// </param>
    public static void ShouldBeAbleToMoveTo(this PieceAttributes pieceAttributes,
        IReadOnlyCollection<MoveOffset> offsetsFromCurrentPosition, IErrorHandler testErrorHandler,
        Action<Game, Square>? setupBoard = null)
        => AbleToMoveWhenOffsetBy(pieceAttributes, offsetsFromCurrentPosition, true, testErrorHandler, setupBoard);

    /// <summary>
    ///     Tests that every offset in invalidOffsetsFromCurrentPosition array results in an invalid move.  Offsets resulting
    ///     in invalid squares are ignored.
    /// </summary>
    /// <param name="pieceAttributes"></param>
    /// <param name="invalidOffsetsFromCurrentPosition"></param>
    /// <param name="errorHandler"></param>
    /// <param name="setupBoard"></param>
    public static void ShouldNotBeAbleToMoveTo(this PieceAttributes pieceAttributes,
        IReadOnlyCollection<MoveOffset> invalidOffsetsFromCurrentPosition, IErrorHandler errorHandler,
        Action<Game, Square>? setupBoard = null)
        => AbleToMoveWhenOffsetBy(pieceAttributes, invalidOffsetsFromCurrentPosition, false, errorHandler, setupBoard);

    public static void ShouldOnlyBeAbleToMoveTo(this PieceAttributes pieceAttributes,
        IReadOnlyCollection<MoveOffset> validOffsetsFromCurrentPosition, IErrorHandler errorHandler,
        Action<Game, Square>? setupBoard = null)
    {
        pieceAttributes.ShouldBeAbleToMoveTo(validOffsetsFromCurrentPosition, errorHandler, setupBoard);
        var invalidOffsetsFromCurrentPosition = validOffsetsFromCurrentPosition.Inverse().ToList().AsReadOnly();
        pieceAttributes.ShouldNotBeAbleToMoveTo(invalidOffsetsFromCurrentPosition, errorHandler, setupBoard);
    }

    /// <summary>
    ///     Tests if A piece can move to a given set of offsets from its current position. Offsets resulting in invalid
    ///     squares are ignored.
    /// </summary>
    /// <param name="pieceAttributes"></param>
    /// <param name="offsetsFromCurrentPosition"></param>
    /// <param name="shouldBeAbleToMove"></param>
    /// <param name="errorHandler"></param>
    /// <param name="setupBoard"></param>
    private static void AbleToMoveWhenOffsetBy(this PieceAttributes pieceAttributes,
        IReadOnlyCollection<MoveOffset> offsetsFromCurrentPosition, bool shouldBeAbleToMove, IErrorHandler errorHandler,
        Action<Game, Square>? setupBoard = null)
    {
        var game = new Game(new Board(), errorHandler, 3);
        for (byte rank = 1; rank < 8; rank++)
        for (var file = 'a'; file < 'h'; file++)
        {
            var from = new Square(file, rank);

            game.Board.Clear();
            game.Board.Place(pieceAttributes, from);
            setupBoard?.Invoke(game, from);

            var pieceAtFrom = game.Pieces[from];
            foreach (var offset in offsetsFromCurrentPosition)
                if (from.TryApplyOffset(offset, out var to))
                {
                    var moveResult = pieceAtFrom.CheckMove(to, game);
                    moveResult
                        .IsValid
                        .Should().Be(shouldBeAbleToMove,
                            $"{pieceAtFrom.PieceAttributes} should {(shouldBeAbleToMove ? "" : "not ")}be able to move from {from} to {to}). {moveResult.Validity.Message()}");
                }
        }
    }

    public static void SetOffsetPositions(this Game game, Square from, IEnumerable<MoveOffset> offsets,
        PieceAttributes pieceAttributes)
    {
        foreach (var offset in offsets)
            if (from.TryApplyOffset(offset, out var to))
                game.Board.Place(pieceAttributes, to);
    }

    /// <summary>
    ///     Sets the positions of the board to be 2 cells away from the given square in all directions.
    /// </summary>
    /// <param name="game"></param>
    /// <param name="square">The square to set the positions around</param>
    /// <param name="pieceAttributes"></param>
    public static void Surround2CellsFrom(this Game game, Square square, PieceAttributes pieceAttributes)
    {
        SetOffsetPositions(game, square, new MoveOffset[]
        {
            (-2, -2), (-1, -2), (0, -2), (1, -2), (2, -2),
            (-2, -1), (2, -1),
            (-2, 0), (2, 0),
            (-2, 1), (2, 1),
            (-2, 2), (-1, 2), (0, 2), (1, 2), (2, 2)
        }, pieceAttributes);
    }

    public static void Surround(this Game game, Square square, PieceAttributes pieceAttributes)
    {
        SetOffsetPositions(game, square, new MoveOffset[]
        {
            (-1, 1),
            (1, 1),
            (1, 1),
            (-1, 0),
            (1, 0),
            (-1, -1),
            (1, -1),
            (1, -1)
        }, pieceAttributes);
    }


    /// <summary>
    ///     Returns an "inverse" of the given offsets. The inverse of a set of offsets is the set of all offsets that are not
    ///     in the given set bounded by -8 to 8. Note that not all of these offsets are valid squares on a chess board, so
    ///     they should be checked before use.
    /// </summary>
    /// <param name="offsets">a collection of offsets</param>
    /// <returns></returns>
    public static IEnumerable<MoveOffset> Inverse(this IReadOnlyCollection<MoveOffset> offsets)
    {
        for (int df = -7; df <= 7; df++)
        for (int dr = -7; dr <= 7; dr++)
        {
            if (offsets.Contains(new MoveOffset(dr, df)))
                continue;

            yield return new MoveOffset(df, dr);
        }
    }

    /// <summary>
    ///     Converts a 15x15 matrix of bytes to an array of MoveOffsets. The matrix should be centered at the center of the
    ///     matrix i.e. (8, 8).
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