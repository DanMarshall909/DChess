using DChess.Core.Moves;

namespace DChess.Core.Exceptions;

public class InvalidMoveException(Move move, string? message = null) : DChessException
{
    public Move Move { get; } = move;

    public override string Message { get; } =
        $"Invalid move from {move}" + (message is not null ? $": {message}" : "");
}