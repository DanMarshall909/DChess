namespace DChess.Core.Game;

public static class ChessPieceTypeExtensions
{
    public static int Value(this ChessPiece.Type pieceType) => pieceType switch
    {
        ChessPiece.Type.Pawn => 1,
        ChessPiece.Type.Knight => 3,
        ChessPiece.Type.Bishop => 3,
        ChessPiece.Type.Rook => 5,
        ChessPiece.Type.Queen => 9,
        _ => 0
    };
}