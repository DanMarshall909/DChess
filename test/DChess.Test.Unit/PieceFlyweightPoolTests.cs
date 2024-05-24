using DChess.Core;
using static DChess.Core.Coordinate;
using static DChess.Core.Piece;
using static DChess.Core.Piece.PieceColour;

namespace DChess.Test.Unit;

public class PieceFlyweightPoolTests
{
    [Fact(DisplayName = "A piece can be retrieved from the pool")]
    public void a_piece_can_be_retrieved_from_the_pool()
    {
        // Arrange
        var board = new Board();
        var piece = new Piece(PieceType.Pawn, White);

        // Act
        var pool = new PieceFlyweightPool(board);
        var flyweight = pool.Get(a1, piece);
        var secondFlyweightForSamePieceAndCoordinate = pool.Get(a1, piece);

        // Assert
        flyweight.Should().BeEquivalentTo(new PawnFlyweight(piece, a1, board));
        flyweight.Should().Be(secondFlyweightForSamePieceAndCoordinate, "the piece should be the same instance");
        
        flyweight.GetType().Should().Be<PawnFlyweight>();
    }
    
}