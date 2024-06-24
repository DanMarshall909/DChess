using DChess.Core.Game;
using DChess.Core.Pieces;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class PieceFlyweightPoolTests : GameTestBase
{
    [Fact(DisplayName = "A piece can be retrieved from the pool")]
    public void a_piece_can_be_retrieved_from_the_pool()
    {
        var chessPiece = new PieceAttributes(ChessPiece.Type.Pawn, White);

        var piece = PieceFlyweightPool.PieceWithProperties(new(a1, chessPiece));
        var secondPieceWithSameDefinition = PieceFlyweightPool.PieceWithProperties(new(a1, chessPiece));

        piece.Should().BeEquivalentTo(new Pawn(new PieceContext(chessPiece, a1)));
        piece.Should().Be(secondPieceWithSameDefinition, "the pieceAttributes should be the same instance");

        piece.GetType().Should().Be<Pawn>();
    }
}