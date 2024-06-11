using DChess.Core.Game;
using DChess.Core.Pieces;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class PiecePoolTests : GameTestBase
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();

    [Fact(DisplayName = "A properties can be retrieved from the pool")]
    public void a_piece_can_be_retrieved_from_the_pool()
    {
        var game = base.Game;
        var chessPiece = new Properties(PieceType.Pawn, White);

        var pool = new PiecePool(game, _invalidMoveHandler);
        var piece = pool.PieceWithProperties(a1, chessPiece);
        var secondPieceWithSameDefinition = pool.PieceWithProperties(a1, chessPiece);

        piece.Should().BeEquivalentTo(new Pawn(new Piece.Arguments(chessPiece, a1, game, _invalidMoveHandler)));
        piece.Should().Be(secondPieceWithSameDefinition, "the properties should be the same instance");

        piece.GetType().Should().Be<Pawn>();
    }
}