namespace DChess.Core.Flyweights;

/// <summary>
///     Represents a non-existent chessPiece.
/// </summary>
public record NullChessPiece : ChessPiece
{
    public NullChessPiece(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public NullChessPiece()
        : base(new PieceContext(new PieceAttributes(None, Kind.None), Square.None))
    {
    }

    public override string PieceName => "NullChessPiece";

    protected override MoveResult ValidatePath(Square to, Game.Game game) =>
        new(NullMove, FromCellDoesNoteContainPiece);
}