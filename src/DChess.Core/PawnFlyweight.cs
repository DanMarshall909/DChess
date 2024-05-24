namespace DChess.Core;

public record PawnFlyweight : PieceFlyweight
{
    public PawnFlyweight(Piece piece, Coordinate coordinate, Board board) 
        : base(piece, coordinate, board)
    {
    }
}