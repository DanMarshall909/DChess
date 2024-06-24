namespace DChess.Core.Game;

public static class ChessPieceTypeExtensions
{
    public static int Value(this Piece.Kind pieceKind) => pieceKind switch
    {
        Piece.Kind.Pawn => 1,
        Piece.Kind.Knight => 3,
        Piece.Kind.Bishop => 3,
        Piece.Kind.Rook => 5,
        Piece.Kind.Queen => 9,
        _ => 0
    };
}