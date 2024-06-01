using DChess.Core.Exceptions;
using DChess.Core.Moves;
using DChess.Core.Pieces;

namespace DChess.Core.Board;

public class Board : IDisposable
{
    public const int MaxPieces = 32;

    private readonly IInvalidMoveHandler _invalidMoveHandler;

    private readonly Dictionary<Coordinate, ChessPiece> _piecesByCoordinate;
    private readonly PiecePool _pool;

    public Board(IInvalidMoveHandler invalidMoveHandler,
        Dictionary<Coordinate, ChessPiece>? piecesByCoordinate = null)
    {
        _invalidMoveHandler = invalidMoveHandler;
        _piecesByCoordinate = piecesByCoordinate ?? new Dictionary<Coordinate, ChessPiece>();
        _pool = new PiecePool(this, invalidMoveHandler);
    }

    /// <summary>
    ///     The horizontal files (columns) of the board, from a to h
    /// </summary>
    public char[] Files => new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

    /// <summary>
    ///     The vertical ranks (rows) of the board, from 1 to 8
    /// </summary>
    public byte[] Ranks => new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

    public Dictionary<Coordinate, Piece> Pieces => _piecesByCoordinate
        .ToDictionary(kvp => kvp.Key, kvp => _pool.Get(kvp.Key, kvp.Value));

    public ChessPiece this[Coordinate coordinate]
    {
        get => _piecesByCoordinate[coordinate];
        set => _piecesByCoordinate[coordinate] = value;
    }

    public ChessPiece this[string coordinateString] => this[new Coordinate(coordinateString)];

    public void Dispose()
    {
    }

    public bool TryGetValue(Coordinate coordinate, out ChessPiece chessPiece)
        => _piecesByCoordinate.TryGetValue(coordinate, out chessPiece);

    public bool HasPieceAt(Coordinate coordinate) => _piecesByCoordinate.TryGetValue(coordinate, out _);

    internal void Move(Move move)
    {
        if (!_piecesByCoordinate.TryGetValue(move.From, out var fromPiece))
            throw new InvalidMoveException(move, $"No piece exists at {move.From}");

        bool pawnIsPromoted = (fromPiece.Type == PieceType.Pawn && move.To.File == 'a') || move.To.File == 'h';
        var toPiece = pawnIsPromoted
            ? new ChessPiece(PieceType.Queen, fromPiece.Colour)
            : fromPiece;

        _piecesByCoordinate.Remove(move.From);
        _piecesByCoordinate[move.To] = toPiece;
    }

    public void Clear()
    {
        _piecesByCoordinate.Clear();
    }
}