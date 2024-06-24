using DChess.Core.Errors;
using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

/// <summary>
///     Represents a non-existent pieceFlyweight.
/// </summary>
public record NullPieceFlyweight : PieceFlyweight
{
    public NullPieceFlyweight(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public NullPieceFlyweight(IErrorHandler errorHandler)
        : base(new PieceContext(new PieceAttributes(ChessPiece.Type.None, None), Coordinate.None))
    {
    }

    public override string PieceName { get; } = "NullPieceFlyweight";

    protected override MoveResult ValidatePath(Coordinate to, Game.Game game) =>
        new(Move.NullMove, MoveValidity.FromCellDoesNoteContainPiece);
}