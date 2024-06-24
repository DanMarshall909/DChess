using DChess.Core.Game;
using DChess.Core.Moves;
using static DChess.Core.Moves.MoveValidity;

namespace DChess.Core.Pieces;

public record Pawn : PieceFlyweight, IIgnorePathCheck
{
    public Pawn(PieceContext pieceContext)
        : base(pieceContext)
    {
    }

    public override string PieceName => "Pawn";

    protected override MoveResult ValidatePath(Coordinate to, Game.Game game)
    {
        var move = new Move(Coordinate, to);

        if (move.IsHorizontal)
            return move.AsInvalidBecause(PawnsCannotMoveHorizontally);

        if (move.IsBackwards(Colour))
            return move.AsInvalidBecause(PawnsCanOnlyMoveForward);

        if (move.IsDiagonal)
        {
            if (!game.Board.HasPieceAt(to))
                return move.AsInvalidBecause(PawnsCanOnlySideStepWhenCapturing);

            return move.Distance.Horizontal == 1
                ? move.AsOkResult()
                : move.AsInvalidBecause(PawnsCanOnlySideStepWhenCapturing);
        }

        int verticalDistance = move.Distance.Vertical;

        bool isFirstMove = (Coordinate.Rank == 2 && Colour == White) ||
                           (Coordinate.Rank == 7 && Colour == Black);

        if (verticalDistance > 2)
            return move.AsInvalidBecause(PawnsCanOnlyMove1Or2SquaresForward);

        if (move.Distance.Horizontal > 2)
            return move.AsInvalidBecause(PawnsCanOnlyMove1SquareHorizontallyAndOnlyWhenTakingAPiece);

        if (verticalDistance == 2 && !isFirstMove)
            return move.AsInvalidBecause(PawnsCanOnlyMove2SquaresForwardFromStartingPosition);

        return move.AsOkResult();
    }
}