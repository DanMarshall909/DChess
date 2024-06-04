using DChess.Core.Board;

namespace DChess.Test.Unit.Rules;

public class GeneralGameRuleTests: BoardTestBase
{
    [Fact(DisplayName = "A pieceProperties cannot take its own pieceProperties")]
    public void a_piece_cannot_take_its_own_piece()
    {
        // Arrange
        var whitePawn = new PieceProperties(PieceType.Pawn, Colour.White);

        Board.PieceAt[a1] = whitePawn;
        Board.PieceAt[b2] = whitePawn;

        // Act
        var boardPiece = Board.Pieces[a1];
        var result = boardPiece.CheckMove(b2);

        result.IsValid.Should().BeFalse();
    }
}