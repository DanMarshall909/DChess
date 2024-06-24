using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

/// <summary>
///     Abstract class for handling piece specific logic.
/// </summary>
public abstract record Piece
{
    protected Piece(PiecePosition piecePosition)
    {
        Properties = piecePosition.Properties;
        Coordinate = piecePosition.Coordinate;
    }

    public abstract string PieceName { get; }
    public Properties Properties { get; init; }
    public Coordinate Coordinate { get; init; }
    public Colour Colour => Properties.Colour;
    public PieceType Type => Properties.Type;

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

        var movedPieceColour = Properties.Colour;
        if (game.CurrentPlayer != Properties.Colour)
            return move.AsInvalidBecause(MoveValidity.CannotMoveOpponentsPiece);

        if (Coordinate == to)
            return move.AsInvalidBecause(MoveValidity.CannotMoveToSameCell);

        if (game.TryGetPiece(to, out var piece) &&
            piece.Colour == movedPieceColour) return move.AsInvalidBecause(MoveValidity.CannotCaptureOwnPiece);

        if (this is not IIgnorePathCheck &&
            move.CoordinatesAlongPath.Any(coordinate => game.Board.HasPieceAt(coordinate)))
            return move.AsInvalidBecause(MoveValidity.CannotJumpOverOtherPieces);

        if (MovingIntoCheck(movedPieceColour, move, game))
            return move.AsInvalidBecause(MoveValidity.CannotMoveIntoCheck);

        return move.AsOkResult();
    }

    private bool MovingIntoCheck(Colour movedPieceColour, Move move, Game.Game game)
    {
        var newGameState = game.AsClone();
        newGameState.Move(move, true);

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

    public void Deconstruct(out Properties properties, out Coordinate coordinate)
    {
        properties = Properties;
        coordinate = Coordinate;
    }
}

public record struct PiecePosition(Properties Properties, Coordinate Coordinate)
{
    public PiecePosition(Coordinate Coordinate, Properties Properties) : this(Properties, Coordinate) { }
    
};
