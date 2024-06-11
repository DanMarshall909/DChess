namespace DChess.Test.Unit;

public class TestInvalidMoveHandler : IInvalidMoveHandler
{
    public readonly List<MoveResult> InvalidMoves = new();

    public void HandleInvalidMove(MoveResult result)
    {
        InvalidMoves.Add(result);
    }

    public void HandleNoKingFound()
    {
    }
}