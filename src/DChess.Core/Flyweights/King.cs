namespace DChess.Core.Flyweights;

internal record King : ChessPiece, IIgnorePathCheck
{
    public King(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public override string PieceName => "King";

    protected override MoveResult ValidatePath(Square to, Game.Game game)
    {
        var move = new Move(Square, to);

        // Check for castling move (king moves 2 squares horizontally)
        if (IsCastlingMove(move))
        {
            return ValidateCastlingMove(move, game);
        }

        if (!move.IsAdjacent)
            return move.AsInvalidBecause(KingCanOnlyMove1SquareAtATime);

        return move.AsOkResult();
    }

    private bool IsCastlingMove(Move move)
    {
        // Castling is when king moves exactly 2 squares horizontally
        return move.IsHorizontal && Math.Abs(move.To.File - move.From.File) == 2;
    }

    private MoveResult ValidateCastlingMove(Move move, Game.Game game)
    {
        // Check if king is in starting position
        if (!IsKingInStartingPosition())
            return move.AsInvalidBecause(CastlingKingNotInStartingPosition);

        // Check if king has moved
        if (HasKingMoved(game))
            return move.AsInvalidBecause(CastlingKingHasMoved);

        // Check if king is in check
        if (game.IsInCheck(Colour))
            return move.AsInvalidBecause(CastlingKingInCheck);

        // Determine if this is kingside or queenside castling
        bool isKingsideCastling = move.To.File > move.From.File;
        Square rookSquare = GetRookSquare(isKingsideCastling);

        // Check if rook is in starting position
        if (!IsRookInStartingPosition(rookSquare, game))
            return move.AsInvalidBecause(CastlingRookNotInStartingPosition);

        // Check if rook has moved
        if (HasRookMoved(rookSquare, game))
            return move.AsInvalidBecause(CastlingRookHasMoved);

        // Check if squares between king and rook are empty
        if (!AreCastlingSquaresClear(move.From, rookSquare, game))
            return move.AsInvalidBecause(CastlingSquaresOccupied);

        // Check if king passes through check
        if (DoesKingPassThroughCheck(move, game))
            return move.AsInvalidBecause(CastlingKingPassesThroughCheck);

        return move.AsOkResult();
    }

    private bool IsKingInStartingPosition()
    {
        var expectedRank = Colour == White ? 1 : 8;
        return Square.Rank == expectedRank && Square.File == 5; // e-file
    }

    private bool HasKingMoved(Game.Game game)
    {
        var kingStartingSquare = Colour == White ? 
            Game.NamedSquare.e1 : Game.NamedSquare.e8;
        
        return game.MoveHistory.Any(move => move.From == kingStartingSquare);
    }

    private Square GetRookSquare(bool isKingsideCastling)
    {
        var rank = Colour == White ? 1 : 8;
        var file = isKingsideCastling ? 8 : 1; // h-file or a-file
        return new Square((char)('a' + file - 1), (byte)rank);
    }

    private bool IsRookInStartingPosition(Square rookSquare, Game.Game game)
    {
        if (!game.Board.TryGetAtributes(rookSquare, out var pieceAttributes))
            return false;

        return pieceAttributes.Kind == Kind.Rook && pieceAttributes.Colour == Colour;
    }

    private bool HasRookMoved(Square rookSquare, Game.Game game)
    {
        return game.MoveHistory.Any(move => move.From == rookSquare);
    }

    private bool AreCastlingSquaresClear(Square kingSquare, Square rookSquare, Game.Game game)
    {
        var minFile = Math.Min(kingSquare.File, rookSquare.File);
        var maxFile = Math.Max(kingSquare.File, rookSquare.File);
        var rank = kingSquare.Rank;

        // Check all squares between king and rook (exclusive)
        for (var file = minFile + 1; file < maxFile; file++)
        {
            var square = new Square((char)('a' + file - 1), (byte)rank);
            if (game.Board.HasPieceAt(square))
                return false;
        }

        return true;
    }

    private bool DoesKingPassThroughCheck(Move move, Game.Game game)
    {
        // Check if king passes through check during castling
        var direction = move.To.File > move.From.File ? 1 : -1;
        
        // Check the square king passes through (one square toward rook)
        var passingSquare = new Square((char)(move.From.File + direction), move.From.Rank);
        
        // Temporarily move king to the passing square and check if in check
        var tempGame = game.AsClone();
        tempGame.Board.RemovePieceAt(move.From);
        tempGame.Board.Place(PieceAttributes, passingSquare);
        
        return tempGame.IsInCheck(Colour);
    }
}