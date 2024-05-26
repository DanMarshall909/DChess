namespace DChess.Core;

public interface IInvalidMoveHandler
{
    public void HandleInvalidMove(Move move, string? message);
}