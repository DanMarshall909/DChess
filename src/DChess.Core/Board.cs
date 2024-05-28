using DChess.Core.Exceptions;
using DChess.Core.Moves;
using DChess.Core.Pieces;

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

    internal void Move(Move move)
    {
        if (!_piecesByCoordinate.TryGetValue(move.From, out var fromPiece))
            throw new InvalidMoveException(move, $"No piece exists at {move.From}");

        _piecesByCoordinate.Remove(move.From);
        _piecesByCoordinate[move.To] = fromPiece;
    }

    public void Clear()
    {
        _piecesByCoordinate.Clear();
    }
}