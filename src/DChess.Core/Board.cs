using DChess.Core.Moves;
using DChess.Core.Pieces;
using static DChess.Core.Coordinate;
using static DChess.Core.Pieces.Colour;

namespace DChess.Core;

public class Board : IDisposable
{
    public void Dispose()
    {
    }

    private readonly IInvalidMoveHandler _invalidMoveHandler;
    public const int MaxPieces = 32;
    private readonly PiecePool _pool;

    public Board(IInvalidMoveHandler invalidMoveHandler,
        Dictionary<Coordinate, ChessPiece>? piecesByCoordinate = null)
    {
        _invalidMoveHandler = invalidMoveHandler;
        _piecesByCoordinate = piecesByCoordinate ?? new Dictionary<Coordinate, ChessPiece>();
        _pool = new PiecePool(this, invalidMoveHandler);
    }

    public Dictionary<Coordinate, Piece> Pieces => _piecesByCoordinate
        .ToDictionary(kvp => kvp.Key, kvp => _pool.Get(kvp.Key, kvp.Value));

    public bool TryGetValue(Coordinate coordinate, out ChessPiece chessPiece)
        => _piecesByCoordinate.TryGetValue(coordinate, out chessPiece);

    public ChessPiece this[Coordinate coordinate]
    {
        get => _piecesByCoordinate[coordinate];
        set => _piecesByCoordinate[coordinate] = value;
    }

    public ChessPiece this[string coordinateString] => this[new Coordinate(coordinateString)];

    private readonly Dictionary<Coordinate, ChessPiece> _piecesByCoordinate;

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

        void Place(Coordinate coordinate, PieceType type, Colour colour)
        {
            board._piecesByCoordinate[coordinate] = new ChessPiece(type, colour);
        }
    }

    internal void Move(Move move)
    {
        if (!_piecesByCoordinate.TryGetValue(move.From, out var fromPiece))
            throw new InvalidMoveException(move, $"No piece exists at {move.From}");

        _piecesByCoordinate.Remove(move.From);
        _piecesByCoordinate[move.To] = fromPiece;
    }
}