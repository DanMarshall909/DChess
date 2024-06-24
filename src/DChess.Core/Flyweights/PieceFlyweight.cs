namespace DChess.Core.Flyweights;

/// <summary>
///     Abstract class for handling piece specific logic.
/// </summary>
public abstract record PieceFlyweight
{
    protected PieceFlyweight(PieceContext pieceContext)
    {
        PieceAttributes = pieceContext.PieceAttributes;
        Coordinate = pieceContext.Coordinate;
    }

    public abstract string PieceName { get; }
    public PieceAttributes PieceAttributes { get; init; }
    public Coordinate Coordinate { get; init; }
    public Colour Colour => PieceAttributes.Colour;
    public Kind Kind => PieceAttributes.Kind;

    public MoveResult CheckMove(Coordinate to, Game.Game game)
    {
        var generalMoveResult = IsGenerallyValid(to, game);

        if (!generalMoveResult.IsValid)
            return generalMoveResult;

        var validity = ValidatePath(to, game);

        var moveResult = validity.IsValid ? generalMoveResult : validity;
        return moveResult;
    }

    private MoveResult IsGenerallyValid(Coordinate to, Game.Game game)
    {
        var move = new Move(Coordinate, to);

        var movedPieceColour = PieceAttributes.Colour;
        if (game.CurrentPlayer != PieceAttributes.Colour)
            return move.AsInvalidBecause(CannotMoveOpponentsPiece);

        if (Coordinate == to)
            return move.AsInvalidBecause(CannotMoveToSameCell);

        if (game.TryGetPiece(to, out var piece) &&
            piece.Colour == movedPieceColour) return move.AsInvalidBecause(CannotCaptureOwnPiece);

        if (this is not IIgnorePathCheck &&
            move.CoordinatesAlongPath.Any(coordinate => game.Board.HasPieceAt(coordinate)))
            return move.AsInvalidBecause(CannotJumpOverOtherPieces);

        if (MovingIntoCheck(movedPieceColour, move, game))
            return move.AsInvalidBecause(CannotMoveIntoCheck);

        return move.AsOkResult();
    }

    private bool MovingIntoCheck(Colour movedPieceColour, Move move, Game.Game game)
    {
        var newGameState = game.AsClone();
        newGameState.Move(move);

        return newGameState.IsInCheck(movedPieceColour);
    }

    public bool CanMoveTo(Coordinate to, Game.Game game, params MoveValidity[] validationsToIgnore)
    {
        var move = new Move(Coordinate, to);
        var val = ValidatePath(to, game.AsClone());
        if (!val.IsValid)
            return false;

        return move.HasPath && !move.IsBlocked(game.Board);
    }

    // todo: restrict this per piece for performance
    public IEnumerable<Coordinate> GetPossibleMoveCoordinates(Game.Game game) => Coordinate.All;

    public IEnumerable<(Coordinate to, MoveResult result)> MoveValidities(Game.Game game)
    {
        // todo: cache?
        var newGameState = game.AsClone();
        return GetPossibleMoveCoordinates(newGameState)
            .Select(to => (to, CheckMove(to, newGameState)));
    }

    protected abstract MoveResult ValidatePath(Coordinate to, Game.Game state);

    public void Deconstruct(out PieceAttributes pieceAttributes, out Coordinate coordinate)
    {
        pieceAttributes = PieceAttributes;
        coordinate = Coordinate;
    }
}

public record struct PieceContext(PieceAttributes PieceAttributes, Coordinate Coordinate)
{
    public PieceContext(Coordinate Coordinate, PieceAttributes PieceAttributes) : this(PieceAttributes, Coordinate) { }
    
};
