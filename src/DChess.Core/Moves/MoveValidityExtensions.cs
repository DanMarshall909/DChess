namespace DChess.Core.Moves;

public static class MoveValidityExtensions
{
    public static MoveResult OkResult(this Move move) => new(move, Ok);
    public static MoveResult InvalidResult(this Move move, MoveValidity invalidReason) => new(move, invalidReason);
    
    public static string Message(this MoveValidity validity)
    {
        return validity switch
        {
            Ok => "OK",
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
            _ => throw new ArgumentOutOfRangeException(nameof(validity), validity, null)
        };
    }
}