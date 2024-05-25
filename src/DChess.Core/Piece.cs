namespace DChess.Core;

public abstract record Piece
{
    protected Piece(PieceStruct PieceStruct, Coordinate Coordinate, Board Board)
    {
        this.PieceStruct = PieceStruct;
        this.Coordinate = Coordinate;
        this.Board = Board;
    }

    public void MoveTo(Coordinate to)
    {
        var move = new Move(Coordinate, to);
        var generalMoveResult = IsGenerallyValid(move);
        
        if (generalMoveResult.Valid != true)
            Board.HandleInvalidMove(move, generalMoveResult.Message);
        
        var isValidMove = ValidateMove(move);

        if(isValidMove.Valid != true)
            Board.HandleInvalidMove(move, isValidMove.Message);
        
        Board.Move(move);
    }
    
    private MoveResult IsGenerallyValid(Move move)
    {
        if (move.From == move.To)
            return new(false, move, "Cannot move to the same square");

        if (Board.Pieces.TryGetValue(move.To, out var piece))
        {
            if (piece.PieceStruct.Colour == PieceStruct.Colour)
                return new(false, move, "Cannot capture your own pieceStruct");
        }

        return new(true, move, null);
    }
    
    protected MoveResult ReportInvalidMove(Move move, string message)
    {
        throw new InvalidMoveException(move, message);
    }

    protected abstract MoveResult ValidateMove(Move move);
    public PieceStruct PieceStruct { get; init; }
    public Coordinate Coordinate { get; init; }
    protected Board Board { get; init; }

    public void Deconstruct(out PieceStruct pieceStruct, out Coordinate coordinate)
    {
        pieceStruct = this.PieceStruct;
        coordinate = this.Coordinate;
    }
}