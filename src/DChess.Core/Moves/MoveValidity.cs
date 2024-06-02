namespace DChess.Core.Moves;

public enum MoveValidity
{
    Ok,
    BishopCanOnlyMoveDiagonally,
    KingCanOnlyMove1SquareAtATime,
    KnightsCanOnlyMoveInAnLShape,
    PawnsCanOnlyMove1Or2SquaresForward,
    PawnsCanOnlyMove1SquareDiagonallyWhenCapturing,
    PawnsCanOnlyMove1SquareHorizontallyAndOnlyWhenTakingAPiece,
    PawnsCanOnlyMove2SquaresForwardFromStartingPosition,
    PawnsCanOnlyMoveForward,
    PawnsCanOnlySideStepWhenCapturing,
    QueenCanOnlyMoveDiagonallyOrInAStraightLine,
    RookCanOnlyMoveInAStraightLine,
    CannotJumpOverOtherPieces,
    CannotMoveToSameCell,
    CannotCaptureOwnPiece,
    CannotMoveIntoCheck
}