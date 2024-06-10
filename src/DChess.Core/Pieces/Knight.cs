using DChess.Core.Game;
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
        var move = new Move(Coordinate, to);
        return MoveMustBeLShape(to, move);
    }

    private MoveResult MoveMustBeLShape(Coordinate to, Move move)
    {
        int dx = Math.Abs(to.File - Coordinate.File);
        int dy = Math.Abs(to.Rank - Coordinate.Rank);

        return dx switch
        {
            1 when dy == 2 => move.OkResult(),
            2 when dy == 1 => move.OkResult(),
            _ => move.InvalidResult(KnightsCanOnlyMoveInAnLShape)
        };
    }
}