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
        
        if (!(move.IsDiagonal || move.IsVertical || move.IsHorizontal))
            return move.AsInvalidResult("King move in any direction but only 1 square at a time");
        
        return move.AsOkResult;
    }
}