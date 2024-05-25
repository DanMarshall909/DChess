namespace DChess.Core;

public abstract record PieceFlyweight(Piece Piece, Coordinate Coordinate, Board Board)
{
    public void MoveTo(Coordinate to)
    {
        var move = new Move(Coordinate, to);
        var generalMoveResult = IsGenerallyValid(move);
        
        if (generalMoveResult.Valid != true)
            Board.HandleInvalidMove(move, generalMoveResult.Message);
        
        var isValidMove = IsValidMove(move);

        if(isValidMove.Valid != true)
            throw new InvalidMoveException(move, isValidMove.Message);
        
        Board.Move(move);
    }

    private MoveResult IsGenerallyValid(Move move)
    {
        if (move.From == move.To)
            return new(false, move, "Cannot move to the same square");

        if (Board.PieceFlyweights.TryGetValue(move.To, out var piece))
        {
            if (piece.Piece.Colour == Piece.Colour)
                return new(false, move, "Cannot capture your own piece");
        }

        return new(true, move, null);
    }

    protected abstract MoveResult IsValidMove(Move move);
}