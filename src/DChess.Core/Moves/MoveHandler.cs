using DChess.Core.Game;

namespace DChess.Core.Moves;

public class MoveHandler(IErrorHandler errorHandler)
{
    public void Make(Move move, GameState gameState, bool force = false)
    {
        if (!gameState.Board.TryGetProperties(move.From, out var props))
            errorHandler.HandleInvalidMove(new MoveResult(move, MoveValidity.FromCellDoesNoteContainPiece));

        bool pawnIsPromoted = props.Type == PieceType.Pawn && (move.To.Rank == 1 || move.To.Rank == 8);
        var updatedProperties = pawnIsPromoted
            ? new Properties(PieceType.Queen, props.Colour)
            : props;

        gameState.Board.RemovePieceAt(move.From);
        gameState.Board.Place(updatedProperties, move.To);

        gameState.CurrentPlayer = gameState.CurrentPlayer == White ? Black : White;
    }

    // Gets the best move for the current player
    public Move GetBestMove(Colour colour, GameState gameState)
    {
        var bestMove = new Move(Coordinate.None, Coordinate.None);
        var bestScore = int.MinValue;

        foreach (var legalMove in gameState.GetLegalMoves(colour))
        {
            int score = GetGameStateScore(legalMove, gameState, colour);
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = legalMove;
            }
        }
        
        return bestMove;
    }

    public int GetGameStateScore(Move move, GameState gameState, Colour colour)
    {
        if (gameState.Status(colour.Invert()) == Checkmate)
            return int.MaxValue;

        var score = 0;
        var props = gameState.Board[move.To];
        if (props != Properties.None)
            score += props.Type switch
            {
                PieceType.Pawn => 1,
                PieceType.Knight => 3,
                PieceType.Bishop => 3,
                PieceType.Rook => 5,
                PieceType.Queen => 9,
                _ => 0
            };

        if (gameState.IsInCheck(gameState.CurrentPlayer))
            score += 3;

        return score;
    }
}