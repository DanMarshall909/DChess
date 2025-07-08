namespace DChess.Core.Errors;

public class DChessException : Exception
{
    private readonly string? _message;

    protected DChessException(string? message = null) => _message = message;

    public override string Message => _message ?? base.Message;
}