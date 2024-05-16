namespace DChess.Core;

public readonly record struct Piece(PieceType Type, PieceColour Colour)
{
}

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
    White, Black
}