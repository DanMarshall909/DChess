namespace DChess.Core.Flyweights;

public static class NamedPiece
{
    public static readonly PieceAttributes WhitePawn = new(Kind.Pawn, White);
    public static readonly PieceAttributes WhiteRook = new(Kind.Rook, White);
    public static readonly PieceAttributes WhiteKnight = new(Kind.Knight, White);
    public static readonly PieceAttributes WhiteBishop = new(Kind.Bishop, White);
    public static readonly PieceAttributes WhiteQueen = new(Kind.Queen, White);
    public static readonly PieceAttributes WhiteKing = new(Kind.King, White);
    public static readonly PieceAttributes BlackPawn = new(Kind.Pawn, Black);
    public static readonly PieceAttributes BlackRook = new(Kind.Rook, Black);
    public static readonly PieceAttributes BlackKnight = new(Kind.Knight, Black);
    public static readonly PieceAttributes BlackBishop = new(Kind.Bishop, Black);
    public static readonly PieceAttributes BlackQueen = new(Kind.Queen, Black);
    public static readonly PieceAttributes BlackKing = new(Kind.King, Black);
}