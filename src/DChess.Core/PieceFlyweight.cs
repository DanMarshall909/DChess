namespace DChess.Core;

public abstract record PieceFlyweight(Piece Piece, Coordinate Coordinate, Board Board)
{
    public void Move(Coordinate to)
    {
        Board.Move(Coordinate, to);
    }
}