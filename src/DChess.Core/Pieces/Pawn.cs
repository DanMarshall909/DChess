using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public record Pawn : Piece
{
    public Pawn(Arguments arguments)
        : base(arguments)
    {
    }

    protected override MoveResult ValidateMove(Move move)
    {
        if (move.To.Rank != move.From.Rank)
            return new(false, move, "Pawns can only side-step when capturing");
        
        if ((Colour == White && move.To.File <= move.From.File)
            || (Colour == Black && move.To.File >= move.From.File))
            return new(false, move, "Pawns can only move forward");

        return new(true, move, string.Empty);
    }
}