using DChess.Core.Board;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

internal record King : Piece
{
    public King(Arguments arguments) : base(arguments)
    {
    }

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Position, to);
        
        if(!move.IsAdjacent)
            return move.AsInvalidResult("King can only move 1 square at a time");
        
        return move.AsOkResult;
    }
}