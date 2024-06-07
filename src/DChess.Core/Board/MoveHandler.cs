using DChess.Core.Exceptions;
using DChess.Core.Moves;

namespace DChess.Core.Board;

public class MoveHandler(Board board)
{
    public void Make(Move move)
    {
        if (!board.TryGetProperties(move.From, out var fromPiece))
            throw new InvalidMoveException(move, $"No piece exists at {move.From}");

        bool pawnIsPromoted = (fromPiece.Type == PieceType.Pawn && move.To.File == 'a') || move.To.File == 'h';
        var toPiece = pawnIsPromoted
            ? new Properties(PieceType.Queen, fromPiece.Colour)
            : fromPiece;

        board.RemovePieceAt(move.From);
        board.SetPiece(move.To, toPiece);
    }
}