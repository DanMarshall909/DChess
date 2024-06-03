namespace DChess.Test.Unit.Rules.Pieces;

public class BishopTests: BoardTestBase
{
    private const int X = LegalPositionValue;

    [Fact(DisplayName = "Bishops can only move vertically or horizontally")]
    public void bishops_can_only_move_vertically_or_horizontally()
    {
        WhiteBishop.ShouldOnlyBeAbleToMoveTo(new byte[15, 15]
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
        }.ToMoveOffsets());
    }

    [Fact(DisplayName = "Bishops cannot jump over other pieces")]
    public void bishops_cannot_jump_over_other_pieces()
    {
        WhiteBishop.ShouldOnlyBeAbleToMoveTo(new byte[15, 15]
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
        }.ToMoveOffsets(), (board, coordinate) =>
            board.Surround2CellsFrom(coordinate, WhitePawn));
    }
}