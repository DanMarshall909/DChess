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
    public static void ShouldBeAbleToMoveToOffsets(this ChessPiece piece,
        IReadOnlyCollection<Offset> offsetsFromCurrentPosition, Action<Board, Coordinate>? setupBoard = null)
        => AbleToMoveWhenOffsetBy(piece, offsetsFromCurrentPosition, true, setupBoard);

    /// <summary>
    /// Tests that every offset in  invalidOffsetsFromCurrentPosition array results in an invalid move.  Offsets resulting in invalid coordinates are ignored.
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="invalidOffsetsFromCurrentPosition"></param>
    /// <param name="setupBoard"></param>
    public static void ShouldNotBeAbleToMoveTo(this ChessPiece piece,
        IReadOnlyCollection<Offset> invalidOffsetsFromCurrentPosition, Action<Board, Coordinate>? setupBoard = null)
        => AbleToMoveWhenOffsetBy(piece, invalidOffsetsFromCurrentPosition, false, setupBoard);

    /// <summary>
    /// Tests if a piece can move to a given set of offsets from its current position. Offsets resulting in invalid coordinates are ignored.
    /// </summary>
    /// <param name="piece"></param>
    /// <param name="offsetsFromCurrentPosition"></param>
    /// <param name="shouldBeAbleToMove"></param>
    /// <param name="setupBoard"></param>
    private static void AbleToMoveWhenOffsetBy(this ChessPiece piece,
        IReadOnlyCollection<Offset> offsetsFromCurrentPosition, bool shouldBeAbleToMove,
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
                if (from.TryOffset(offset, out var to))
                {
                    pieceAtFrom.CheckMove(to!.Value).Valid
                        .Should().Be(shouldBeAbleToMove,
                            $"{pieceAtFrom.GetType()} should {(shouldBeAbleToMove ? "" : "not ")}be able to move from {from} to {to})");
                }
            }
        }
    }

    public static void SurroundWith(this Board board, Coordinate from, ChessPiece chessPiece)
    {
        void TrySet(Offset offset)
        {
            if (from.TryOffset(offset, out var coordinate))
            {
                board[coordinate!.Value] = chessPiece;
            }
        }


        TrySet((-1, 1));
        TrySet((1, 1));
        TrySet((1, 1));

        TrySet((-1, 0));
        TrySet((1, 0));

        TrySet((-1, -1));
        TrySet((1, -1));
        TrySet((1, -1));
    }


    /// <summary>
    /// Returns an "inverse" of the given offsets. The inverse of a set of offsets is the set of all offsets that are not in the given set bounded by -8 to 8. Note that not all of these offsets are valid coordinates on a chess board, so they should be checked before use.  
    /// </summary>
    /// <param name="offsets">a collection of offsets</param>
    /// <returns></returns>
    public static IEnumerable<Offset> Inverse(this IReadOnlyCollection<Offset> offsets)
    {
        for (int df = -8; df <= 8; df++)
        {
            for (int dr = -8; dr <= 8; dr++)
            {
                if (offsets.Contains(new Offset(dr, df)))
                    continue;

                yield return new Offset(df, dr);
            }
        }
    }
}