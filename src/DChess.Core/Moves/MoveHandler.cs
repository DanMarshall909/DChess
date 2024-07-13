using DChess.Core.Flyweights;

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
        if (!game.Board.TryGetAtributes(move.From, out var props))
            _errorHandler.HandleInvalidMove(new MoveResult(move, FromCellDoesNoteContainPiece));

        bool pawnIsPromoted = props.Kind is Kind.Pawn && move.To.Rank is 1 or 8;
        var updatedProperties = pawnIsPromoted
            ? new PieceAttributes(props.Colour, Kind.Queen)
            : props;

        game.Board.RemovePieceAt(move.From);
        game.Board.Place(updatedProperties, move.To);
        game.CurrentPlayer = game.CurrentPlayer.Invert();
    }

    public static bool HasLegalMoves(Colour colour, Game.Game game)
    {
        var legalMoves = LegalMoves(colour, game).ToList();
        return legalMoves.Any();
    }

    public static IEnumerable<Move> LegalMoves(Colour colour, Game.Game game)
    {
        var friendlyPieces = game.FriendlyPieces(colour).ToList();

        List<Move> validMoves = [];
        foreach (var piece in friendlyPieces)
        {
            // get all valid moves for the piece
            validMoves.AddRange(GetValidMoves(piece, game));
            
            // var moveValidities = piece.MoveValidities(game);
            // foreach (var moveValidity in moveValidities)
            //     if (moveValidity.result.IsValid)
            //         moves.Add(new Move(piece.Square, moveValidity.to));
        }

        return validMoves;
    }

    private static List<Move> GetValidMoves(PieceFlyweight piece, Game.Game game)
    {
        var validMoves = new List<Move>();
        for (var f = 0; f < 8; f++)
        for (var r = 0; r < 8; r++)
        {
            var to = Square.FromZeroOffset(f, r);
            var move = new Move(piece.Square, to);
            if (piece.CheckMove(to, game).IsValid)
                validMoves.Add(move);
        }

        return validMoves;
    }


    public int EvaluatePosition(Game.Game game, int depth, int alpha, int beta, bool isMaximizingPlayer = true)
    {
        if (game.Status(game.CurrentPlayer) == Checkmate)
            return isMaximizingPlayer ? int.MaxValue : int.MinValue;

        if (depth == 0)
            return GetGameStateScore(game, game.CurrentPlayer);
 
        if (isMaximizingPlayer)
        {
            int maxEval = int.MinValue;
            foreach (var move in LegalMoves(game.CurrentPlayer, game))
            {
                game.Make(move);
                int eval = EvaluatePosition(game, depth - 1, alpha, beta);
                game.UndoLastMove();
                maxEval = Math.Max(maxEval, eval);
                alpha = Math.Max(alpha, eval);
                if (beta <= alpha)
                    break;
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            foreach (var move in LegalMoves(game.CurrentPlayer, game))
            {
                game.Make(move);
                int eval = EvaluatePosition(game, depth - 1, alpha, beta);
                game.UndoLastMove();
                minEval = Math.Min(minEval, eval);
                beta = Math.Min(beta, eval);
                if (beta <= alpha)
                    break;
            }
            return minEval;
        }
    }

    public static int GetGameStateScore(Game.Game game, Colour playerColour)
    {
        var score = 0;

        var status = game.Status(playerColour);
        score += status switch
        {
            Stalemate => 0,
            Checkmate => -1_000_000,
            Check => 10,
            InPlay => 0,
            _ => 0
        };

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

    public Move GetBestMove(Game.Game game, Colour colour, int maxDepth = 3)
    {
        maxDepth = Math.Min(maxDepth, _maxAllowableDepth);
        var bestMove = NullMove;
        var bestScore = int.MinValue;
        var legalMoves = LegalMoves(colour, game);
        foreach (var move in legalMoves)
        {
            var clone = game.AsClone();
            clone.Make(move);
            int score = EvaluatePosition(clone, _maxAllowableDepth, int.MinValue, int.MaxValue, false);
            clone.UndoLastMove();

            if (score >= bestScore)
            {
                bestScore = score;
                bestMove = move;
            }
        }

        return bestMove;
    }
}