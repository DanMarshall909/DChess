namespace DChess.Core.Moves;

public enum MoveValidity
{
    Ok,
    InvalidMove,
    BishopCanOnlyMoveDiagonally,
    BlackIsInCheckMate,
    CannotCaptureOwnPiece,
    CannotJumpOverOtherPieces,
    CannotMoveIntoCheck,
    CannotMoveOpponentsPiece,
    CannotMoveToSameCell,
    FromCellDoesNoteContainPiece,
    KingCanOnlyMove1SquareAtATime,
    KnightsCanOnlyMoveInAnLShape,
    PawnsCanOnlyMove1SquareForwardOr2SquaresForwardOnTheFirstMove,
    PawnsCanOnlyMoveForward,
    PawnsCanOnlySideStep1SquareWhenCapturing,
    PawnsCannotMoveHorizontally,
    QueenCanOnlyMoveDiagonallyOrInAStraightLine,
    RookCanOnlyMoveInAStraightLine,
    WhiteIsInCheckMate
}