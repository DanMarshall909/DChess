using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public abstract record Piece
{
    protected Piece(Arguments arguments)
    {
        ChessPiece = arguments.ChessPiece;
        Coordinate = arguments.Coordinate;
        Board = arguments.Board;
        InvalidMoveHandler = arguments.InvalidMoveHandler;
    }

    public IInvalidMoveHandler InvalidMoveHandler { get; set; }

    public void MoveTo(Coordinate to)
    {
        
        var move = new Move(Coordinate, to);
        var result = CheckMoveTo(move);
        if (!result.Valid)
            InvalidMoveHandler.HandleInvalidMove(move, result.Message);

        Board.Move(move);
    }

    
    public MoveResult CheckMoveTo(Move move)
    {
        var generalMoveResult = IsGenerallyValid(move);

        if (generalMoveResult.Valid != true)
            return generalMoveResult;

        var isValidMove = ValidateMove(move);

        return isValidMove.Valid ? generalMoveResult : isValidMove;
    }

    private MoveResult IsGenerallyValid(Move move)
    {
        if (move.From == move.To)
            return new(false, move, "Cannot move to the same square");

        if (Board.Pieces.TryGetValue(move.To, out var piece))
        {
            if (piece.ChessPiece.Colour == ChessPiece.Colour)
                return new(false, move, "Cannot capture your own piece");
        }

        return new(true, move, null);
    }

    protected MoveResult ReportInvalidMove(Move move, string message)
    {
        throw new InvalidMoveException(move, message);
    }

    protected abstract MoveResult ValidateMove(Move move);
    public ChessPiece ChessPiece { get; init; }
    public Coordinate Coordinate { get; init; }
    public Colour Colour => ChessPiece.Colour;
    public PieceType Type => ChessPiece.Type;
    
    protected Board Board { get; init; }

    public void Deconstruct(out ChessPiece chessPiece, out Coordinate coordinate)
    {
        chessPiece = ChessPiece;
        coordinate = Coordinate;
    }

    public record Arguments(ChessPiece ChessPiece, Coordinate Coordinate, Board Board, IInvalidMoveHandler InvalidMoveHandler);
}