namespace DChess.Core;

public record NullPiece(PieceType Type, PieceColor Color) : Piece(Type, Color)
{
}