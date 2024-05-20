namespace DChess.Core;

public struct Piece
{
    public PieceType Type { get; private set; }
    public PieceColour Colour { get; private set; }
    public Coordinate Coordinate { get; private set; }

    public Piece(PieceType type, PieceColour colour, Coordinate position)
    {
        Initialize(type, colour, position);
    }

    public Piece(PieceType type, PieceColour colour, string coordinateString)
    {
        Initialize(type, colour, new Coordinate(coordinateString));
    }

    public void Initialize(PieceType type, PieceColour colour, Coordinate position)
    {
        Type = type;
        Colour = colour;
        Coordinate = position;
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
}

