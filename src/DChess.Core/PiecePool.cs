using static DChess.Core.PieceProperties;
using static DChess.Core.PieceProperties.PieceColour;
using static DChess.Core.PieceProperties.PieceType;

namespace DChess.Core;

/// <summary>
/// A pool of pieces that can be reused to reduce memory allocation / GC pressure
/// This allows us to use structs to store the pieces internally, but expose classes
/// to the outside world to allow for polymorphism.
/// </summary>
public class PiecePool
{
    private readonly Dictionary<PieceProperties, Piece> _pool = new();

    public PiecePool()
    {
        for (var file = 'a'; file <= 'h'; file++)
        {
            for (byte rank = 1; rank <= 8; rank++)
            {
                var coordinate = new Coordinate(file, rank);
                var props = new PieceProperties(PieceType.Pawn, White, coordinate);
                _pool[props] = CreatePiece(props);
            }
        }
    }

    public Piece this[PieceProperties pieceProperties]
    {
        get
        {
            if (_pool.TryGetValue(pieceProperties, out var piece))
                return piece;

            piece = CreatePiece(pieceProperties);
            _pool[pieceProperties] = piece;
            
            return piece;
        }
    }

    private static Piece CreatePiece(PieceProperties properties)
    {
        return properties.Type switch
        {
            PieceType.Pawn => new Pawn(properties),
            Rook => throw new NotImplementedException(),
            Knight => throw new NotImplementedException(),
            Bishop => throw new NotImplementedException(),
            Queen => throw new NotImplementedException(),
            King => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(properties.Type), properties.Type, null)
        };
    }
}

public abstract class Piece(PieceProperties pieceProperties)
{
    public PieceProperties PieceProperties { get; init; } = pieceProperties;
}

public class Pawn : Piece
{
    public Pawn(PieceProperties properties) : base(properties)
    {
    }
}