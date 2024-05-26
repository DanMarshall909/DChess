namespace DChess.Core.Moves;

public interface IInvalidMoveHandler
{
    public void HandleInvalidMove(Move move, string? message);
}