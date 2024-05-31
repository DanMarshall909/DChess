namespace DChess.Test.Unit.Rules.Pieces;

public class RookTests(BoardFixture fixture) :BoardTestBase(fixture)
{

    [Fact(DisplayName = "Rooks only can move vertically")]
    public void rooks_can_move_vertically()
    {
    }
}