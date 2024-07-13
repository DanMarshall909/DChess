namespace DChess.Core.Flyweights;

/// <summary>
///     Abstract class for handling piece specific logic.
/// </summary>
public abstract record PieceFlyweight
{
    protected PieceFlyweight(PieceContext pieceContext)
    {
        PieceAttributes = pieceContext.PieceAttributes;
        Square = pieceContext.Square;
    }

    public override string ToString() => PieceAttributes.ToString();

    public abstract string PieceName { get; }
    public PieceAttributes PieceAttributes { get; }
    public Square Square { get; }
    public Colour Colour => PieceAttributes.Colour;
    public Kind Kind => PieceAttributes.Kind;

    public MoveResult CheckMove(Square to, Game.Game game)
    {
        var generalMoveResult = IsGenerallyValid(to, game);

        if (!generalMoveResult.IsValid)
            return generalMoveResult;

        var validity = ValidatePath(to, game);

        var moveResult = validity.IsValid ? generalMoveResult : validity;
        return moveResult;
    }

    private MoveResult IsGenerallyValid(Square to, Game.Game game)
    {
        var move = new Move(Square, to);

        var movedPieceColour = PieceAttributes.Colour;
        if (game.CurrentPlayer != PieceAttributes.Colour)
            return move.AsInvalidBecause(CannotMoveOpponentsPiece);

        if (Square == to)
            return move.AsInvalidBecause(CannotMoveToSameCell);

        if (game.TryGetPiece(to, out var piece) &&
            piece.Colour == movedPieceColour) return move.AsInvalidBecause(CannotCaptureOwnPiece);

        if (this is not IIgnorePathCheck &&
            move.SquaresAlongPath.Any(square => game.Board.HasPieceAt(square)))
            return move.AsInvalidBecause(CannotJumpOverOtherPieces);

        if (MovingIntoCheck(movedPieceColour, move, game))
            return move.AsInvalidBecause(CannotMoveIntoCheck);

        return move.AsOkResult();
    }

    
    private bool MovingIntoCheck(Colour movedPieceColour, Move move, Game.Game game)
    {
        var newGameState = game.AsClone();
        newGameState.Make(move);

        return newGameState.IsInCheck(movedPieceColour);
    }
    
    public bool CanMoveTo(Square to, Game.Game game)
    {
        var move = new Move(Square, to);
        var val = ValidatePath(to, game.AsClone());
        if (!val.IsValid)
            return false;

        return !move.IsBlocked(game.Board);
    }

    public IEnumerable<(Square to, MoveResult result)> MoveValidities(Game.Game game)
    {
        // todo: cache?
        var newGameState = game.AsClone();
        return SquaresToCheckForMoveValidMoves()
            .Select(to => (to, CheckMove(to, newGameState)));
    }

    // todo: We don't need to check very square dependent on piece. Optimise this to check only those that need to be.
    private IEnumerable<Square> SquaresToCheckForMoveValidMoves() => Square.All;

    protected abstract MoveResult ValidatePath(Square to, Game.Game state);

    public void Deconstruct(out PieceAttributes pieceAttributes, out Square square)
    {
        pieceAttributes = PieceAttributes;
        square = Square;
    }
}