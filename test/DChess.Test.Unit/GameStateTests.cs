using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class GameStateTests : GameTestBase
{
    [Fact(DisplayName = "Legal moves are detected")]
    public void legal_moves_are_detected()
    {
        Sut.GameState.Place(WhiteKing, f1);
        Sut.GameState.Place(BlackQueen, a2);
        Sut.GameState.Place(BlackKing, f8);
        Sut.GameState.HasLegalMoves(White).Should().BeTrue("White king can move to e1 or g1");
    }

    [Fact(DisplayName = "gameState state reflects king in check")]
    public void game_state_reflects_king_in_check()
    {
        Sut.GameState.Place(WhiteKing, f1);
        Sut.GameState.Place(BlackQueen, a2);
        Sut.GameState.Status(White).Should().Be(InPlay);

        Sut.GameState.CurrentPlayer = Black;
        Sut.Move(a2, a6);
        Sut.GameState.Status(White).Should().Be(Check);
    }

    [Fact(DisplayName = "gameState state reflects king in checkmate")]
    public void game_state_reflects_king_in_checkmate()
    {
        Sut.GameState.Place(WhiteKing, f1);
        Sut.GameState.Place(BlackKing, f8);
        Sut.GameState.Place(BlackQueen, c2);
        Sut.GameState.Place(BlackRook, b8);
        Sut.GameState.Status(White).Should().Be(InPlay);

        Sut.Move(b8, b1);
        Sut.GameState.Status(White).Should().Be(Checkmate);
    }
}