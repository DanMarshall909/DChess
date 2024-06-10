using DChess.Core.Game;

namespace DChess.Test.Unit;

public abstract class PieceTestBase
{
    protected PieceTestBase() => Game = new Game(InvalidMoveHandler);
    protected IInvalidMoveHandler InvalidMoveHandler { get; } = new TestInvalidMoveHandler();
    protected Game Game { get; init; }
}