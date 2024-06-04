using DChess.Core.Moves;
using DChess.Core.Pieces;

namespace DChess.Core.Board;

/// <summary>
///     A pool of pieces that can be reused to reduce memory allocation / GC pressure
///     This allows us to use structs to store the pieceProperties internally, but expose classes
///     to the outside world to allow for polymorphism.
/// </summary>
public class PiecePool(Board board, IInvalidMoveHandler invalidMoveHandler)
{
    private readonly Dictionary<(Coordinate, PieceProperties), Piece> _pool = new();

    public Piece Get(Coordinate coordinate, PieceProperties pieceProperties)
    {
        if (_pool.TryGetValue((coordinate, pieceProperties), out var piece))
            return piece;

        piece = CreatePiece(coordinate, pieceProperties);
        _pool[(coordinate, pieceProperties)] = piece;

        return piece;
    }

    private Piece CreatePiece(Coordinate coordinate, PieceProperties pieceProperties)
    {
        Piece.Arguments arguments = new(pieceProperties, coordinate, board, invalidMoveHandler);
        return pieceProperties.Type switch
        {
            PieceType.Pawn => new Pawn(arguments),
            PieceType.Rook => new Rook(arguments),
            PieceType.Knight => new Knight(arguments),
            PieceType.Bishop => new Bishop(arguments),
            PieceType.Queen => new Queen(arguments),
            PieceType.King => new King(arguments),
            _ => throw new ArgumentOutOfRangeException(nameof(pieceProperties.Type), pieceProperties.Type, null)
        };
    }

    public void Dispose()
    {
        _pool.Clear();
    }
}