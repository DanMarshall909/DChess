using DChess.Core.Moves;
using static DChess.Core.Pieces.PieceType;

namespace DChess.Core.Pieces;

/// <summary>
/// A pool of pieces that can be reused to reduce memory allocation / GC pressure
/// This allows us to use structs to store the chessPiece internally, but expose classes
/// to the outside world to allow for polymorphism.
/// </summary>
public class PiecePool(Board board, IInvalidMoveHandler invalidMoveHandler)
{
    private readonly Dictionary<(Coordinate, ChessPiece), Piece> _pool = new();

    public Piece Get(Coordinate coordinate, ChessPiece chessPiece)
    {
        if (_pool.TryGetValue((coordinate, chessPiece), out var piece))
            return piece;

        piece = CreatePiece(coordinate, chessPiece);
        _pool[(coordinate, chessPiece)] = piece;

        return piece;
    }


    private Piece CreatePiece(Coordinate coordinate, ChessPiece chessPiece)
    {
        Piece.Arguments arguments = new(chessPiece, coordinate, board, invalidMoveHandler);
        return chessPiece.Type switch
        {
            PieceType.Pawn => new Pawn(arguments),
            Rook => throw new NotImplementedException(),
            PieceType.Knight => new Knight(arguments),
            Bishop => throw new NotImplementedException(),
            Queen => throw new NotImplementedException(),
            King => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(chessPiece.Type), chessPiece.Type, null)
        };
    }
}