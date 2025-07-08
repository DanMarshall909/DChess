using DChess.Core.Flyweights;

namespace DChess.Core.Game;

/// <summary>
///     A pool of pieces that can be reused to reduce memory allocation / GC pressure
///     This allows us to use structs to store the pieceAttributes internally, but expose classes
///     to the outside world to allow for polymorphism.
/// </summary>
public static class ChessPieceFactory
{
    private static readonly ConcurrentDictionary<PieceContext, ChessPiece> Pool = new();

    public static ChessPiece PieceWithContext(PieceContext pieceContext)
    {
        if (Pool.TryGetValue(pieceContext, out var piece))
            return piece;

        piece = CreatePiece(pieceContext);
        Pool[pieceContext] = piece;

        return piece;
    }

    private static ChessPiece CreatePiece(PieceContext pieceContext)
    {
        var props = pieceContext.PieceAttributes;
        return props.Kind switch
        {
            Kind.Pawn => new Pawn(pieceContext),
            Kind.Rook => new Rook(pieceContext),
            Kind.Knight => new Knight(pieceContext),
            Kind.Bishop => new Bishop(pieceContext),
            Kind.Queen => new Queen(pieceContext),
            Kind.King => new King(pieceContext),
            Kind.None => new NullChessPiece(pieceContext),
            _ => throw new ArgumentOutOfRangeException(nameof(props), props, null)
        };
    }
}