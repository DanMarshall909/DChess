namespace DChess.Core;

public class Cell(Piece? piece)
{
    public Piece? Piece { get; set; } = piece;
}