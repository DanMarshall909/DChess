using DChess.Core.Board;
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
        int dx = Math.Abs(to.File - Position.File);
        int dy = Math.Abs(to.Rank - Position.Rank);

        return dx switch
        {
            1 when dy == 2 => move.AsOkResult,
            2 when dy == 1 => move.AsOkResult,
            _ => move.AsInvalidResult("Knights can only move in an L shape")
        };
    }
}