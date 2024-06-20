namespace DChess.Core.Errors;

public class DChessException : Exception
{
    private readonly string? _message;

    public override string Message => _message ?? base.Message;

    protected DChessException(string? message = null) => _message = message;
}