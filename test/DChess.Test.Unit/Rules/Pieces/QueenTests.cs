namespace DChess.Test.Unit.Rules.Pieces;

public class QueenTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    private const int X = LegalPositionValue;

    [Fact(DisplayName = "Queens can only move diagonally, vertically or horizontally")]
    public void queens_can_only_move_diagonally_vertically_or_horizontally()
    {
        WhiteQueen.ShouldOnlyBeAbleToMoveTo((new byte[15, 15]
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
            { X, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, X },
        }).ToMoveOffsets());
    }
}