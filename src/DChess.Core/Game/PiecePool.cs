using System.Collections.Concurrent;
using DChess.Core.Pieces;

namespace DChess.Core.Game;

/// <summary>
///     A pool of pieces that can be reused to reduce memory allocation / GC pressure
///     This allows us to use structs to store the properties internally, but expose classes
///     to the outside world to allow for polymorphism.
/// </summary>
public class PiecePool
{
    private static readonly ConcurrentDictionary<Pieces.PiecePosition, Piece> Pool = new();

    public static Piece PieceWithProperties(Pieces.PiecePosition piecePosition)
    {
        if (Pool.TryGetValue(piecePosition, out var piece))
            return piece;

        var (coordinate, properties) = piecePosition;
        
        piece = CreatePiece(piecePosition);
        Pool[piecePosition] = piece;

        return piece;
    }

    private static Piece CreatePiece(PiecePosition piecePosition)
    {
        var props = piecePosition.Properties;
        return props.Type switch
        {
            PieceType.Pawn => new Pawn(piecePosition),
            PieceType.Rook => new Rook(piecePosition),
            PieceType.Knight => new Knight(piecePosition),
            PieceType.Bishop => new Bishop(piecePosition),
            PieceType.Queen => new Queen(piecePosition),
            PieceType.King => new King(piecePosition),
            PieceType.None => new NullPiece(piecePosition),
            _ => throw new ArgumentOutOfRangeException(nameof(props), props, null)
        };
    }
}
