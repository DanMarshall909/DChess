using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class GameStateTests : GameTestBase
{
    [Fact(DisplayName = "Legal moves are detected")]
    public void legal_moves_are_detected()
    {
        Sut.Board.Place(WhiteKing, f1);
        Sut.Board.Place(BlackQueen, a2);
        Sut.Board.Place(BlackKing, f8);
        Sut.HasLegalMoves(White).Should().BeTrue("White king can move to e1 or g1");
    }

    [Fact(DisplayName = "Game state reflects king in check")]
    public void game_state_reflects_king_in_check()
    {
        Sut.Board.Place(WhiteKing, f1);
        Sut.Board.Place(BlackQueen, a2);
        Sut.Status(White).Should().Be(InPlay);

        Sut.CurrentPlayer = Black;
        Sut.Move(a2, a6);
        Sut.Status(White).Should().Be(Check);
    }

    [Fact(DisplayName = "Game state reflects king in checkmate")]
    public void game_state_reflects_king_in_checkmate()
    {
        Sut.Board.Place(WhiteKing, f1);
        Sut.Board.Place(BlackKing, f8);
        Sut.Board.Place(BlackQueen, c2);
        Sut.Board.Place(BlackRook, b8);
        Sut.Status(White).Should().Be(InPlay);

        Sut.Move(b8, b1);
        Sut.Status(White).Should().Be(Checkmate);
    }
}