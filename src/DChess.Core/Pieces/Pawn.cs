﻿using DChess.Core.Game;
using DChess.Core.Moves;

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
        var move = new Move(Coordinate, to);

        if (move.IsHorizontal)
            return move.InvalidResult(PawnsCannotMoveHorizontally);

        if (move.IsBackwards(Colour))
            return move.InvalidResult(PawnsCanOnlyMoveForward);

        if (move.IsDiagonal)
        {
            if (!Game.GameState.HasPieceAt(to))
                return move.InvalidResult(PawnsCanOnlySideStepWhenCapturing);

            return move.Distance.Horizontal == 1
                ? move.OkResult()
                : move.InvalidResult(PawnsCanOnlySideStepWhenCapturing);
        }

        int verticalDistance = move.Distance.Vertical;

        bool isFirstMove = (Coordinate.Rank == 2 && Colour == White) ||
                           (Coordinate.Rank == 7 && Colour == Black);

        if (verticalDistance > 2)
            return move.InvalidResult(PawnsCanOnlyMove1Or2SquaresForward);

        if (move.Distance.Horizontal > 2)
            return move.InvalidResult(PawnsCanOnlyMove1SquareHorizontallyAndOnlyWhenTakingAPiece);

        if (verticalDistance == 2 && !isFirstMove)
            return move.InvalidResult(PawnsCanOnlyMove2SquaresForwardFromStartingPosition);

        return move.OkResult();
    }
}