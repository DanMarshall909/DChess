using DChess.Core.Game;

namespace DChess.Test.Unit;

public abstract class GameTestBase
{
    protected GameTestBase() => Sut = new GameState(new Board(), ErrorHandler);

    protected IErrorHandler ErrorHandler { get; } = new TestErrorHandler();
    protected MoveHandler MoveHandler => new(ErrorHandler);
    protected GameState Sut { get; init; }
}