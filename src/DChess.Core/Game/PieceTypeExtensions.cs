namespace DChess.Core.Game;

public static class ChessPieceTypeExtensions
{
    public static int Value(this Kind pieceKind) => pieceKind switch
    {
        Kind.Pawn => 1,
        Kind.Knight => 3,
        Kind.Bishop => 3,
        Kind.Rook => 5,
        Kind.Queen => 9,
        _ => 0
    };
}