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

    protected override MoveResult ValidatePath(Coordinate to, Game.Game game)
    {
        var move = new Move(Coordinate, to);

        if (!move.IsAdjacent)
            return move.AsInvalidBecause(KingCanOnlyMove1SquareAtATime);

        return move.AsOkResult();
    }
}