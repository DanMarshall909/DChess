namespace DChess.Core.Game;

public static class PieceTypeExtensions
{
    public static int Value(this PieceType pieceType) => pieceType switch
    {
        PieceType.Pawn => 1,
        PieceType.Knight => 3,
        PieceType.Bishop => 3,
        PieceType.Rook => 5,
        PieceType.Queen => 9,
        _ => 0
    };
}