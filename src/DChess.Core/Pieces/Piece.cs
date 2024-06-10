using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public abstract record Piece
{
    protected Piece(Arguments arguments)
    {
        Properties = arguments.PieceProperties;
        Coordinate = arguments.Coordinate;
        Game = arguments.Game;
        InvalidMoveHandler = arguments.InvalidMoveHandler;
    }

    public abstract string PieceName { get; }
    public IInvalidMoveHandler InvalidMoveHandler { get; set; }
    public Properties Properties { get; init; }
    public Coordinate Coordinate { get; init; }
    public Colour Colour => Properties.Colour;
    public PieceType Type => Properties.Type;

    protected Game.Game Game { get; init; }

    public void MoveTo(Coordinate to)
    {
        var move = new Move(Coordinate, to);
        var result = CheckMove(to);
        if (!result.IsValid)
            InvalidMoveHandler.HandleInvalidMove(result);

        Game.Move(move);
    }

    public MoveResult CheckMove(Coordinate to)
    {
        var generalMoveResult = IsGenerallyValid(to);

        if (!generalMoveResult.IsValid)
            return generalMoveResult;

        var validity = ValidateMove(to);

        var moveResult = validity.IsValid ? generalMoveResult : validity;
        return moveResult;
    }

    private MoveResult IsGenerallyValid(Coordinate to)
    {
        var move = new Move(Coordinate, to);

        if (Coordinate == to)
            return move.InvalidResult(CannotMoveToSameCell);

        var movedPieceColour = Properties.Colour;

        if (Game.GameState.TryGetPiece(to, out var piece) &&
            piece.Colour == movedPieceColour) return move.InvalidResult(CannotCaptureOwnPiece);

        if (this is not IIgnorePathCheck &&
            move.Path.Any(coordinate => Game.GameState.HasPieceAt(coordinate)))
            return move.InvalidResult(CannotJumpOverOtherPieces);

        if (MovingIntoCheck(movedPieceColour, move))
            return move.InvalidResult(CannotMoveIntoCheck);

        return move.OkResult();
    }

    private bool MovingIntoCheck(Colour movedPieceColour, Move move)
    {
        var newBoard = Game.GameState.Clone();
        newBoard.Move(move);

        return newBoard.GameState.IsInCheck(movedPieceColour);
    }

    public bool CanMoveTo(Coordinate coordinate)
    {
        var move = new Move(Coordinate, coordinate);
        if(!move.Path.Any())
            return false;
        
        return !move.IsBlocked(Game.GameState);
    }

    protected abstract MoveResult ValidateMove(Coordinate to);

    public void Deconstruct(out Properties properties, out Coordinate coordinate)
    {
        properties = Properties;
        coordinate = Coordinate;
    }

    public record Arguments(Properties PieceProperties, Coordinate Coordinate, Game.Game Game,
        IInvalidMoveHandler InvalidMoveHandler);
}

public record NullPiece : Piece
{
    public NullPiece(Arguments arguments) : base(arguments)
    {
    }

    public override string PieceName { get; } = "NullPiece";

    protected override MoveResult ValidateMove(Coordinate to)
    {
        return new MoveResult(Move.Invalid(), FromCellDoesNoteContainPiece);
    }
}