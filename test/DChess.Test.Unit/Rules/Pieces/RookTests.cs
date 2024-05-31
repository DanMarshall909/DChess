namespace DChess.Test.Unit.Rules.Pieces;

public class BishopTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    private const int X = LegalPositionValue;

    [Fact(DisplayName = "Bishops can only move vertically or horizontally")]
    public void bishops_can_only_move_vertically_or_horizontally()
    {
        WhiteBishop.ShouldOnlyBeAbleToMoveTo((new byte[17, 17]
        {
            { X, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, X },
            { 0, X, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, X, 0 },
            { 0, 0, X, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, X, 0, 0 },
            { 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0 },
            { 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0 },
            { 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0 },
            { 0, 0, X, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, X, 0, 0 },
            { 0, X, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, X, 0 },
            { X, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, X },
        }).ToMoveOffsets());
    }
}