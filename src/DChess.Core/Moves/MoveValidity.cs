namespace DChess.Core.Moves;

public enum MoveValidity
{
    Ok,
    InvalidMove,
    BishopCanOnlyMoveDiagonally,
    KingCanOnlyMove1SquareAtATime,
    KnightsCanOnlyMoveInAnLShape,
    PawnsCannotMoveHorizontally,
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
    CannotMoveIntoCheck,
    WhiteIsInCheckMate,
    BlackIsInCheckMate,
    FromCellDoesNoteContainPiece
}