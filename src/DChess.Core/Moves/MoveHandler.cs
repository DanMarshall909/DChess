using DChess.Core.Game;

namespace DChess.Core.Moves;

public class MoveHandler(IErrorHandler errorHandler)
{
    public void Make(Move move, GameState gameState, bool force = false)
    {
        if (!gameState.TryGetProperties(move.From, out var props))
            errorHandler.HandleInvalidMove(new MoveResult(move, MoveValidity.FromCellDoesNoteContainPiece));
        
        bool pawnIsPromoted = props.Type == PieceType.Pawn && (move.To.Rank == 1 || move.To.Rank == 8);
        var updatedProperties = pawnIsPromoted
            ? new Properties(PieceType.Queen, props.Colour)
            : props;
        
        gameState.BoardState.RemovePieceAt(move.From);
        gameState.BoardState.SetPiece(move.To, updatedProperties);
        
        gameState.CurrentPlayer = gameState.CurrentPlayer == White ? Black : White;
    }
    
    
}