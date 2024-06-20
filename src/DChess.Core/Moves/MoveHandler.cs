using DChess.Core.Errors;
using DChess.Core.Game;

namespace DChess.Core.Moves;

public class MoveHandler(IErrorHandler errorHandler)
{
    public void Make(Move move, Game.Game game, bool force = false)
    {
        if (!game.Board.TryGetProperties(move.From, out var props))
            errorHandler.HandleInvalidMove(new MoveResult(move, MoveValidity.FromCellDoesNoteContainPiece));

        bool pawnIsPromoted = props.Type == PieceType.Pawn && (move.To.Rank == 1 || move.To.Rank == 8);
        var updatedProperties = pawnIsPromoted
            ? new Properties(PieceType.Queen, props.Colour)
            : props;

        game.Board.RemovePieceAt(move.From);
        game.Board.Place(updatedProperties, move.To);

        game.CurrentPlayer = game.CurrentPlayer.Invert();
    }

    // Gets the best move for the current player
    public Move GetBestMove(Colour colour, Game.Game game)
    {
        var bestMove = new Move(Coordinate.None, Coordinate.None);
        var bestScore = int.MinValue;

        foreach (var legalMove in game.GetLegalMoves(colour))
        {
            int score = GetGameStateScore(legalMove, game, colour);
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = legalMove;
            }
        }
        
        return bestMove;
    }

    public int GetGameStateScore(Move move, Game.Game game, Colour colour)
    {
        if (game.Status(colour.Invert()) == Checkmate)
            return int.MaxValue;

        var score = 0;
        var props = game.Board[move.To];
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

        if (game.IsInCheck(game.CurrentPlayer))
            score += 3;

        return score;
    }
}