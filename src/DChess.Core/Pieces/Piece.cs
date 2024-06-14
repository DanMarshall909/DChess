using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

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
        ValidateMove(to, gameState);
        
        var generalMoveResult = IsGenerallyValid(to, gameState);

        if (!generalMoveResult.IsValid)
            return generalMoveResult;

        var validity = ValidateMove(to, gameState);

        var moveResult = validity.IsValid ? generalMoveResult : validity;
        return moveResult;
    }

    private MoveResult IsGenerallyValid(Coordinate to, GameState gameState)
    {
        var move = new Move(Coordinate, to);
        
        var movedPieceColour = Properties.Colour;
        if(gameState.GetProperties(to).Colour != Properties.Colour)
            return move.InvalidResult(CannotMoveOpponentsPiece);

        if (Coordinate == to)
            return move.InvalidResult(CannotMoveToSameCell);

        if (gameState.TryGetPiece(to, out var piece) &&
            piece.Colour == movedPieceColour) return move.InvalidResult(CannotCaptureOwnPiece);

        if (this is not IIgnorePathCheck &&
            move.CoordinatesAlongPath.Any(coordinate => gameState.HasPieceAt(coordinate)))
            return move.InvalidResult(CannotJumpOverOtherPieces);

        if (MovingIntoCheck(movedPieceColour, move, gameState))
            return move.InvalidResult(CannotMoveIntoCheck);

        return move.OkResult();
    }

    private bool MovingIntoCheck(Colour movedPieceColour, Move move, GameState gameState)
    {
        var newGameState = gameState.AsClone();
        newGameState.Move(move, force: true);

        return newGameState.IsInCheck(movedPieceColour);
    }

    public bool CanMoveTo(Coordinate to, GameState gameState)
    {
        var move = new Move(Coordinate, to);
        var val = ValidateMove(to, gameState.AsClone());
        if (!val.IsValid)
            return false;
        return move.IsLegalIfNotBlocked && !move.IsBlocked(gameState.BoardState);
    }

    protected abstract MoveResult ValidateMove(Coordinate to, GameState state);

    public void Deconstruct(out Properties properties, out Coordinate coordinate)
    {
        properties = Properties;
        coordinate = Coordinate;
    }

    public record Arguments(Properties PieceProperties, Coordinate Coordinate);

    public IEnumerable<Move> LegalMoves(GameState gameState)
    {
        return Coordinate
            .All
            .Where(to => CanMoveTo(to, gameState))
            .Select(to => new Move(Coordinate, to));
    }
}

public record NullPiece : Piece
{
    public NullPiece(Arguments arguments) : base(arguments)
    {
    }
    
    public NullPiece(IErrorHandler errorHandler) 
        : base(new Arguments(new Properties(PieceType.None, None), Coordinate.None))
    {
    }

    public override string PieceName { get; } = "NullPiece";

    protected override MoveResult ValidateMove(Coordinate to, GameState gameState) =>
        new(Move.InvalidMove, FromCellDoesNoteContainPiece);
}