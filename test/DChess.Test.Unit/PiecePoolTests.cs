using DChess.Core.Game;
using DChess.Core.Pieces;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class PiecePoolTests : GameTestBase
{
    [Fact(DisplayName = "A properties can be retrieved from the pool")]
    public void a_piece_can_be_retrieved_from_the_pool()
    {
        var game = base.Sut;
        var chessPiece = new Properties(PieceType.Pawn, White);

        var pool = new PiecePool(game);
        var piece = pool.PieceWithProperties(a1, chessPiece);
        var secondPieceWithSameDefinition = pool.PieceWithProperties(a1, chessPiece);

        piece.Should().BeEquivalentTo(new Pawn(new Piece.Arguments(chessPiece, a1)));
        piece.Should().Be(secondPieceWithSameDefinition, "the properties should be the same instance");

        piece.GetType().Should().Be<Pawn>();
    }
}