namespace DChess.Core;

public record PawnFlyweight : PieceFlyweight
{
    public PawnFlyweight(Piece piece, Coordinate coordinate, Board board) 
        : base(piece, coordinate, board)
    {
    }

    protected override (bool IsValid, string ReasonInvalid) IsValidMove(Coordinate coordinate, Coordinate to)
    {
        if (to.Rank != coordinate.Rank)
            return (false, "Pawns can only move forward");

        return (true, null);
    }
}