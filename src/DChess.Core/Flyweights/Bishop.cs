namespace DChess.Core.Pieces;

internal record Bishop : PieceFlyweight
{
    public Bishop(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public override string PieceName => "Bishop";

    protected override MoveResult ValidatePath(Coordinate to, Game.Game game)
    {
        var move = new Move(Coordinate, to);
        if (!move.IsDiagonal)
            return move.AsInvalidBecause(BishopCanOnlyMoveDiagonally);

        return move.AsOkResult();
    }
}