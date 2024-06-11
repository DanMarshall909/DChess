using DChess.Core.Game;

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

    public void HandleNoPieceAt(Coordinate moveFrom)
    {
        throw new NotImplementedException();
    }
}