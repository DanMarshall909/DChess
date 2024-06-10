using DChess.Core.Game;

namespace DChess.Test.Unit.Rules;

public class GeneralPieceRuleTests: PieceTestBase
{
    [Fact(DisplayName = "A properties cannot take its own properties")]
    public void a_piece_cannot_take_its_own_piece()
    {
        // Arrange
        var whitePawn = new Properties(PieceType.Pawn, Colour.White);

        Game.GameState.Set(a1, whitePawn);
        Game.GameState.Set(b2, whitePawn);

        // Act
        var boardPiece = Game.GameState.Pieces[a1];
        var result = boardPiece.CheckMove(b2);

        result.IsValid.Should().BeFalse();
    }
}