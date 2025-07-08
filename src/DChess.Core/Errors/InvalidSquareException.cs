namespace DChess.Core.Errors;

public class InvalidSquareException : DChessException
{
    public InvalidSquareException(char file, byte rank, string message = "")
    {
        File = file;
        Rank = rank;
        Message = message;
    }

    public InvalidSquareException(string positionNameMustBeCharactersLong) =>
        Message = positionNameMustBeCharactersLong;

    public char? File { get; set; }
    public byte? Rank { get; set; }
    public override string Message { get; }
}