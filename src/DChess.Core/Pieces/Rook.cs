using DChess.Core.Board;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

internal record Rook : Piece
{
    public Rook(Arguments arguments) : base(arguments)
    {
    }

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Position, to);
        
        if (!(move.IsHorizontal || move.IsVertical))
            return move.AsInvalidResult("Rook can only move in a straight line");
        
        return move.AsOkResult;
    }
}