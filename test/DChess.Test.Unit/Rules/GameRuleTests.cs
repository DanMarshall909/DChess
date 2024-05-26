using DChess.Core.Moves;
using DChess.Core.Pieces;
using static DChess.Core.Coordinate;

namespace DChess.Test.Unit.Rules;

public class GameRuleTests(BoardFixture fixture) : BoardTestBase(fixture)
{

    [Fact(DisplayName = "A piece cannot take its own piece")]
    public void a_piece_cannot_take_its_own_piece()
    {
        // Arrange
        var whitePawn = new ChessPiece(PieceType.Pawn, Colour.White);

        Fixture.Board[a1] = whitePawn;
        Fixture.Board[b1] = whitePawn;

        // Act
        var boardPiece = Board.Pieces[a1];
        var move = new Move(a1, b1);
        var result = boardPiece.CheckMoveTo(move);
        
        result.Valid.Should().BeFalse();
    }
}