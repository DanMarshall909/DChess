﻿using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public record Pawn : Piece, IIgnorePathCheck
{
    public Pawn(Arguments arguments)
        : base(arguments)
    {
    }

    public override string PieceName => "Pawn";

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Current, to);

        if (move.IsBackwards(Colour))
            return move.InvalidResult(PawnsCanOnlyMoveForward);

        if (move.IsDiagonal)
        {
            if (!Board.HasPieceAt(to))
                return move.InvalidResult(PawnsCanOnlySideStepWhenCapturing);

            return move.HorizontalDistance == 1
                ? move.OkResult()
                : move.InvalidResult(PawnsCanOnlySideStepWhenCapturing);
        }

        int verticalDistance = move.VerticalDistance;

        bool isFirstMove = (Current.File != 2 && Colour == White) ||
                           (Current.File != 7 && Colour == Black);

        if (verticalDistance > 2)
            return move.InvalidResult(PawnsCanOnlyMove1Or2SquaresForward);

        if (move.HorizontalDistance > 2)
            return move.InvalidResult(PawnsCanOnlyMove1SquareHorizontallyAndOnlyWhenTakingAPiece);
        
        if (verticalDistance == 2 && !isFirstMove)
            return move.InvalidResult(PawnsCanOnlyMove2SquaresForwardFromStartingPosition);

        return move.OkResult();
    }
}