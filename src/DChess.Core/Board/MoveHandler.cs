using DChess.Core.Exceptions;
using DChess.Core.Moves;

namespace DChess.Core.Board;

public class MoveHandler(Game game)
{
    public void Make(Move move)
    {
        if (!game.GameState.TryGetProperties(move.From, out var fromPiece))
            throw new InvalidMoveException(move, $"No piece exists at {move.From}");

        bool pawnIsPromoted = (fromPiece.Type == PieceType.Pawn && move.To.File == 'a') || move.To.File == 'h';
        var toPiece = pawnIsPromoted
            ? new Properties(PieceType.Queen, fromPiece.Colour)
            : fromPiece;

        game.GameState.RemovePieceAt(move.From);
        game.GameState.SetPiece(move.To, toPiece);
    }
}