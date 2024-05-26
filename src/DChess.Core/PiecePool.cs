using static DChess.Core.PieceStruct.PieceType;

namespace DChess.Core;

/// <summary>
/// A pool of pieceStruct that can be reused to reduce memory allocation / GC pressure
/// This allows us to use structs to store the pieceStruct internally, but expose classes
/// to the outside world to allow for polymorphism.
/// </summary>
public class PiecePool
{
    private readonly Dictionary<(Coordinate, PieceStruct), Piece> _pool = new();
    private readonly Board _board;
    private IInvalidMoveHandler _invalidMoveHandler;

    /// <summary>
    /// A pool of pieceStruct that can be reused to reduce memory allocation / GC pressure
    /// This allows us to use structs to store the pieceStruct internally, but expose classes
    /// to the outside world to allow for polymorphism.
    /// </summary>
    public PiecePool(Board board, IInvalidMoveHandler invalidMoveHandler)
    {
        _board = board;
        _invalidMoveHandler = invalidMoveHandler;
    }

    public Piece Get(Coordinate coordinate, PieceStruct pieceStruct)
    {
        if (_pool.TryGetValue((coordinate, pieceStruct), out var piece))
            return piece;

        piece = CreatePiece(coordinate, pieceStruct);
        _pool[(coordinate, pieceStruct)] = piece;

        return piece;
    }


    private Piece CreatePiece(Coordinate coordinate, PieceStruct pieceStruct)
    {
        Piece.Arguments arguments = new(pieceStruct, coordinate, _board, _invalidMoveHandler);
        return pieceStruct.Type switch
        {
            PieceStruct.PieceType.Pawn => new Pawn(arguments),
            Rook => throw new NotImplementedException(),
            Knight => throw new NotImplementedException(),
            Bishop => throw new NotImplementedException(),
            Queen => throw new NotImplementedException(),
            King => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(pieceStruct.Type), pieceStruct.Type, null)
        };
    }
}