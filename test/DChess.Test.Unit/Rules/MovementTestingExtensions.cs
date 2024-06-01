using DChess.Core.Board;
using DChess.Core.Moves;

namespace DChess.Test.Unit.Rules.Pieces;

public static class MovementTestingExtensions
{
    /// <summary>
    /// Test that a piece can move to a given set of offsets from its current position. Offsets resulting in invalid coordinates are ignored.
    /// </summary>
    /// <param name="piece">The piece that will be evaluated e.g. WhiteKnight</param>
    /// <param name="offsetsFromCurrentPosition">An array of offsets that the piece should be able to move to from its current position. For example A knight would have offsets in an L shape i.e. (1, 2), (1, -2), (-1, 2), (-1, -2), (2, 1), (2, -1), (-2, 1), (-2, -1)</param>
    /// <param name="setupBoard">An optional action that can be used to setup the board before each test. The action takes in the board and the coordinate of the piece being tested</param>
    public static void ShouldBeAbleToMoveTo(this ChessPiece piece,
        IReadOnlyCollection<MoveOffset> offsetsFromCurrentPosition, Action<Board, Coordinate>? setupBoard = null)
        => AbleToMoveWhenOffsetBy(piece, offsetsFromCurrentPosition, true, setupBoard);

    /// <summary>
    /// Tests that every offset in  invalidOffsetsFromCurrentPosition array results in an invalid move.  Offsets resulting in invalid coordinates are ignored.
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="invalidOffsetsFromCurrentPosition"></param>
    /// <param name="setupBoard"></param>
    public static void ShouldNotBeAbleToMoveTo(this ChessPiece piece,
        IReadOnlyCollection<MoveOffset> invalidOffsetsFromCurrentPosition, Action<Board, Coordinate>? setupBoard = null)
        => AbleToMoveWhenOffsetBy(piece, invalidOffsetsFromCurrentPosition, false, setupBoard);

    public static void ShouldOnlyBeAbleToMoveTo(this ChessPiece piece,
        IReadOnlyCollection<MoveOffset> validOffsetsFromCurrentPosition, Action<Board, Coordinate>? setupBoard = null)
    {
        piece.ShouldBeAbleToMoveTo(validOffsetsFromCurrentPosition, setupBoard);
        var invalidOffsetsFromCurrentPosition = validOffsetsFromCurrentPosition.Inverse().ToList().AsReadOnly();
        piece.ShouldNotBeAbleToMoveTo(invalidOffsetsFromCurrentPosition, setupBoard);
    }

    /// <summary>
    /// Tests if a piece can move to a given set of offsets from its current position. Offsets resulting in invalid coordinates are ignored.
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="offsetsFromCurrentPosition"></param>
    /// <param name="shouldBeAbleToMove"></param>
    /// <param name="setupBoard"></param>
    private static void AbleToMoveWhenOffsetBy(this ChessPiece piece,
        IReadOnlyCollection<MoveOffset> offsetsFromCurrentPosition, bool shouldBeAbleToMove,
        Action<Board, Coordinate>? setupBoard = null)
    {
        var board = new Board(new TestInvalidMoveHandler());
        for (byte rank = 1; rank < 8; rank++)
        for (var file = 'a'; file < 'h'; file++)
        {
            var from = new Coordinate(file, rank);

            board.Clear();
            board[from] = piece;
            setupBoard?.Invoke(board, from);

            var pieceAtFrom = board.Pieces[from];
            foreach (var offset in offsetsFromCurrentPosition)
            {
                if (from.TryApplyOffset(offset, out var to))
                {
                    pieceAtFrom.CheckMove(to!.Value).Valid
                        .Should().Be(shouldBeAbleToMove,
                            $"{pieceAtFrom.ChessPiece} should {(shouldBeAbleToMove ? "" : "not ")}be able to move from {from} to {to})");
                }
            }
        }
    }

    public static void SetOffsetPositions(this Board board, Coordinate from, IReadOnlyCollection<MoveOffset> offsets,
        ChessPiece piece)
    {
        foreach (var offset in offsets)
        {
            if (from.TryApplyOffset(offset, out var to))
            {
                board[to!.Value] = piece;
            }
        }
    }

    /// <summary>
    /// Sets the positions of the board to be 2 cells away from the given coordinate in all directions.
    /// </summary>
    /// <param name="board"></param>
    /// <param name="coordinate">The coordinate to set the positions around</param>
    /// <param name="piece"></param>
    public static void Surround2CellsFrom(this Board board, Coordinate coordinate, ChessPiece piece)
    {
        SetOffsetPositions(board, coordinate, new MoveOffset[]
        {
            (-2, -2), (-1, -2), (0, -2), (1, -2), (2, -2),
            (-2, -1), (2, -1),
            (-2, 0), (2, 0),
            (-2, 1), (2, 1),
            (-2, 2), (-1, 2), (0, 2), (1, 2), (2, 2),
        }, piece);
    }

    public static void Surround(this Board board, Coordinate coordinate, ChessPiece piece)
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
            (1, -1),
        }, piece);
    }


    /// <summary>
    /// Returns an "inverse" of the given offsets. The inverse of a set of offsets is the set of all offsets that are not in the given set bounded by -8 to 8. Note that not all of these offsets are valid coordinates on a chess board, so they should be checked before use.  
    /// </summary>
    /// <param name="offsets">a collection of offsets</param>
    /// <returns></returns>
    public static IEnumerable<MoveOffset> Inverse(this IReadOnlyCollection<MoveOffset> offsets)
    {
        for (int df = -8; df <= 8; df++)
        {
            for (int dr = -8; dr <= 8; dr++)
            {
                if (offsets.Contains(new MoveOffset(dr, df)))
                    continue;

                yield return new MoveOffset(df, dr);
            }
        }
    }

    public const int LegalPositionValue = 1;

    /// <summary>
    ///  Converts a 17x17 matrix of bytes to an array of MoveOffsets. The matrix should be centered at the center of the matrix i.e. (8, 8). The matrix should be 17x17.
    /// </summary>
    /// <param name="movesFromCenter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static MoveOffset[] ToMoveOffsets(this byte[,] movesFromCenter)
    {
        var offsets = new List<MoveOffset>();
        int files = movesFromCenter.GetLength(0);
        int ranks = movesFromCenter.GetLength(1);
        if (files != 15 || ranks != 15)
        {
            throw new ArgumentException("Matrix must be 15x15");
        }

        for (var i = 0; i < files; i++)
        {
            for (var j = 0; j < ranks; j++)
            {
                if (movesFromCenter[i, j] == LegalPositionValue)
                {
                    offsets.Add((i - 7, j - 7));
                }
            }
        }

        return offsets.ToArray();
    }
}