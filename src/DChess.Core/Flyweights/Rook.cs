namespace DChess.Core.Pieces;

internal record Rook : PieceFlyweight
{
    public Rook(PieceContext pieceContext) : base(pieceContext)
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