namespace DChess.Core.Board;

public readonly struct PieceProperties(PieceType type, Colour colour)
{
    public PieceType Type { get; private init; } = type;
    public Colour Colour { get; private init; } = colour;
}