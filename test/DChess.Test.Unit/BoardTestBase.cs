using DChess.Core.Board;

namespace DChess.Test.Unit;

public abstract class BoardTestBase
{
    protected BoardTestBase() => Game = new Game(InvalidMoveHandler);
    protected IInvalidMoveHandler InvalidMoveHandler { get; } = new TestInvalidMoveHandler();
    protected Game Game { get; init; }
}