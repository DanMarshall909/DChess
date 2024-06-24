namespace DChess.Core.Flyweights;

public record Pawn : PieceFlyweight, IIgnorePathCheck
{
    public Pawn(PieceContext pieceContext)
        : base(pieceContext)
    {
    }

    public override string PieceName => "Pawn";

    protected override MoveResult ValidatePath(Square to, Game.Game game)
    {
        var move = new Move(Square, to);

        if (IsMoveHorizontal(move))
            return move.AsInvalidBecause(PawnsCannotMoveHorizontally);

        if (IsMoveBackwards(move))
            return move.AsInvalidBecause(PawnsCanOnlyMoveForward);

        if (IsMoveDiagonal(move))
            return ValidateDiagonalMove(move, game);

        return ValidateStraightMove(move);
    }

    private bool IsMoveHorizontal(Move move)
    {
        return move.IsHorizontal;
    }

    private bool IsMoveBackwards(Move move)
    {
        return move.IsBackwards(Colour);
    }

    private bool IsMoveDiagonal(Move move)
    {
        return move.IsDiagonal;
    }

    private MoveResult ValidateDiagonalMove(Move move, Game.Game game)
    {
        if (!game.Board.HasPieceAt(move.To))
            return move.AsInvalidBecause(PawnsCanOnlySideStepWhenCapturing);

        return move.Distance.Horizontal == 1
            ? move.AsOkResult()
            : move.AsInvalidBecause(PawnsCanOnlySideStepWhenCapturing);
    }

    private MoveResult ValidateStraightMove(Move move)
    {
        int verticalDistance = move.Distance.Vertical;

        bool isFirstMove = (Square.Rank == 2 && Colour == White) ||
                           (Square.Rank == 7 && Colour == Black);

        if (verticalDistance > 2)
            return move.AsInvalidBecause(PawnsCanOnlyMove1Or2SquaresForward);

        if (move.Distance.Horizontal > 2)
            return move.AsInvalidBecause(PawnsCanOnlyMove1SquareHorizontallyAndOnlyWhenTakingAPiece);

        if (verticalDistance == 2 && !isFirstMove)
            return move.AsInvalidBecause(PawnsCanOnlyMove2SquaresForwardFromStartingPosition);

        return move.AsOkResult();
    }
}