using DChess.Core.Moves;

namespace DChess.Test.Unit;

public class TestInvalidMoveHandler : IInvalidMoveHandler
{
    public readonly List<MoveResult> InvalidMoves = new();

    public void HandleInvalidMove(Move move, string? message)
    {
        InvalidMoves.Add(new MoveResult(false, move, message));
    }
}