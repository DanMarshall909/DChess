using DChess.Core.Game;

namespace DChess.Test.Unit.Rules;

public class GeneralGameRuleTests: GameTestBase
{
    [Fact(DisplayName = "A properties cannot take its own properties")]
    public void a_piece_cannot_take_its_own_piece()
    {
        var whitePawn = new Properties(PieceType.Pawn, Colour.White);

        Game.GameState.Place(whitePawn, a1);
        Game.GameState.Place(whitePawn, b2);

        var boardPiece = Game.GameState.Pieces[a1];
        var result = boardPiece.CheckMove(b2);

        result.IsValid.Should().BeFalse();
    }
}

