using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

internal record Rook : Piece
{
    public Rook(Arguments arguments) : base(arguments)
    {
    }

    public override string PieceName => "Rook";

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Coordinate, to);

        if (!(move.IsHorizontal || move.IsVertical))
            return move.InvalidResult(RookCanOnlyMoveInAStraightLine);

        return move.OkResult();
    }
}