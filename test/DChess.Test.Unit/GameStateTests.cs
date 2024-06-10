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
    
    [Fact(DisplayName = "Game state reflects king in checkmate")]
    public void game_state_reflects_king_in_checkmate()
    {
        Game.GameState.Place(WhiteKing, f1);
        Game.GameState.Place(BlackKing , f8);
        Game.GameState.Place(BlackQueen, c2);
        Game.GameState.Place(BlackRook, b8);
        Game.GameState.IsInCheckmate(Colour.White).Should().BeFalse();
        
        Game.Move(b8, b1);
        Game.GameState.IsInCheckmate(Colour.White).Should().BeTrue();
    }
}