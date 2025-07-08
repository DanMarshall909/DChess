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
    PawnsCannotTakeForward,
    PawnsCanOnlySideStep1SquareWhenCapturing,
    PawnsCannotMoveHorizontally,
    QueenCanOnlyMoveDiagonallyOrInAStraightLine,
    RookCanOnlyMoveInAStraightLine,
    WhiteIsInCheckMate,
    CastlingKingHasMoved,
    CastlingRookHasMoved,
    CastlingKingInCheck,
    CastlingKingPassesThroughCheck,
    CastlingSquaresOccupied,
    CastlingKingNotInStartingPosition,
    CastlingRookNotInStartingPosition
}