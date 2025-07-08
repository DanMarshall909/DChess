using DChess.Core.Game;
using FluentAssertions;
using Xunit;

namespace DChess.Core.SimpleTests;

public class GameNullabilityTests
{
    [Fact(DisplayName = "UndoLastMove should not throw null reference exception when no moves have been made")]
    public void undo_last_move_should_not_throw_when_no_moves_made()
    {
        var game = new Game.Game(new Board(), new TestErrorHandler(), 3);
        
        Action act = () => game.UndoLastMove();
        
        act.Should().NotThrow<NullReferenceException>("Game should handle case where no moves have been made");
    }
    
    [Fact(DisplayName = "Game should initialize properly without null reference warnings")]
    public void game_should_initialize_properly()
    {
        var game = new Game.Game(new Board(), new TestErrorHandler(), 3);
        
        game.Should().NotBeNull();
        game.Board.Should().NotBeNull();
        game.CurrentPlayer.Should().Be(Colour.White);
    }
}