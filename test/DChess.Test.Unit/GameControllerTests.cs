using DChess.Core.Game;

namespace DChess.Test.Unit;

public class GameControllerTests
{
    private readonly IErrorHandler _errorHandler = new TestErrorHandler();

    [Fact(DisplayName = "A game can be created with default options")]
    public void a_game_can_be_created_()
    {
        var controller = new GameController(_errorHandler);
        var newGameOptions = new GameOptions(Colour.Black);
        
        controller.NewGame(newGameOptions);
    }
}