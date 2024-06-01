using DChess.Core.Moves;

namespace DChess.Core.Pieces;

internal record King : Piece, IIgnorePathCheck
{
    public King(Arguments arguments) : base(arguments)
    {
    }
    public override string PieceName => "King";
    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Current, to);
        
        if(!move.IsAdjacent)
            return move.AsInvalidResult("King can only move 1 square at a time");
        
        return move.AsOkResult;
    }
}