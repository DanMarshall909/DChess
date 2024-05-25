namespace DChess.Core;

public record PawnFlyweight : PieceFlyweight
{
    public PawnFlyweight(Piece piece, Coordinate coordinate, Board board) 
        : base(piece, coordinate, board)
    {
    }

    protected override MoveResult IsValidMove(Move move)
    {
        if (move.To.Rank != move.From.Rank)
            return new(false, move, "Pawns can only move forward");

        return new(true, move, string.Empty);
    }
}