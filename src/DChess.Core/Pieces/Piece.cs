using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public abstract record Piece
{
    protected Piece(Arguments arguments)
    {
        ChessPiece = arguments.ChessPiece;
        Current = arguments.Coordinate;
        Board = arguments.Board;
        InvalidMoveHandler = arguments.InvalidMoveHandler;
    }

    public abstract string PieceName { get; }
    public IInvalidMoveHandler InvalidMoveHandler { get; set; }
    public ChessPiece ChessPiece { get; init; }
    public Coordinate Current { get; init; }
    public Colour Colour => ChessPiece.Colour;
    public PieceType Type => ChessPiece.Type;

    protected Board.Board Board { get; init; }

    public void MoveTo(Coordinate to)
    {
        var move = new Move(Current, to);
        var result = CheckMove(to);
        if (!result.IsValid)
            InvalidMoveHandler.HandleInvalidMove(result);

        Board.Move(move);
    }


    public MoveResult CheckMove(Coordinate to)
    {
        var generalMoveResult = IsGenerallyValid(to);

        if (!generalMoveResult.IsValid)
            return generalMoveResult;

        var isValidMove = ValidateMove(to);

        return isValidMove.IsValid ? generalMoveResult : isValidMove;
    }

    private MoveResult IsGenerallyValid(Coordinate to)
    {
        var move = new Move(Current, to);
        
        if (Current == to)
            return move.InvalidResult(CannotMoveToSameCell);

        if (Board.Pieces.TryGetValue(to, out var piece))
            if (piece.ChessPiece.Colour == ChessPiece.Colour)
                return move.InvalidResult(CannotCaptureOwnPiece);


        if (this is not IIgnorePathCheck && move.Path.Any(coordinate => Board.HasPieceAt(coordinate)))
            return move.InvalidResult(CannotJumpOverOtherPieces);

        return move.OkResult();
    }

    protected abstract MoveResult ValidateMove(Coordinate to);

    public void Deconstruct(out ChessPiece chessPiece, out Coordinate coordinate)
    {
        chessPiece = ChessPiece;
        coordinate = Current;
    }

    public void MoveTo(string coordinateString)
    {
        MoveTo(new Coordinate(coordinateString));
    }

    public record Arguments(ChessPiece ChessPiece, Coordinate Coordinate, Board.Board Board,
        IInvalidMoveHandler InvalidMoveHandler);
}