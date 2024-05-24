namespace DChess.Core;

public abstract record PieceFlyweight(Piece Piece, Coordinate Coordinate, Board Board)
{
    public void Move(Coordinate to)
    {
        var isValidMove = IsValidMove(Coordinate, to);

        if (isValidMove.IsValid != true)
            throw new InvalidMoveException(Coordinate, to, isValidMove.ReasonInvalid);

        Board.Move(Coordinate, to);
    }

    protected abstract (bool IsValid, string ReasonInvalid) IsValidMove(Coordinate coordinate, Coordinate to);
}