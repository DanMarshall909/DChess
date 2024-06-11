using DChess.Core.Game;

namespace DChess.Test.Unit;

public class GameControllerTests
{
    private readonly IInvalidMoveHandler _invalidMoveHandler = new TestInvalidMoveHandler();

    [Fact(DisplayName = "A game can be created with default options")]
    public void a_game_can_be_created_()
    {
        var controller = new GameController(_invalidMoveHandler);
        var newGameOptions = new GameOptions(Colour.Black);
        
        controller.NewGame(newGameOptions);
    }
}