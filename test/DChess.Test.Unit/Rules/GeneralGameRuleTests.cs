using DChess.Core.Board;

namespace DChess.Test.Unit.Rules;

public class GeneralGameRuleTests: BoardTestBase
{
    [Fact(DisplayName = "A piece cannot take its own piece")]
    public void a_piece_cannot_take_its_own_piece()
    {
        // Arrange
        var whitePawn = new ChessPiece(PieceType.Pawn, Colour.White);

        Board.ChessPieces[a1] = whitePawn;
        Board.ChessPieces[b2] = whitePawn;

        // Act
        var boardPiece = Board.Pieces[a1];
        var result = boardPiece.CheckMove(b2);

        result.IsValid.Should().BeFalse();
    }
}