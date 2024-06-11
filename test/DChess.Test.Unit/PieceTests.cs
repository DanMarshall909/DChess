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
        Sut.GameState.Place(WhitePawn, a2);
        Sut.GameState.Place(WhiteKing, f1);
        Sut.Move(a2,b3);

        // ReSharper disable once HeapView.BoxingAllocation
        Sut.GameState.Pieces[b3].Properties.Should()
            .BeEquivalentTo(WhitePawn, "the properties should be moved");

        Sut.GameState.FriendlyPieces(White).Count().Should()
            .Be(2, "the properties should be moved, not duplicated");
    }
    
    [Fact(DisplayName = "A queen can move backwards")]
    public void a_queen_can_move_backwards()
    {
        Sut.GameState.Place(WhiteQueen, b2);
        Sut.GameState.Place(WhiteKing, f1);
        Sut.GameState.FriendlyPieces(White).Count().Should().Be(2);

        Sut.Move(b2, a3);

        var args = new Piece.Arguments(WhiteQueen, a3, Sut.GameState, _invalidMoveHandler);
        Sut.GameState.Pieces[a3].Should()
            .BeEquivalentTo(new Queen(args), "the properties should be moved");

        Sut.GameState.FriendlyPieces(White).Count().Should()
            .Be(2, "the properties should be moved, not duplicated");
    }


    [Fact(DisplayName = "Invalid move should not be allowed")]
    public void invalid_move_should_not_be_allowed()
    {
        Sut.GameState.Place(WhiteKing, f1);
        Sut.GameState.Place(new Properties(PieceType.Pawn, White), a1);
        var piece = Sut.GameState.Pieces[a1];

        piece.CheckMove(a6, Sut.GameState).IsValid.Should().BeFalse();
    }
}