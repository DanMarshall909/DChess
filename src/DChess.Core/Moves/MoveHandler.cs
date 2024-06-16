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

    // Gets the best move for the current player
    public Move GetBestMove(Colour colour, GameState gameState)
    {
        var bestMove = new Move(Coordinate.None, Coordinate.None);
        var bestScore = int.MinValue;

        foreach (var piece in gameState.FriendlyPieces(colour))
        foreach (var move in piece.LegalMoves(gameState))
        {
            int score = GetGameStateScore(move, gameState, colour);
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }

        return bestMove;
    }

    private int GetGameStateScore(Move move, GameState gameState, Colour colour)
    {
        if (gameState.Status(colour.Invert()) == Checkmate)
            return int.MaxValue;

        var score = 0;
        var props = gameState.BoardState[move.To];
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