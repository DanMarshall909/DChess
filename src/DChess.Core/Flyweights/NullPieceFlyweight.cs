namespace DChess.Core.Flyweights;

/// <summary>
///     Represents a non-existent pieceFlyweight.
/// </summary>
public record NullPieceFlyweight : PieceFlyweight
{
    public NullPieceFlyweight(PieceContext pieceContext) : base(pieceContext)
    {
    }

    public NullPieceFlyweight()
        : base(new PieceContext(new PieceAttributes(None, Kind.None), Square.None))
    {
    }

    public override string PieceName => "NullPieceFlyweight";

    protected override MoveResult ValidatePath(Square to, Game.Game game) =>
        new(NullMove, FromCellDoesNoteContainPiece);
}