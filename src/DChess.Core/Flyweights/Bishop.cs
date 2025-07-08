namespace DChess.Core.Flyweights;

internal record Bishop : ChessPiece
{
    public Bishop(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public override string PieceName => "Bishop";

    protected override MoveResult ValidatePath(Square to, Game.Game game)
    {
        var move = new Move(Square, to);
        if (!move.IsDiagonal)
            return move.AsInvalidBecause(BishopCanOnlyMoveDiagonally);

        return move.AsOkResult();
    }
}