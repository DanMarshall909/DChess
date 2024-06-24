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
        return props.Kind switch
        {
            Piece.Kind.Pawn => new Pawn(pieceContext),
            Piece.Kind.Rook => new Rook(pieceContext),
            Piece.Kind.Knight => new Knight(pieceContext),
            Piece.Kind.Bishop => new Bishop(pieceContext),
            Piece.Kind.Queen => new Queen(pieceContext),
            Piece.Kind.King => new King(pieceContext),
            Piece.Kind.None => new NullPieceFlyweight(pieceContext),
            _ => throw new ArgumentOutOfRangeException(nameof(props), props, null)
        };
    }
}
