using DChess.Core.Errors;
using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

/// <summary>
///     Represents a non-existent piece.
/// </summary>
public record NullPiece : Piece
{
    public NullPiece(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public NullPiece(IErrorHandler errorHandler)
        : base(new PieceContext(new PieceAttributes(PieceType.None, None), Coordinate.None))
    {
    }

    public override string PieceName { get; } = "NullPiece";

    protected override MoveResult ValidatePath(Coordinate to, Game.Game game) =>
        new(Move.NullMove, MoveValidity.FromCellDoesNoteContainPiece);
}