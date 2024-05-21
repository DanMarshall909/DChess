namespace DChess.Core;

public class PieceCollection
{
    private readonly Dictionary<Coordinate, PieceProperties> _pieces = new();

    public void Add(PieceProperties pieceProperties)
    {
        if (_pieces.Count >= Board.MaxPieces)
        {
            throw new InvalidOperationException("Cannot add more pieces to the board");
        }

        _pieces[pieceProperties.Coordinate] = pieceProperties;
    }

    public bool TryGetPiece(Coordinate coordinate, out PieceProperties pieceProperties) =>
        _pieces.TryGetValue(coordinate, out pieceProperties);

    public PieceProperties this[Coordinate coordinate]
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