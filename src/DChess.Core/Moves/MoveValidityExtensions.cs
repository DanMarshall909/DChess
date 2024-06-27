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
            PawnsCanOnlyMove1SquareForwardOr2SquaresForwardOnTheFirstMove => "Pawns can only move 1 square forward or 2 squares on the first move",
            PawnsCanOnlyMoveForward => "Pawns can only move forward",
            PawnsCanOnlySideStep1SquareWhenCapturing => "Pawns can only side step one square when capturing",
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