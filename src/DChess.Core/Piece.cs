namespace DChess.Core;

public record Piece(PieceType Type, PieceColor Color);


public enum PieceType
{
    Pawn
}

public enum PieceColor
{
    White
}