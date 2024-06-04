namespace DChess.Core.Pieces;

public static class NamedChessPiece
{
    public static readonly PieceProperties WhitePawn = new(PieceType.Pawn, White);
    public static readonly PieceProperties WhiteRook = new(PieceType.Rook, White);
    public static readonly PieceProperties WhiteKnight = new(PieceType.Knight, White);
    public static readonly PieceProperties WhiteBishop = new(PieceType.Bishop, White);
    public static readonly PieceProperties WhiteQueen = new(PieceType.Queen, White);
    public static readonly PieceProperties WhiteKing = new(PieceType.King, White);
    public static readonly PieceProperties BlackPawn = new(PieceType.Pawn, Black);
    public static readonly PieceProperties BlackRook = new(PieceType.Rook, Black);
    public static readonly PieceProperties BlackKnight = new(PieceType.Knight, Black);
    public static readonly PieceProperties BlackBishop = new(PieceType.Bishop, Black);
    public static readonly PieceProperties BlackQueen = new(PieceType.Queen, Black);
    public static readonly PieceProperties BlackKing = new(PieceType.King, Black);
}