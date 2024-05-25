namespace DChess.Core;

public record Pawn : Piece
{
    public Pawn(PieceStruct pieceStruct, Coordinate coordinate, Board board) 
        : base(pieceStruct, coordinate, board)
    {
    }

    protected override MoveResult ValidateMove(Move move)
    {
        if (move.To.Rank != move.From.Rank)
            return new(false, move, "Pawns can only move forward");

        return new(true, move, string.Empty);
    }
}