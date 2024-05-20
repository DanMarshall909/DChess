namespace DChess.Core;

public class PieceCollection
{
    private readonly Dictionary<Coordinate, Piece> _pieces = new();

    public void Add(Piece piece)
    {
        if (_pieces.Count >= Board.MaxPieces)
        {
            throw new InvalidOperationException("Cannot add more pieces to the board");
        }

        _pieces[piece.Coordinate] = piece;
    }

    public bool TryGetPiece(Coordinate coordinate, out Piece piece) =>
        _pieces.TryGetValue(coordinate, out piece);

    public Piece this[Coordinate coordinate]
    {
        get
        {
            if (_pieces.TryGetValue(coordinate, out var piece))
                return piece;

            throw new KeyNotFoundException($"No piece at {coordinate}");
        }
        set => _pieces[coordinate] = value;
    }
}