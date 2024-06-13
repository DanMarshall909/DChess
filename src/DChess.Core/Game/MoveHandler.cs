using DChess.Core.Exceptions;
using DChess.Core.Moves;

namespace DChess.Core.Game;

public class MoveHandler(IErrorHandler errorHandler)
{
    public void Make(Move move, GameState gameState, bool force = false)
    {
        if (!gameState.TryGetProperties(move.From, out var fromPiece))
            throw new InvalidMoveException(move, $"No piece exists at {move.From}");

        bool pawnIsPromoted = (fromPiece.Type == PieceType.Pawn && move.To.File == 'a') || move.To.File == 'h';
        var toPiece = pawnIsPromoted
            ? new Properties(PieceType.Queen, fromPiece.Colour)
            : fromPiece;

        gameState.RemovePieceAt(move.From);
        gameState.SetPiece(move.To, toPiece);
    }
}