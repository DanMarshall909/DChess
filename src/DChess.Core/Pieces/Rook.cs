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

    protected override MoveResult ValidateMovement(Coordinate to, GameState gameState)
    {
        var move = new Move(Coordinate, to);

        if (!(move.IsHorizontal || move.IsVertical))
            return move.InvalidResult(RookCanOnlyMoveInAStraightLine);

        return move.OkResult();
    }
}