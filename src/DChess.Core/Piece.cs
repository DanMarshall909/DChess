namespace DChess.Core;

public record struct Piece(PieceType Type, PieceColor Colour)
{
}

public static class PieceDefinitions
{
    public static Piece WhitePawn = new(PieceType.Pawn, PieceColor.White);
}

public enum PieceType
{
    Pawn
}

public enum PieceColor
{
    White
}