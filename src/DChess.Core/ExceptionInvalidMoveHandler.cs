namespace DChess.Core;

public class ExceptionInvalidMoveHandler : IInvalidMoveHandler
{
    public void HandleInvalidMove(Move move, string? message)
    {
        throw new InvalidMoveException(move, message);
    }
}