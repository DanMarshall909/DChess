using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public abstract record Piece
{
    protected Piece(Arguments arguments)
    {
        ChessPiece = arguments.ChessPiece;
        Position = arguments.Coordinate;
        Board = arguments.Board;
        InvalidMoveHandler = arguments.InvalidMoveHandler;
    }

    public IInvalidMoveHandler InvalidMoveHandler { get; set; }

    public void MoveTo(Coordinate to)
    {
        var move = new Move(Position, to);
        var result = CheckMove(move);
        if (!result.Valid)
            InvalidMoveHandler.HandleInvalidMove(move, result.Message);

        Board.Move(move);
    }

    
    public MoveResult CheckMove(Move move)
    {
        var generalMoveResult = IsGenerallyValid(move.To);

        if (generalMoveResult.Valid != true)
            return generalMoveResult;

        var isValidMove = ValidateMove(move.To);

        return isValidMove.Valid ? generalMoveResult : isValidMove;
    }

    private MoveResult IsGenerallyValid(Coordinate to)
    {
        if (Position == to)
            return new(false, new(Position, to), "Cannot move to the same square");

        if (Board.Pieces.TryGetValue(to, out var piece))
        {
            if (piece.ChessPiece.Colour == ChessPiece.Colour)
                return new(false, new(Position, to), "Cannot capture your own piece");
        }

        return new(true, new(Position, to), null);
    }

    protected abstract MoveResult ValidateMove(Coordinate to);
    public ChessPiece ChessPiece { get; init; }
    public Coordinate Position { get; init; }
    public Colour Colour => ChessPiece.Colour;
    public PieceType Type => ChessPiece.Type;
    
    protected Board Board { get; init; }

    public void Deconstruct(out ChessPiece chessPiece, out Coordinate coordinate)
    {
        chessPiece = ChessPiece;
        coordinate = Position;
    }

    public record Arguments(ChessPiece ChessPiece, Coordinate Coordinate, Board Board, IInvalidMoveHandler InvalidMoveHandler);

    public MoveResult CheckMove(Coordinate move, Coordinate coordinate) 
        => CheckMove(new Move(Position, move));
}