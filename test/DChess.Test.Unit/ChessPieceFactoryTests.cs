using DChess.Core.Flyweights;
using DChess.Core.Game;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class ChessPieceFactoryTests : GameTestBase
{
    [Fact(DisplayName = "A piece can be retrieved from the pool")]
    public void a_piece_can_be_retrieved_from_the_pool()
    {
        var attribute = new PieceAttributes(White, Piece.Kind.Pawn);

        var piece = ChessPieceFactory.PieceWithContext(new PieceContext(a1, attribute));
        var secondPieceWithSameDefinition = ChessPieceFactory.PieceWithContext(new PieceContext(a1, attribute));

        piece.Should().BeEquivalentTo(new Pawn(new PieceContext(attribute, a1)));
        piece.Should().Be(secondPieceWithSameDefinition, "the pieceAttributes should be the same instance");

        piece.GetType().Should().Be<Pawn>();
    }
}