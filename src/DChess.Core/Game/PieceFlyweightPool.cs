using System.Collections.Concurrent;
using DChess.Core.Pieces;

namespace DChess.Core.Game;

/// <summary>
///     A pool of pieces that can be reused to reduce memory allocation / GC pressure
///     This allows us to use structs to store the pieceAttributes internally, but expose classes
///     to the outside world to allow for polymorphism.
/// </summary>
public class PieceFlyweightPool
{
    private static readonly ConcurrentDictionary<Pieces.PieceContext, PieceFlyweight> Pool = new();

    public static PieceFlyweight PieceWithProperties(Pieces.PieceContext pieceContext)
    {
        if (Pool.TryGetValue(pieceContext, out var piece))
            return piece;

        var (coordinate, properties) = pieceContext;
        
        piece = CreatePiece(pieceContext);
        Pool[pieceContext] = piece;

        return piece;
    }

    private static PieceFlyweight CreatePiece(PieceContext pieceContext)
    {
        var props = pieceContext.PieceAttributes;
        return props.Type switch
        {
            ChessPiece.Type.Pawn => new Pawn(pieceContext),
            ChessPiece.Type.Rook => new Rook(pieceContext),
            ChessPiece.Type.Knight => new Knight(pieceContext),
            ChessPiece.Type.Bishop => new Bishop(pieceContext),
            ChessPiece.Type.Queen => new Queen(pieceContext),
            ChessPiece.Type.King => new King(pieceContext),
            ChessPiece.Type.None => new NullPieceFlyweight(pieceContext),
            _ => throw new ArgumentOutOfRangeException(nameof(props), props, null)
        };
    }
}
