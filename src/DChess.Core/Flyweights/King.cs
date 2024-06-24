namespace DChess.Core.Flyweights;

internal record King : PieceFlyweight, IIgnorePathCheck
{
    public King(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public override string PieceName => "King";

    protected override MoveResult ValidatePath(Square to, Game.Game game)
    {
        var move = new Move(Square, to);

        if (!move.IsAdjacent)
            return move.AsInvalidBecause(KingCanOnlyMove1SquareAtATime);

        return move.AsOkResult();
    }
}