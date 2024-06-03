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

    public abstract string PieceName { get; }
    public IInvalidMoveHandler InvalidMoveHandler { get; set; }
    public ChessPiece ChessPiece { get; init; }
    public Coordinate Coordinate { get; init; }
    public Colour Colour => ChessPiece.Colour;
    public PieceType Type => ChessPiece.Type;

    protected Board.Board Board { get; init; }

    public void MoveTo(Coordinate to)
    {
        var move = new Move(Coordinate, to);
        var result = CheckMove(to);
        if (!result.IsValid)
            InvalidMoveHandler.HandleInvalidMove(result);

        Board.Make(move);
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

        var movedPieceColour = ChessPiece.Colour;

        if (Board.Pieces.TryGetValue(to, out var piece) &&
            piece.Colour == movedPieceColour) return move.InvalidResult(CannotCaptureOwnPiece);

        if (this is not IIgnorePathCheck && move.Path.Any(coordinate => Board.HasPieceAt(coordinate)))
            return move.InvalidResult(CannotJumpOverOtherPieces);

        if (IsInCheck(movedPieceColour, move)) 
            return move.InvalidResult(CannotMoveIntoCheck);

        return move.OkResult();
    }

    private bool IsInCheck(Colour movedPieceColour, Move move)
    {
        var kingCoordinate = Board.GetKingCoordinate(movedPieceColour);
        
        if (!kingCoordinate.IsValid())
            return false;

        var newBoard = Board.Clone();
        newBoard.Make(move);
        foreach (var (_, p) in newBoard.OpposingPiecesByCoordinate(movedPieceColour))
        {
            if (p.CanMoveTo(kingCoordinate))
                return true;
        }

        return false;
    }

    private bool CanMoveTo(Coordinate coordinate) => !new Move(Coordinate, coordinate).IsBlocked(Board);

    protected abstract MoveResult ValidateMove(Coordinate to);

    public void Deconstruct(out ChessPiece chessPiece, out Coordinate coordinate)
    {
        chessPiece = ChessPiece;
        coordinate = Coordinate;
    }

    public record Arguments(ChessPiece ChessPiece, Coordinate Coordinate, Board.Board Board,
        IInvalidMoveHandler InvalidMoveHandler);
}