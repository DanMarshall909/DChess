using DChess.Core.Game;
using DChess.Core.Moves;
using static DChess.Core.Moves.MoveValidity;

namespace DChess.Core.Pieces;

internal record Rook : Piece
{
    public Rook(Arguments arguments) : base(arguments)
    {
    }

    public override string PieceName => "Rook";

    protected override MoveResult ValidatePath(Coordinate to, Game.Game game)
    {
        var move = new Move(Coordinate, to);

        if (!(move.IsHorizontal || move.IsVertical))
            return move.AsInvalidBecause(RookCanOnlyMoveInAStraightLine);

        return move.AsOkResult();
    }
}