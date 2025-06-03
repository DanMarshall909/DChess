namespace DChess.Core.Flyweights;

public record Pawn : ChessPiece, IIgnorePathCheck
{
    public Pawn(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public override string PieceName => "Pawn";

    protected override MoveResult ValidatePath(Square to, Game.Game game)
    {
        var move = new Move(Square, to);

        bool isFirstMove = (Square.Rank == 2 && Colour == White) ||
                           (Square.Rank == 7 && Colour == Black);

        if (move.IsVertical)
        {
            if (move.Distance.Total > (isFirstMove ? 2 : 1))
                return move.AsInvalidBecause(PawnsCanOnlyMove1SquareForwardOr2SquaresForwardOnTheFirstMove);

            if (move.IsBackwards(Colour))
                return move.AsInvalidBecause(PawnsCanOnlyMoveForward);

            // Check if attempting to capture forward
            if (game.Board.HasPieceAt(to))
                return move.AsInvalidBecause(PawnsCannotTakeForward);
        }
        else if (move.IsDiagonal)
        {
            if (!game.Board.HasPieceAt(to) || !move.IsAdjacent)
                return move.AsInvalidBecause(PawnsCanOnlySideStep1SquareWhenCapturing);
        }
        else if (move.IsHorizontal)
        {
            return move.AsInvalidBecause(PawnsCannotMoveHorizontally);
        }
        else
        {
            return move.AsInvalidBecause(PawnsCanOnlySideStep1SquareWhenCapturing);
        }

        return move.AsOkResult();
    }
}