namespace DChess.Test.Unit.Rules.Pieces;

public class RookTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    private const int X = LegalPositionValue;

    [Fact(DisplayName = "Rooks can only move vertically or horizontally")]
    public void rooks_can_only_move_vertically()
    {
        var moveOffsets = (new byte[15, 15]
        {
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { X, X, X, X, X, X, X, 0, X, X, X, X, X, X, X },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
        }).ToMoveOffsets();
        WhiteRook.ShouldOnlyBeAbleToMoveTo(moveOffsets);
    }
}