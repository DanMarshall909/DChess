using DChess.Core.Game;

namespace DChess.Test.Unit;

public abstract class GameTestBase
{
    protected GameTestBase() => Sut = new Game(InvalidMoveHandler);

    protected IInvalidMoveHandler InvalidMoveHandler { get; } = new TestInvalidMoveHandler();
    protected Game Sut { get; init; }
}