using DChess.Core.Game;

namespace DChess.Test.Unit;

public class GameStateTests: GameTestBase
{
    [Fact(DisplayName = "Game state reflects king in check")]
    public void game_state_reflects_king_in_check()
    {
        Game.GameState.Place(WhiteKing, f1);
        Game.GameState.Place(BlackQueen, a2);
        Game.GameState.IsInCheck(Colour.White).Should().BeFalse();
        
        Game.Move(a2, a1);
        Game.GameState.IsInCheck(Colour.White).Should().BeTrue();
    }
}