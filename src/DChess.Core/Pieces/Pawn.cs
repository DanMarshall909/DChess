using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public record Pawn : Piece
{
    public Pawn(Arguments arguments)
        : base(arguments)
    {
    }

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Position, to);

        if ((Colour == White && to.File <= Position.File)
            || (Colour == Black && to.File >= Position.File))
            return move.AsInvalidResult("Pawns can only move forward");

        bool sameFile = to.Rank == Position.Rank;
        if (!sameFile)
        {
            if (!Board.Pieces.TryGetValue(to, out var piece))
                return move.AsInvalidResult("Pawns can only side-step when capturing");

            var diff = (byte)Math.Abs(to.File - Position.File);
            return diff == 1
                ? move.AsOkResult
                : move.AsInvalidResult("Pawns can only move 1 square diagonally when capturing");
        }


        var absFileDifference = (byte)Math.Abs(to.File - Position.File);
        return absFileDifference switch
        {
            > 2 => move.AsInvalidResult("Pawns can only move 1 or 2 squares forward"),
            2 when Position.File != 2 && Position.File != 7 => move.AsInvalidResult(
                "Pawns can only move 2 squares forward from starting position"),
            _ => new MoveResult(true, new Move(Position, to), string.Empty)
        };
    }
}