using DChess.Core;
using static DChess.Core.Coordinate;
using static DChess.Core.PieceStruct;
using static DChess.Core.PieceStruct.PieceColour;

namespace DChess.Test.Unit;

public class PiecePoolTests
{
    private TestInvalidMoveHandler _invalidMoveHandler = new();

    [Fact(DisplayName = "A pieceStruct can be retrieved from the pool")]
    public void a_piece_can_be_retrieved_from_the_pool()
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);
        var pieceStruct = new PieceStruct(PieceType.Pawn, White);

        // Act
        var pool = new PiecePool(board, _invalidMoveHandler);
        var piece = pool.Get(a1, pieceStruct);
        var secondPieceWithSameDefinition = pool.Get(a1, pieceStruct);

        // Assert
        piece.Should().BeEquivalentTo(new Pawn(new(pieceStruct, a1, board, _invalidMoveHandler)));
        piece.Should().Be(secondPieceWithSameDefinition, "the piece should be the same instance");
        
        piece.GetType().Should().Be<Pawn>();
    }
    
}