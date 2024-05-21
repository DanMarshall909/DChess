using DChess.Core;
using static DChess.Core.Coordinate;
using static DChess.Core.PieceProperties;
using static DChess.Core.PieceProperties.PieceColour;

namespace DChess.Test.Unit;

public class PiecePoolTests
{
    [Fact(DisplayName = "A piece can be retrieved from the pool")]
    public void a_piece_can_be_retrieved_from_the_pool()
    {
        // Arrange
        var pool = new PiecePool();
        var props = new PieceProperties(PieceType.Pawn, White, a1);

        // Act
        var piece = pool[props];
        var piece2 = pool[props];

        // Assert
        piece.PieceProperties.Should().BeEquivalentTo(props);
        piece.Should().Be(piece2, "the piece should be the same instance");
        
        piece.GetType().Should().Be<Pawn>();
    }
    
}