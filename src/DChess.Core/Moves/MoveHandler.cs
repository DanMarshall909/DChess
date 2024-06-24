using DChess.Core.Errors;
using DChess.Core.Game;
using static DChess.Core.Moves.Move;

namespace DChess.Core.Moves;

public class MoveHandler(IErrorHandler errorHandler)
{
    public void Make(Move move, Game.Game game)
    {
        ApplyMove(move, game);
        game.CurrentPlayer = game.CurrentPlayer.Invert();
    }

    private void ApplyMove(Move move, Game.Game game)
    {
        if (!game.Board.TryGetProperties(move.From, out var props))
            errorHandler.HandleInvalidMove(new MoveResult(move, MoveValidity.FromCellDoesNoteContainPiece));

        bool pawnIsPromoted = props.Type == PieceType.Pawn && (move.To.Rank == 1 || move.To.Rank == 8);
        var updatedProperties = pawnIsPromoted
            ? new Properties(PieceType.Queen, props.Colour)
            : props;

        game.Board.RemovePieceAt(move.From);
        game.Board.Place(updatedProperties, move.To);
    }

    // Gets the best move for the current player
    public Move GetBestMove(Game.Game game, Colour playerColour, int depth = 1)
    {
        var bestMove = NullMove;
        var bestScore = int.MinValue;

        var oppositeColour = playerColour.Invert();
        foreach (var move in game.GetLegalMoves(playerColour))
        {
            var clonedGame = game.AsClone();
            clonedGame.Move(move);
            int score = GetGameStateScore(clonedGame, playerColour);
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }

            if (depth > 1)
            {
                int opponentScore = -GetGameStateScore(clonedGame, oppositeColour);
                if (opponentScore > bestScore)
                {
                    bestScore = opponentScore;
                    bestMove = move;
                }
            }
        }

        return bestMove;
    }

    public int GetGameStateScore(Game.Game game, Colour playerColour)
    {
        var score = 0;
        var oppositeColour = playerColour.Invert();
        
        var status = game.Status(oppositeColour);
        if (status == Checkmate)
            return int.MinValue;
        if (status == Check)
            score += 10;

        score += MaterialScore(playerColour, game);

        return score;
    }

    private static int MaterialScore(Colour playerColour, Game.Game clonedGame)
    {
        var opponentColour = playerColour.Invert();
        var result = 0;
        foreach (var (_, piece) in clonedGame.Pieces)
        {
            if (piece.Properties.Colour == playerColour)
                result += piece.Type.Value();
            else if (piece.Properties.Colour == opponentColour)
                result -= piece.Type.Value();
        }

        return result;
    }
}