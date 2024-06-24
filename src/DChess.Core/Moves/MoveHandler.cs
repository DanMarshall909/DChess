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
        if (!game.Board.TryGetAtrributes(move.From, out var props))
            errorHandler.HandleInvalidMove(new MoveResult(move, FromCellDoesNoteContainPiece));

        bool pawnIsPromoted = props.Kind is Kind.Pawn && move.To.Rank is 1 or 8;
        var updatedProperties = pawnIsPromoted
            ? new PieceAttributes(props.Colour, Kind.Queen)
            : props;

        game.Board.RemovePieceAt(move.From);
        game.Board.Place(updatedProperties, move.To);
    }
    
    public static bool HasLegalMoves(Colour colour, Game.Game game) => LegalMoves(colour, game).Any();

    public static IEnumerable<Move> LegalMoves(Colour colour, Game.Game game)
    {
        foreach (var piece in game.FriendlyPieces(colour))
        foreach (var moveValidity in piece.MoveValidities(game))
            if (moveValidity.result.IsValid)
                yield return new Move(piece.Square, moveValidity.to);
    }

    // Gets the best move for the current player
    public Move GetBestMove(Game.Game game, Colour playerColour, int depth = 1)
    {
        var bestMove = NullMove;
        var bestScore = int.MinValue;

        var oppositeColour = playerColour.Invert();
        foreach (var move in LegalMoves(playerColour, game))
        {
            var clonedGame = game.AsClone();
            clonedGame.Move(move);
            int score = GetGameStateScore(clonedGame, playerColour);
            if (score > bestScore)
            {
                bestScore = score;
                bestMove = move;
            }

            if (depth <= 1) continue;
            
            int opponentScore = -GetGameStateScore(clonedGame, oppositeColour);
            
            if (opponentScore <= bestScore) continue;
            
            bestScore = opponentScore;
            bestMove = move;
        }

        return bestMove;
    }

    public int GetGameStateScore(Game.Game game, Colour playerColour)
    {
        var score = 0;
        var oppositeColour = playerColour.Invert();
        
        var status = game.Status(oppositeColour);
        switch (status)
        {
            case Checkmate:
                return int.MinValue;
            case Check:
                score += 10;
                break;
        }

        score += MaterialScore(playerColour, game);

        return score;
    }

    private static int MaterialScore(Colour playerColour, Game.Game clonedGame)
    {
        var opponentColour = playerColour.Invert();
        var result = 0;
        foreach (var (_, piece) in clonedGame.Pieces)
        {
            if (piece.PieceAttributes.Colour == playerColour)
                result += piece.Kind.Value();
            else if (piece.PieceAttributes.Colour == opponentColour)
                result -= piece.Kind.Value();
        }

        return result;
    }
}