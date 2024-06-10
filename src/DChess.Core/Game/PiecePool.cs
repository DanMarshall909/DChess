using DChess.Core.Moves;
using DChess.Core.Pieces;

namespace DChess.Core.Game;

/// <summary>
///     A pool of pieces that can be reused to reduce memory allocation / GC pressure
///     This allows us to use structs to store the properties internally, but expose classes
///     to the outside world to allow for polymorphism.
/// </summary>
public class PiecePool(Game game, IInvalidMoveHandler invalidMoveHandler)
{
    private readonly Dictionary<(Coordinate, Properties), Piece> _pool = new();

    public Piece PieceWithProperties(Coordinate coordinate, Properties properties)
    {
        if (_pool.TryGetValue((coordinate, properties), out var piece))
            return piece;

        piece = CreatePiece(coordinate, properties);
        _pool[(coordinate, properties)] = piece;

        return piece;
    }

    private Piece CreatePiece(Coordinate coordinate, Properties properties)
    {
        Piece.Arguments arguments = new(properties, coordinate, game, invalidMoveHandler);
        return properties.Type switch
        {
            PieceType.Pawn => new Pawn(arguments),
            PieceType.Rook => new Rook(arguments),
            PieceType.Knight => new Knight(arguments),
            PieceType.Bishop => new Bishop(arguments),
            PieceType.Queen => new Queen(arguments),
            PieceType.King => new King(arguments),
            PieceType.None => new NullPiece(arguments),
            _ => throw new ArgumentOutOfRangeException(nameof(properties.Type), properties.Type, null)
        };
    }

    public void Dispose()
    {
        _pool.Clear();
    }
}