using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public abstract record Piece
{
    protected Piece(Arguments arguments)
    {
        PieceProperties = arguments.PieceProperties;
        Coordinate = arguments.Coordinate;
        Game = arguments.Game;
        InvalidMoveHandler = arguments.InvalidMoveHandler;
    }

    public abstract string PieceName { get; }
    public IInvalidMoveHandler InvalidMoveHandler { get; set; }
    public Properties PieceProperties { get; init; }
    public Coordinate Coordinate { get; init; }
    public Colour Colour => PieceProperties.Colour;
    public PieceType Type => PieceProperties.Type;

    protected Game Game { get; init; }

    public void MoveTo(Coordinate to)
    {
        var move = new Move(Coordinate, to);
        var result = CheckMove(to);
        if (!result.IsValid)
            InvalidMoveHandler.HandleInvalidMove(result);

        Game.Make(move);
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

        var movedPieceColour = PieceProperties.Colour;

        if (Game.GameState.TryGetPiece(to, out var piece) &&
            piece.Colour == movedPieceColour) return move.InvalidResult(CannotCaptureOwnPiece);

        if (this is not IIgnorePathCheck &&
            move.Path.Any(coordinate => Game.GameState.HasPieceAt(coordinate)))
            return move.InvalidResult(CannotJumpOverOtherPieces);

        if (IsInCheck(movedPieceColour, move))
            return move.InvalidResult(CannotMoveIntoCheck);

        return move.OkResult();
    }

    private bool IsInCheck(Colour movedPieceColour, Move move)
    {
        var kingCoordinate = Game.GameState.GetKingCoordinate(movedPieceColour);
        if (kingCoordinate == NullCoordinate)
            return false;

        var newBoard = Game.GameState.Clone();
        newBoard.Make(move);

        return newBoard.GameState.OpposingPiecesByCoordinate(movedPieceColour).Any(p => p.CanMoveTo(kingCoordinate));
    }

    private bool CanMoveTo(Coordinate coordinate) => !new Move(Coordinate, coordinate).IsBlocked(Game);

    protected abstract MoveResult ValidateMove(Coordinate to);

    public void Deconstruct(out Properties properties, out Coordinate coordinate)
    {
        properties = PieceProperties;
        coordinate = Coordinate;
    }

    public record Arguments(Properties PieceProperties, Coordinate Coordinate, Game Game,
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