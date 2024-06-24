namespace DChess.Core.Flyweights;

internal record Knight : PieceFlyweight, IIgnorePathCheck
{
    public Knight(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public override string PieceName => "Knight";

    protected override MoveResult ValidatePath(Square to, Game.Game game)
    {
        var move = new Move(Square, to);
        return MoveMustBeLShape(to, move);
    }

    private MoveResult MoveMustBeLShape(Square to, Move move)
    {
        int dx = Math.Abs(to.File - Square.File);
        int dy = Math.Abs(to.Rank - Square.Rank);

        return dx switch
        {
            1 when dy == 2 => move.AsOkResult(),
            2 when dy == 1 => move.AsOkResult(),
            _ => move.AsInvalidBecause(KnightsCanOnlyMoveInAnLShape)
        };
    }
}