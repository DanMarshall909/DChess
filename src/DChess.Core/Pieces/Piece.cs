using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

/// <summary>
/// Abstract class for handling piece specific logic. 
/// </summary>
public abstract record Piece
{
    protected Piece(Arguments arguments)
    {
        Properties = arguments.PieceProperties;
        Coordinate = arguments.Coordinate;
    }

    public abstract string PieceName { get; }
    public Properties Properties { get; init; }
    public Coordinate Coordinate { get; init; }
    public Colour Colour => Properties.Colour;
    public PieceType Type => Properties.Type;

    public MoveResult CheckMove(Coordinate to, GameState gameState)
    {
        var generalMoveResult = IsGenerallyValid(to, gameState);

        if (!generalMoveResult.IsValid)
            return generalMoveResult;

        var validity = ValidatePath(to, gameState);

        var moveResult = validity.IsValid ? generalMoveResult : validity;
        return moveResult;
    }

    private MoveResult IsGenerallyValid(Coordinate to, GameState gameState)
    {
        var move = new Move(Coordinate, to);

        var movedPieceColour = Properties.Colour;
        if (gameState.CurrentPlayer != Properties.Colour)
            return move.AsInvalidBecause(MoveValidity.CannotMoveOpponentsPiece);

        if (Coordinate == to)
            return move.AsInvalidBecause(MoveValidity.CannotMoveToSameCell);

        if (gameState.TryGetPiece(to, out var piece) &&
            piece.Colour == movedPieceColour) return move.AsInvalidBecause(MoveValidity.CannotCaptureOwnPiece);

        if (this is not IIgnorePathCheck &&
            move.CoordinatesAlongPath.Any(coordinate => gameState.BoardState.HasPieceAt(coordinate)))
            return move.AsInvalidBecause(MoveValidity.CannotJumpOverOtherPieces);

        if (MovingIntoCheck(movedPieceColour, move, gameState))
            return move.AsInvalidBecause(MoveValidity.CannotMoveIntoCheck);

        return move.AsOkResult();
    }

    private bool MovingIntoCheck(Colour movedPieceColour, Move move, GameState gameState)
    {
        var newGameState = gameState.AsClone();
        newGameState.Move(move, force: true);

        return newGameState.IsInCheck(movedPieceColour);
    }

    public bool CanMoveTo(Coordinate to, GameState gameState, params MoveValidity[] validationsToIgnore)
    {
        var move = new Move(Coordinate, to);
        var val = ValidatePath(to, gameState.AsClone());
        if (!val.IsValid)
            return false;

        return move.HasPath && !move.IsBlocked(gameState.BoardState);
    }

    public IEnumerable<Move> LegalMoves(GameState gameState)
    {
        var newGameState = gameState.AsClone();
        return Coordinate
            .All
            .Where(to => CheckMove(to ,newGameState).IsValid)
            .Select(to => new Move(Coordinate, to));
    }

    protected abstract MoveResult ValidatePath(Coordinate to, GameState state);

    public void Deconstruct(out Properties properties, out Coordinate coordinate)
    {
        properties = Properties;
        coordinate = Coordinate;
    }

    public record Arguments(Properties PieceProperties, Coordinate Coordinate);
}