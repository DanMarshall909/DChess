using DChess.Core.Moves;
using DChess.Core.Pieces;

namespace DChess.Core.Board;

/// <summary>
///     A pool of pieces that can be reused to reduce memory allocation / GC pressure
///     This allows us to use structs to store the properties internally, but expose classes
///     to the outside world to allow for polymorphism.
/// </summary>
public class PiecePool(Board board, IInvalidMoveHandler invalidMoveHandler)
{
    private readonly Dictionary<(Coordinate, Properties), Piece> _pool = new();

    public Piece GetPiece(Coordinate coordinate, Properties properties)
    {
        if (_pool.TryGetValue((coordinate, properties), out var piece))
            return piece;

        piece = CreatePiece(coordinate, properties);
        _pool[(coordinate, properties)] = piece;

        return piece;
    }

    private Piece CreatePiece(Coordinate coordinate, Properties properties)
    {
        Piece.Arguments arguments = new(properties, coordinate, board, invalidMoveHandler);
        return properties.Type switch
        {
            PieceType.Pawn => new Pawn(arguments),
            PieceType.Rook => new Rook(arguments),
            PieceType.Knight => new Knight(arguments),
            PieceType.Bishop => new Bishop(arguments),
            PieceType.Queen => new Queen(arguments),
            PieceType.King => new King(arguments),
            _ => throw new ArgumentOutOfRangeException(nameof(properties.Type), properties.Type, null)
        };
    }

    public void Dispose()
    {
        _pool.Clear();
    }
}