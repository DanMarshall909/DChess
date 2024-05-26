using DChess.Core.Moves;
using DChess.Core.Pieces;
using static DChess.Core.Pieces.ChessPiece;

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
        Place(a8, BlackRook);
        Place(b8, BlackKnight);
        Place(c8, BlackBishop);
        Place(d8, BlackQueen);
        Place(e8, BlackKing);
        Place(f8, BlackBishop);
        Place(g8, BlackKnight);
        Place(h8, BlackRook);

        Place(a7, BlackPawn);
        Place(b7, BlackPawn);
        Place(c7, BlackPawn);
        Place(d7, BlackPawn);
        Place(e7, BlackPawn);
        Place(f7, BlackPawn);
        Place(g7, BlackPawn);
        Place(h7, BlackPawn);

        Place(a2, WhitePawn);
        Place(b2, WhitePawn);
        Place(c2, WhitePawn);
        Place(d2, WhitePawn);
        Place(e2, WhitePawn);
        Place(f2, WhitePawn);
        Place(g2, WhitePawn);
        Place(h2, WhitePawn);

        Place(a1, WhiteRook);
        Place(b1, WhiteKnight);
        Place(c1, WhiteBishop);
        Place(d1, WhiteQueen);
        Place(e1, WhiteKing);
        Place(f1, WhiteBishop);
        Place(g1, WhiteKnight);
        Place(h1, WhiteRook);
        return;

        void Place(Coordinate coordinate, ChessPiece chessPiece)
        {
            board._piecesByCoordinate[coordinate] = new ChessPiece(chessPiece.Type, chessPiece.Colour);
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