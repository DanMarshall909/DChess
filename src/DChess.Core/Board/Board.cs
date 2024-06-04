using System.Collections.ObjectModel;
using DChess.Core.Exceptions;
using DChess.Core.Moves;
using DChess.Core.Pieces;

namespace DChess.Core.Board;

public sealed class Board : IDisposable
{
    public bool HasPieceAt(Coordinate coordinate) => _pieceAt.TryGetValue(coordinate, out _);

    public Dictionary<Coordinate, PieceProperties> PieceAt => _pieceAt;

    public Dictionary<Coordinate, Piece> OpposingPiecesByCoordinate(Colour colour) => _pieceAt
        .Where(x => x.Value.Colour != colour)
        .ToDictionary(kvp => kvp.Key, kvp => _pool.Get(kvp.Key, kvp.Value));

    public Dictionary<Coordinate, Piece> Friendly(Colour colour) => _pieceAt
        .Where(x => x.Value.Colour == colour)
        .ToDictionary(kvp => kvp.Key, kvp => _pool.Get(kvp.Key, kvp.Value));

    public ReadOnlyDictionary<Coordinate, Piece> Pieces => _pieceAt
        .ToDictionary(kvp => kvp.Key, kvp => _pool.Get(kvp.Key, kvp.Value)).AsReadOnly();

    public bool TryGetProperties(Coordinate coordinate, out PieceProperties pieceProperties)
        => _pieceAt.TryGetValue(coordinate, out pieceProperties);

    /// <summary>
    ///     The vertical ranks (rows) of the board, from 1 to 8
    /// </summary>
    public static byte[] Ranks = { 1, 2, 3, 4, 5, 6, 7, 8 };

    /// <summary>
    ///     The horizontal files (columns) of the board, from a to h
    /// </summary>
    public static char[] Files = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

    internal void Make(Move move)
    {
        if (!_pieceAt.TryGetValue(move.From, out var fromPiece))
            throw new InvalidMoveException(move, $"No pieceProperties exists at {move.From}");

        bool pawnIsPromoted = (fromPiece.Type == PieceType.Pawn && move.To.File == 'a') || move.To.File == 'h';
        var toPiece = pawnIsPromoted
            ? new PieceProperties(PieceType.Queen, fromPiece.Colour)
            : fromPiece;

        _pieceAt.Remove(move.From);
        _pieceAt[move.To] = toPiece;
    }

    public Board(IInvalidMoveHandler invalidMoveHandler,
        Dictionary<Coordinate, PieceProperties>? piecesByCoordinate = null)
    {
        _invalidMoveHandler = invalidMoveHandler;
        _pieceAt = piecesByCoordinate ?? new Dictionary<Coordinate, PieceProperties>();
        _pool = new PiecePool(this, invalidMoveHandler);
    }

    private readonly IInvalidMoveHandler _invalidMoveHandler;

    private readonly Dictionary<Coordinate, PieceProperties> _pieceAt;

    private readonly PiecePool _pool;

    public void Dispose()
    {
        _pieceAt.Clear();
        _pool.Dispose();
    }

    public void Clear()
    {
        _pieceAt.Clear();
    }

    public Board Clone()
    {
        var pieces = _pieceAt.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        return new Board(_invalidMoveHandler, pieces);
    }

    public Coordinate GetKingCoordinate(Colour movedPieceColour)
    {
        return _pieceAt
            .Where(x => x.Value.Type == PieceType.King && x.Value.Colour == movedPieceColour)
            .Select(x => x.Key)
            .FirstOrDefault();
    }
}