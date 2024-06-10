using DChess.Core.Game;
using DChess.Core.Pieces;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class PiecePoolTests
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();

    [Fact(DisplayName = "A properties can be retrieved from the pool")]
    public void a_piece_can_be_retrieved_from_the_pool()
    {
        // Arrange
        var game = new Game(_invalidMoveHandler);
        var chessPiece = new Properties(PieceType.Pawn, White);

        // Act
        var pool = new PiecePool(game, _invalidMoveHandler);
        var piece = pool.PieceWithProperties(a1, chessPiece);
        var secondPieceWithSameDefinition = pool.PieceWithProperties(a1, chessPiece);

        // Assert
        piece.Should().BeEquivalentTo(new Pawn(new Piece.Arguments(chessPiece, a1, game, _invalidMoveHandler)));
        piece.Should().Be(secondPieceWithSameDefinition, "the properties should be the same instance");

        piece.GetType().Should().Be<Pawn>();
    }
}