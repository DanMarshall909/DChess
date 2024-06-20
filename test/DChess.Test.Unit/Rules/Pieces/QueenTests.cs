namespace DChess.Test.Unit.Rules.Pieces;

public class QueenTests : GameTestBase
{
    private const int X = LegalPositionValue;

    [Fact(DisplayName = "Queens can only move diagonally, vertically or horizontally")]
    public void queens_can_only_move_diagonally_vertically_or_horizontally()
    {
        WhiteQueen.ShouldOnlyBeAbleToMoveTo(new byte[15, 15]
        {
            { X, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, X },
            { 0, X, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, X, 0 },
            { 0, 0, X, 0, 0, 0, 0, X, 0, 0, 0, 0, X, 0, 0 },
            { 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0 },
            { 0, 0, 0, 0, X, 0, 0, X, 0, 0, X, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, X, 0, X, 0, X, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, X, X, 0, 0, 0, 0, 0, 0 },
            { X, X, X, X, X, X, X, 0, X, X, X, X, X, X, X },
            { 0, 0, 0, 0, 0, 0, X, X, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, X, 0, X, 0, X, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, X, 0, 0, X, 0, 0, X, 0, 0, 0, 0 },
            { 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0 },
            { 0, 0, X, 0, 0, 0, 0, X, 0, 0, 0, 0, X, 0, 0 },
            { 0, X, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, X, 0 },
            { X, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, X }
        }.ToMoveOffsets(), new TestErrorHandler());
    }

    [Fact(DisplayName = "Queens cannot jump over other pieces")]
    public void queens_cannot_jump_over_other_pieces()
    {
        WhiteQueen.ShouldOnlyBeAbleToMoveTo(new byte[15, 15]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, X, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, X, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        }.ToMoveOffsets(), ErrorHandler, (board, coordinate) =>
            board.Surround2CellsFrom(coordinate, WhitePawn));
    }
}