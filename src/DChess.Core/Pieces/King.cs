using DChess.Core.Game;
using DChess.Core.Moves;
using static DChess.Core.Moves.MoveValidity;

namespace DChess.Core.Pieces;

internal record King : Piece, IIgnorePathCheck
{
    public King(Arguments arguments) : base(arguments)
    {
    }

    public override string PieceName => "King";

    protected override MoveResult ValidateMovement(Coordinate to, GameState gameState)
    {
        var move = new Move(Coordinate, to);

        if (!move.IsAdjacent)
            return move.InvalidResult(KingCanOnlyMove1SquareAtATime);

        return move.OkResult();
    }
}