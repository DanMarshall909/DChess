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

    public void MoveTo(Coordinate to)
    {
        var move = new Move(Current, to);
        var result = CheckMove(to);
        if (!result.Valid)
            InvalidMoveHandler.HandleInvalidMove(move, result.Message);

        Board.Move(move);
    }


    public MoveResult CheckMove(Coordinate to)
    {
        var generalMoveResult = IsGenerallyValid(to);

        if (generalMoveResult.Valid != true)
            return generalMoveResult;

        var isValidMove = ValidateMove(to);

        return isValidMove.Valid ? generalMoveResult : isValidMove;
    }

    private MoveResult IsGenerallyValid(Coordinate to)
    {
        if (Current == to)
            return new MoveResult(false, new Move(Current, to), "Cannot to to the same square");

        if (Board.Pieces.TryGetValue(to, out var piece))
            if (piece.ChessPiece.Colour == ChessPiece.Colour)
                return new MoveResult(false, new Move(Current, to), "Cannot capture your own piece");
        
        if (this is not IIgnorePathCheck)
        {
            var move = new Move(Current, to);
            
            // Check if there are any pieces between the current position and the destination
            foreach (var coordinate in move.Path)
            {
                if (Board.HasPieceAt(coordinate))
                    return move.AsInvalidResult($"{PieceName} cannot jump over other pieces");
            }
        }

        return new MoveResult(true, new Move(Current, to), null);
    }

    protected abstract MoveResult ValidateMove(Coordinate to);
    public ChessPiece ChessPiece { get; init; }
    public Coordinate Current { get; init; }
    public Colour Colour => ChessPiece.Colour;
    public PieceType Type => ChessPiece.Type;

    protected Board.Board Board { get; init; }

    public void Deconstruct(out ChessPiece chessPiece, out Coordinate coordinate)
    {
        chessPiece = ChessPiece;
        coordinate = Current;
    }

    public record Arguments(ChessPiece ChessPiece, Coordinate Coordinate, Board.Board Board,
        IInvalidMoveHandler InvalidMoveHandler);

    public void MoveTo(string coordinateString)
    {
        MoveTo(new Coordinate(coordinateString));
    }}