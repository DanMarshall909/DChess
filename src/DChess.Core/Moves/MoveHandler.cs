namespace DChess.Core.Moves;

public class MoveHandler
{
    private readonly int _maxAllowableDepth;
    private readonly IErrorHandler _errorHandler;

    public MoveHandler(IErrorHandler errorHandler, int maxAllowableDepth)
    {
        _errorHandler = errorHandler;
        _maxAllowableDepth = maxAllowableDepth;
    }

    public void Make(Move move, Game.Game game)
    {
        ApplyMove(move, game);
        game.CurrentPlayer = game.CurrentPlayer.Invert();
    }

    private void ApplyMove(Move move, Game.Game game)
    {
        if (!game.Board.TryGetAtributes(move.From, out var props))
            _errorHandler.HandleInvalidMove(new MoveResult(move, FromCellDoesNoteContainPiece));

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
        // function minimax(board, depth, isMaximizingPlayer):
        // if current board state is a terminal state :
        //     return value of the board
        //
        // if isMaximizingPlayer :
        //     bestVal = -INFINITY 
        //     for each move in board :
        //         value = minimax(board, depth+1, false)
        //         bestVal = max( bestVal, value) 
        //     return bestVal
        //
        // else :
        //     bestVal = +INFINITY 
        //     for each move in board :
        //         value = minimax(board, depth+1, true)
        //         bestVal = min( bestVal, value) 
        //     return bestVal

        var bestMove = NullMove;

        var bestVal = int.MinValue;
        foreach (var move in LegalMoves(playerColour, game))
        {
            var clonedGame = game.AsClone();
            clonedGame.Move(move);
            var value = Minimax(clonedGame, depth, false);
            if (value > bestVal)
            {
                bestVal = value;
                bestMove = move;
            }
        }

        return bestMove;
    }

    int Minimax(Game.Game game, int depth, bool isMaximizingPlayer)
    {
        var bestVal = isMaximizingPlayer ? int.MinValue : int.MaxValue;

        if (game.Status(game.Opponent) == Checkmate)
            return bestVal;

        if (depth == 0)
            return GetGameStateScore(game, game.CurrentPlayer);

        if (isMaximizingPlayer)
        {
            foreach (var move in LegalMoves(game.CurrentPlayer, game))
            {
                var clonedGame = game.AsClone();
                clonedGame.Move(move);
                var value = Minimax(clonedGame, depth - 1, false);
                bestVal = Math.Max(bestVal, value);
            }

            return bestVal;
        }
        else
        {
            foreach (var move in LegalMoves(game.CurrentPlayer, game))
            {
                var clonedGame = game.AsClone();
                clonedGame.Move(move);
                var value = Minimax(clonedGame, depth - 1, true);
                bestVal = Math.Min(bestVal, value);
            }

            return bestVal;
        }
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