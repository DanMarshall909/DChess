using static DChess.Core.Coordinate;
using static DChess.Core.Piece;
using static DChess.Core.Piece.PieceColour;

namespace DChess.Core;

public class Board
{
    public const int MaxPieces = 32;
    private readonly PieceFlyweightPool _pool;

    public Board(Dictionary<Coordinate, Piece>? piecesByCoordinate = null)
    {
        _piecesByCoordinate = piecesByCoordinate ?? new Dictionary<Coordinate, Piece>();
        _pool = new PieceFlyweightPool(this);
    }

    public Dictionary<Coordinate, PieceFlyweight> PieceFlyweights => _piecesByCoordinate
        .ToDictionary(kvp => kvp.Key, kvp => _pool.Get(kvp.Key, kvp.Value));

    public bool TryGetValue(Coordinate coordinate, out Piece piece)
        => _piecesByCoordinate.TryGetValue(coordinate, out piece);

    public Piece this[Coordinate coordinate]
    {
        get => _piecesByCoordinate[coordinate];
        set => _piecesByCoordinate[coordinate] = value;
    }

    public Piece this[string coordinateString] => this[new Coordinate(coordinateString)];

    private readonly Dictionary<Coordinate, Piece> _piecesByCoordinate;

    public bool HasPieceAt(Coordinate coordinate) => _piecesByCoordinate.TryGetValue(coordinate, out _);

    public static void SetStandardLayout(Board board)
    {
        Place(a8, PieceType.Rook, Black);
        Place(b8, PieceType.Knight, Black);
        Place(c8, PieceType.Bishop, Black);
        Place(d8, PieceType.Queen, Black);
        Place(e8, PieceType.King, Black);
        Place(f8, PieceType.Bishop, Black);
        Place(g8, PieceType.Knight, Black);
        Place(h8, PieceType.Rook, Black);

        Place(a7, PieceType.Pawn, Black);
        Place(b7, PieceType.Pawn, Black);
        Place(c7, PieceType.Pawn, Black);
        Place(d7, PieceType.Pawn, Black);
        Place(e7, PieceType.Pawn, Black);
        Place(f7, PieceType.Pawn, Black);
        Place(g7, PieceType.Pawn, Black);
        Place(h7, PieceType.Pawn, Black);

        Place(a2, PieceType.Pawn, White);
        Place(b2, PieceType.Pawn, White);
        Place(c2, PieceType.Pawn, White);
        Place(d2, PieceType.Pawn, White);
        Place(e2, PieceType.Pawn, White);
        Place(f2, PieceType.Pawn, White);
        Place(g2, PieceType.Pawn, White);
        Place(h2, PieceType.Pawn, White);

        Place(a1, PieceType.Rook, White);
        Place(b1, PieceType.Knight, White);
        Place(c1, PieceType.Bishop, White);
        Place(d1, PieceType.Queen, White);
        Place(e1, PieceType.King, White);
        Place(f1, PieceType.Bishop, White);
        Place(g1, PieceType.Knight, White);
        Place(h1, PieceType.Rook, White);
        return;

        void Place(Coordinate coordinate, PieceType type, PieceColour colour)
        {
            board._piecesByCoordinate[coordinate] = new Piece(type, colour);
        }
    }

    public void Move(Coordinate from, Coordinate to)
    {
        if (!_piecesByCoordinate.TryGetValue(from, out var pieceStruct))
            throw new InvalidMoveException(from, to, $"No piece exists at {from}");
        _piecesByCoordinate.Remove(from);
        _piecesByCoordinate[to] = pieceStruct;
    }
}

public class InvalidMoveException(Coordinate from, Coordinate to, string? message = null) : Exception
{
    public Coordinate To { get; } = to;
    public Coordinate From { get; } = from;

    public override string Message { get; } =
        $"Invalid move from {from}, to: {to}" + (message is not null ? $": {message}" : "");
}