namespace DChess.Core.Flyweights;

public record struct PieceContext(PieceAttributes PieceAttributes, Square Square)
{
    public PieceContext(Square Square, PieceAttributes PieceAttributes) : this(PieceAttributes, Square) { }
};