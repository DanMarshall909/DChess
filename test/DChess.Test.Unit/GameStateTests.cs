using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class GameStateTests : GameTestBase
{
    [Fact(DisplayName = "Legal moves are detected")]
    public void legal_moves_are_detected()
    {
        Game.GameState.Place(WhiteKing, f1);
        Game.GameState.Place(BlackQueen, a2);
        Game.GameState.Place(BlackKing, f8);
        Game.GameState.HasLegalMoves(White).Should().BeTrue("White king can move to e1 or g1");
    }

    [Fact(DisplayName = "Game state reflects king in check")]
    public void game_state_reflects_king_in_check()
    {
        Game.GameState.Place(WhiteKing, f1);
        Game.GameState.Place(BlackQueen, a2);
        Game.GameState.Status(White).Should().Be(InPlay);

        Game.Move(a2, a6);
        Game.GameState.Status(White).Should().Be(Check);
    }

    [Fact(DisplayName = "Game state reflects king in checkmate")]
    public void game_state_reflects_king_in_checkmate()
    {
        Game.GameState.Place(WhiteKing, f1);
        Game.GameState.Place(BlackKing, f8);
        Game.GameState.Place(BlackQueen, c2);
        Game.GameState.Place(BlackRook, b8);
        Game.GameState.Status(White).Should().Be(InPlay);

        Game.Move(b8, b1);
        Game.GameState.Status(White).Should().Be(Checkmate);
    }
}