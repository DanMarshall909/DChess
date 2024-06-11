using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Test.Unit;

public class GameController(IInvalidMoveHandler InvalidMoveHandler)
{
    public Game CurrentGame { get; private set; }

    public void NewGame(GameOptions gameOptions)
    {
        CurrentGame = new Game(InvalidMoveHandler, gameOptions);
    }
}