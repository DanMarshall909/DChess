using DChess.Core.Game;

namespace DChess.Test.Unit;

public abstract class GameTestBase
{
    protected GameTestBase() => Game = new Game(InvalidMoveHandler);

    protected IInvalidMoveHandler InvalidMoveHandler { get; } = new TestInvalidMoveHandler();
    protected Game Game { get; init; }
}