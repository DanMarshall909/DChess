using DChess.Core.Board;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

internal record Bishop : Piece
{
    public Bishop(Arguments arguments) : base(arguments)
    {
    }

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Position, to);

        if (Math.Abs(to.File - Position.File) != Math.Abs(to.Rank - Position.Rank))
            return move.AsInvalidResult("Bishop can only move diagonally");
        
        return move.AsOkResult;
    }
}