using DChess.Core.Moves;

namespace DChess.Core.Pieces;

internal record Knight : Piece, IIgnorePathCheck
{
    public Knight(Arguments arguments) : base(arguments)
    {
    }

    public override string PieceName => "Knight";

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Current, to);
        return MoveMustBeLShape(to, move);
    }

    private MoveResult MoveMustBeLShape(Coordinate to, Move move)
    {
        int dx = Math.Abs(to.File - Current.File);
        int dy = Math.Abs(to.Rank - Current.Rank);

        return dx switch
        {
            1 when dy == 2 => move.AsOkResult,
            2 when dy == 1 => move.AsOkResult,
            _ => move.AsInvalidResult("Knights can only move in an L shape")
        };
    }
}