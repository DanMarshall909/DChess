using DChess.Core.Flyweights;
using DChess.Core.Game;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class ChessPieceTests : GameTestBase
{
    [Fact(DisplayName = "A piece can be moved")]
    public void a_piece_can_be_moved()
    {
        Sut.Board.Place(WhitePawn, a2);
        Sut.Board.Place(WhiteKing, f1);
        Sut.Move(a2, b3);

        Sut.Board.ShouldHavePieceAt(b3, WhitePawn, "the piece should be moved");
        Sut.Board.ShouldNotHavePieceAt(a2, "the piece should be moved from its original position");
        Sut.FriendlyPieces(White).Count().Should()
            .Be(2, "the piece should be moved, not duplicated");
    }

    [Fact(DisplayName = "A queen can move backwards")]
    public void a_queen_can_move_backwards()
    {
        Sut.Board.Place(WhiteQueen, b2);
        Sut.Board.Place(WhiteKing, f1);

        Sut.Move(b2, a3);

        var args = new PieceContext(WhiteQueen, a3);
        Sut.Board.ShouldHavePieceAt(a3, WhiteQueen, "the queen should be moved");
        Sut.Board.ShouldNotHavePieceAt(b2, "the queen should be moved from its original position");
        Sut.FriendlyPieces(White).Count().Should()
            .Be(2, "the piece should be moved, not duplicated");
    }

    [Fact(DisplayName = "Invalid move should not be allowed")]
    public void invalid_move_should_not_be_allowed()
    {
        Sut.Board.Place(WhiteKing, f1);
        Sut.Board.Place(new PieceAttributes(White, Piece.Kind.Pawn), a1);

        Sut.Board.ShouldNotBeValidMove(a1, a6, "pawns cannot move 5 squares forward");
    }
}