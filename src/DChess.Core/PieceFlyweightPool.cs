using static DChess.Core.Piece.PieceType;

namespace DChess.Core;

/// <summary>
/// A pool of piece that can be reused to reduce memory allocation / GC pressure
/// This allows us to use structs to store the piece internally, but expose classes
/// to the outside world to allow for polymorphism.
/// </summary>
public class PieceFlyweightPool
{
    private readonly Dictionary<(Coordinate, Piece), PieceFlyweight> _pool = new();
    private readonly Board _board;

    /// <summary>
    /// A pool of piece that can be reused to reduce memory allocation / GC pressure
    /// This allows us to use structs to store the piece internally, but expose classes
    /// to the outside world to allow for polymorphism.
    /// </summary>
    public PieceFlyweightPool(Board board) => _board = board;

    public PieceFlyweight Get(Coordinate coordinate, Piece piece)
    {
        if (_pool.TryGetValue((coordinate, piece), out var pieceFlyweight))
            return pieceFlyweight;

        pieceFlyweight = CreateFlyweight(coordinate, piece);
        _pool[(coordinate, piece)] = pieceFlyweight;

        return pieceFlyweight;
    }


    private PieceFlyweight CreateFlyweight(Coordinate coordinate, Piece piece)
    {
        return piece.Type switch
        {
            Pawn => new PawnFlyweight(piece, coordinate, _board),
            Rook => throw new NotImplementedException(),
            Knight => throw new NotImplementedException(),
            Bishop => throw new NotImplementedException(),
            Queen => throw new NotImplementedException(),
            King => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(piece.Type), piece.Type, null)
        };
    }
}