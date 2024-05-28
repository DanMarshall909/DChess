namespace DChess.Core.Exceptions;

public class InvalidCoordinateException  : DChessException
{
    public InvalidCoordinateException(char file, byte rank, string message = "")
    {
        File = file;
        Rank = rank;
        Message = message;
    }

    public InvalidCoordinateException(string positionNameMustBeCharactersLong) => Message = positionNameMustBeCharactersLong;

    public char? File { get; set; }
    public byte? Rank { get; set; }
    public override string Message { get; }
}