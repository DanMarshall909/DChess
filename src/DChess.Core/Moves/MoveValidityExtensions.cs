namespace DChess.Core.Moves;

public static class MoveValidityExtensions
{
    public static string Message(this MoveValidity validity)
    {
        return validity switch
        {
            Ok => "OK",
            InvalidMove => "Invalid move",
            BishopCanOnlyMoveDiagonally => "Bishop can only move diagonally",
            KingCanOnlyMove1SquareAtATime => "King can only move 1 square at a time",
            KnightsCanOnlyMoveInAnLShape => "Knights can only move in an L shape",
            PawnsCannotMoveHorizontally => "Pawns cannot move horizontally",
            PawnsCanOnlyMove1Or2SquaresForward => "Pawns can only move 1 or 2 squares forward",
            PawnsCanOnlyMove1SquareDiagonallyWhenCapturing => "Pawns can only move 1 square diagonally when capturing",
            PawnsCanOnlyMove1SquareHorizontallyAndOnlyWhenTakingAPiece =>
                "Pawns can only move 1 square horizontally and only when taking a piece",
            PawnsCanOnlyMove2SquaresForwardFromStartingPosition =>
                "Pawns can only move 2 squares forward from starting position",
            PawnsCanOnlyMoveForward => "Pawns can only move forward",
            PawnsCanOnlySideStepWhenCapturing => "Pawns can only side-step when capturing",
            QueenCanOnlyMoveDiagonallyOrInAStraightLine => "Queen can only move diagonally, or in a straight line",
            RookCanOnlyMoveInAStraightLine => "Rook can only move in a straight line",
            CannotJumpOverOtherPieces => "Cannot jump over other pieces",
            CannotMoveToSameCell => "Cannot move to the same cell",
            CannotCaptureOwnPiece => "Cannot capture own piece",
            CannotMoveIntoCheck => "Cannot move into check",
            WhiteIsInCheckMate => "White is in check mate",
            BlackIsInCheckMate => "Black is in check mate",
            FromCellDoesNoteContainPiece => "From cell does not contain a piece",
            CannotMoveOpponentsPiece => "Cannot move opponent's piece",
            _ => throw new ArgumentOutOfRangeException(nameof(validity), validity, null)
        };
    }
}