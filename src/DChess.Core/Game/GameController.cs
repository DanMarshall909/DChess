using DChess.Core.Moves;
using DChess.Test.Unit;

namespace DChess.Core.Game;

public class GameController(IErrorHandler errorHandler)
{
    private Game CurrentGame { get; set; } = null!;

    public void NewGame(GameOptions gameOptions)
    {
        CurrentGame = new Game(errorHandler, gameOptions);
    }
}