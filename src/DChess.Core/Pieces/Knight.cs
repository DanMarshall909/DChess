using DChess.Core.Moves;

namespace DChess.Core.Pieces;

internal record Knight : Piece
{
    public Knight(Arguments arguments) : base(arguments)
    {
    }

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Position, to);
        return MoveMustBeLShape(to, move);
    }

    private MoveResult MoveMustBeLShape(Coordinate to, Move move)
    {
        var dx = Math.Abs(to.File - Position.File);
        var dy = Math.Abs(to.Rank - Position.Rank);

        switch (dx)
        {
            case 1:
            {
                if (dy == 2)
                    return move.AsOkResult;
                break;
            }
            case 2:
            {
                if (dy == 1)
                    return move.AsOkResult;
                break;
            }
            default:
                return move.AsOkResult;
        }

        return move.AsInvalidResult("Knights can only move in an L shape");
    }
}