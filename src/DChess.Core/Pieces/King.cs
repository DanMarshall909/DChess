using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

internal record King : Piece, IIgnorePathCheck
{
    public King(Arguments arguments) : base(arguments)
    {
    }

    public override string PieceName => "King";

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Coordinate, to);

        if (!move.IsAdjacent)
            return move.InvalidResult(KingCanOnlyMove1SquareAtATime);

        return move.OkResult();
    }
}