namespace DChess.Core.Flyweights;

public record Queen : ChessPiece
{
    public Queen(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public override string PieceName => "Queen";

    protected override MoveResult ValidatePath(Square to, Game.Game game)
    {
        var move = new Move(Square, to);

        return move.IsDiagonal || move.IsVertical || move.IsHorizontal
            ? move.AsOkResult()
            : move.AsInvalidBecause(QueenCanOnlyMoveDiagonallyOrInAStraightLine);
    }
}