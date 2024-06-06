using DChess.Core.Board;

namespace DChess.Test.Unit.Rules;

public class GeneralGameRuleTests: BoardTestBase
{
    [Fact(DisplayName = "A properties cannot take its own properties")]
    public void a_piece_cannot_take_its_own_piece()
    {
        // Arrange
        var whitePawn = new Properties(PieceType.Pawn, Colour.White);

        Board.Set(a1, whitePawn);
        Board.Set(b2, whitePawn);

        // Act
        var boardPiece = Board.Pieces[a1];
        var result = boardPiece.CheckMove(b2);

        result.IsValid.Should().BeFalse();
    }
}