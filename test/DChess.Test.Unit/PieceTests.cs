using DChess.Core.Game;
using DChess.Core.Pieces;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class PieceTests : GameTestBase
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();

    [Fact(DisplayName = "A piece can be moved")]
    public void a_piece_can_be_moved()
    {
        Game.GameState.Place(WhitePawn, a2);
        Game.GameState.Place(WhiteKing, f1);

        var piece = Game.GameState.Pieces[a2];
        piece.MoveTo(b3);

        // ReSharper disable once HeapView.BoxingAllocation
        Game.GameState.Pieces[b3].Properties.Should()
            .BeEquivalentTo(WhitePawn, "the properties should be moved");

        Game.GameState.FriendlyPieces(White).Count().Should()
            .Be(2, "the properties should be moved, not duplicated");
    }
    
    [Fact(DisplayName = "A queen can move backwards")]
    public void a_queen_can_move_backwards()
    {
        Game.GameState.Place(WhiteQueen, b2);
        Game.GameState.Place(WhiteKing, f1);
        Game.GameState.FriendlyPieces(White).Count().Should().Be(2);

        var piece = Game.GameState.Pieces[b2];
        piece.MoveTo(a3);

        var args = new Piece.Arguments(WhiteQueen, a3, Game, _invalidMoveHandler);
        Game.GameState.Pieces[a3].Should()
            .BeEquivalentTo(new Queen(args), "the properties should be moved");

        Game.GameState.FriendlyPieces(White).Count().Should()
            .Be(2, "the properties should be moved, not duplicated");
    }


    [Fact(DisplayName = "Invalid move should not be allowed")]
    public void invalid_move_should_not_be_allowed()
    {
        Game.GameState.Place(WhiteKing, f1);
        Game.GameState.Place(new Properties(PieceType.Pawn, White), a1);
        var piece = Game.GameState.Pieces[a1];

        piece.CheckMove(a6).IsValid.Should().BeFalse();
    }
}