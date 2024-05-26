using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public record Pawn : Piece
{
    public Pawn(Arguments arguments)
        : base(arguments)
    {
    }

    protected override MoveResult ValidateMove(Coordinate to)
    {
        if (to.Rank != Position.Rank)
            return new(false, new(Position, to), "Pawns can only side-step when capturing");
        
        if ((Colour == White && to.File <= Position.File)
            || (Colour == Black && to.File >= Position.File))
            return new(false, new(Position, to), "Pawns can only move forward");

        return new(true, new(Position, to), string.Empty);
    }
}