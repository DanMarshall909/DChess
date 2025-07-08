using DChess.Core.Errors;
using DChess.Core.Game;

namespace DChess.Test.Unit;

public abstract class GameTestBase
{
    protected GameTestBase() => Sut = new Game(new Board(), ErrorHandler, MaxAllowableDepth);
    public int MaxAllowableDepth { get; set; } = 2;

    protected IErrorHandler ErrorHandler { get; } = new TestErrorHandler();
    protected MoveHandler MoveHandler => new(ErrorHandler, MaxAllowableDepth);
    protected Game Sut { get; init; }
}