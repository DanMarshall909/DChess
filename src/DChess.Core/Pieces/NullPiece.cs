using DChess.Core.Errors;
using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

/// <summary>
///     Represents a non-existent piece.
/// </summary>
public record NullPiece : Piece
{
    public NullPiece(PiecePosition piecePosition) : base(piecePosition)
    {
    }

    public NullPiece(IErrorHandler errorHandler)
        : base(new PiecePosition(new Properties(PieceType.None, None), Coordinate.None))
    {
    }

    public override string PieceName { get; } = "NullPiece";

    protected override MoveResult ValidatePath(Coordinate to, Game.Game game) =>
        new(Move.NullMove, MoveValidity.FromCellDoesNoteContainPiece);
}