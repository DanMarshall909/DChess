using DChess.Core.Game;
using DChess.Core.Moves;
using static DChess.Core.Moves.MoveValidity;

namespace DChess.Core.Pieces;

internal record King : PieceFlyweight, IIgnorePathCheck
{
    public King(PieceContext pieceContext) : base(pieceContext)
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