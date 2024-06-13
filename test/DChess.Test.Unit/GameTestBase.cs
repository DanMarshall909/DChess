using DChess.Core.Game;

namespace DChess.Test.Unit;

public abstract class GameTestBase
{
    protected GameTestBase() => Sut = new Game(ErrorHandler);

    protected IErrorHandler ErrorHandler { get; } = new TestErrorHandler();
    protected Game Sut { get; init; }
}