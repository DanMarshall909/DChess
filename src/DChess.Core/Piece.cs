namespace DChess.Core;

public abstract record Piece(PieceType Type, PieceColor Color);


public enum PieceType
{
    Pawn
}

public enum PieceColor
{
    White
}