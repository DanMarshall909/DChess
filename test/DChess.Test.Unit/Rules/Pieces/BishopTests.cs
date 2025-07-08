namespace DChess.Test.Unit.Rules.Pieces;

public class BishopTests : GameTestBase
{
    private const int X = LegalPositionValue;

    [Fact(DisplayName = "Bishops can only move vertically or horizontally")]
    public void bishops_can_only_move_vertically_or_horizontally()
    {
        WhiteBishop.ShouldOnlyBeAbleToMoveTo(new byte[,]
        {
            { X, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, X },
            { 0, X, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, X, 0 },
            { 0, 0, X, 0, 0, 0, 0, 0, 0, 0, 0, 0, X, 0, 0 },
            { 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0 },
            { 0, 0, 0, 0, X, 0, 0, 0, 0, 0, X, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, X, 0, 0, 0, 0, 0, X, 0, 0, 0, 0 },
            { 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0 },
            { 0, 0, X, 0, 0, 0, 0, 0, 0, 0, 0, 0, X, 0, 0 },
            { 0, X, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, X, 0 },
            { X, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, X }
        }.ToMoveOffsets(), ErrorHandler);
    }

    [Fact(DisplayName = "Bishops cannot jump over other pieces")]
    public void bishops_cannot_jump_over_other_pieces()
    {
        WhiteBishop.ShouldOnlyBeAbleToMoveTo(new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        }.ToMoveOffsets(), ErrorHandler, (board, square) =>
            board.Surround2CellsFrom(square, WhitePawn));
    }
}