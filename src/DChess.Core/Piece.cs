namespace DChess.Core;

/// <summary>
/// A pool of pieces that can be reused to reduce memory allocation / GC pressure
/// </summary>
public class PiecePool
{
    // A stack ensures that the most recently used object is the first to be reused.
    // This is efficient for memory caching and locality, as the objects that were
    // used recently are likely to be still in the CPU cache, making their reuse faster
    private readonly Stack<Piece> _pool = new();

    public Piece GetPiece(PieceType type, PieceColour colour, Coordinate position)
    {
        if (_pool.Count <= 0) return new Piece(type, colour, position);

        var piece = _pool.Pop();
        piece.Initialize(type, colour, position);
        return piece;
    }

    public void ReturnPiece(Piece piece)
    {
        _pool.Push(piece);
    }
}

public class Piece
{
    public PieceType Type { get; private set; }
    public PieceColour Colour { get; private set; }
    public Coordinate Coordinate { get; private set; }

    public Piece(PieceType type, PieceColour colour, Coordinate position)
    {
        Initialize(type, colour, position);
    }

    public Piece(PieceType type, PieceColour colour, string coordinateString)
    {
        Initialize(type, colour, new Coordinate(coordinateString));
    }

    public void Initialize(PieceType type, PieceColour colour, Coordinate position)
    {
        Type = type;
        Colour = colour;
        Coordinate = position;
    }
}

public enum PieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

public enum PieceColour
{
    White,
    Black
}