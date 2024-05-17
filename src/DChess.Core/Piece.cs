namespace DChess.Core;

public readonly record struct Piece(PieceType Type, PieceColour Colour, Coordinate Coordinate)
{
    public Piece(PieceType pawn, PieceColour colour, string coordinateString) 
        : this(pawn, colour, new Coordinate(coordinateString)) { }
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
    White,
    Black
}