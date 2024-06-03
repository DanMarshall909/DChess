using DChess.Core.Board;

namespace DChess.Test.Unit;

public abstract class BoardTestBase
{
    protected BoardTestBase() => Board = new Board(InvalidMoveHandler);
    protected IInvalidMoveHandler InvalidMoveHandler { get; } = new TestInvalidMoveHandler();
    protected Board Board { get; init; }
}