namespace DChess.Core;

public record Pawn : Piece
{
    public Pawn(Arguments arguments) 
        : base(arguments)
    {
    }

    protected override MoveResult ValidateMove(Move move)
    {
        if (move.To.Rank != move.From.Rank)
            return new(false, move, "Pawns can only move forward");

        return new(true, move, string.Empty);
    }
}