using System.Collections.Concurrent;
using DChess.Core.Pieces;

namespace DChess.Core.Game;

/// <summary>
///     A pool of pieces that can be reused to reduce memory allocation / GC pressure
///     This allows us to use structs to store the pieceAttributes internally, but expose classes
///     to the outside world to allow for polymorphism.
/// </summary>
public class PiecePool
{
    private static readonly ConcurrentDictionary<Pieces.PieceContext, Piece> Pool = new();

    public static Piece PieceWithProperties(Pieces.PieceContext pieceContext)
    {
        if (Pool.TryGetValue(pieceContext, out var piece))
            return piece;

        var (coordinate, properties) = pieceContext;
        
        piece = CreatePiece(pieceContext);
        Pool[pieceContext] = piece;

        return piece;
    }

    private static Piece CreatePiece(PieceContext pieceContext)
    {
        var props = pieceContext.PieceAttributes;
        return props.Type switch
        {
            PieceType.Pawn => new Pawn(pieceContext),
            PieceType.Rook => new Rook(pieceContext),
            PieceType.Knight => new Knight(pieceContext),
            PieceType.Bishop => new Bishop(pieceContext),
            PieceType.Queen => new Queen(pieceContext),
            PieceType.King => new King(pieceContext),
            PieceType.None => new NullPiece(pieceContext),
            _ => throw new ArgumentOutOfRangeException(nameof(props), props, null)
        };
    }
}
