using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public record Pawn : Piece, IIgnorePathCheck
{
    public Pawn(Arguments arguments)
        : base(arguments)
    {
    }

    public override string PieceName => "Pawn";

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Current, to);

        if (move.IsBackwards(Colour))
            return move.AsInvalidResult("Pawns can only move forward");

        if (move.IsDiagonal)
        {
            if (!Board.HasPieceAt(to))
                return move.AsInvalidResult("Pawns can only side-step when capturing");

            return move.HorizontalDistance == 1
                ? move.AsOkResult
                : move.AsInvalidResult("Pawns can only move 1 square diagonally when capturing");
        }
        
        int verticalDistance = move.VerticalDistance;

        bool isFirstMove = Current.File != 2 && Colour == White || 
                           Current.File != 7 && Colour == Black;
        
        if (verticalDistance > 2)
            return move.AsInvalidResult("Pawns can only move 1 or 2 squares forward");
        
        if (move.HorizontalDistance > 2)
            return move.AsInvalidResult("Pawns can only move 1 square horizontally and only when taking a piece");

        if (verticalDistance == 2 && (!isFirstMove))
            return move.AsInvalidResult("Pawns can only move 2 squares forward from starting position");

        return new MoveResult(true, new Move(Current, to), string.Empty);
    }
}