namespace DChess.Core;

public readonly struct Piece(Piece.PieceType type, Piece.PieceColour colour)
{
    public PieceType Type { get; private init; } = type;
    public PieceColour Colour { get; private init; } = colour;

    public enum PieceType
    {
        Pawn,
        Rook,
        Knight,
        Bishop,
        Queen,
        King
    }

    public enum PieceColour
    {
        White,
        Black
    }
}

