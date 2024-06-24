namespace DChess.Core.Flyweights;

internal record Rook : PieceFlyweight
{
    public Rook(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public override string PieceName => "Rook";

    protected override MoveResult ValidatePath(Square to, Game.Game game)
    {
        var move = new Move(Square, to);

        if (!(move.IsHorizontal || move.IsVertical))
            return move.AsInvalidBecause(RookCanOnlyMoveInAStraightLine);

        return move.AsOkResult();
    }
}