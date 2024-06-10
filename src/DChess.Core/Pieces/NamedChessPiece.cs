using DChess.Core.Game;

namespace DChess.Core.Pieces;

public static class NamedChessPiece
{
    public static readonly Properties WhitePawn = new(PieceType.Pawn, White);
    public static readonly Properties WhiteRook = new(PieceType.Rook, White);
    public static readonly Properties WhiteKnight = new(PieceType.Knight, White);
    public static readonly Properties WhiteBishop = new(PieceType.Bishop, White);
    public static readonly Properties WhiteQueen = new(PieceType.Queen, White);
    public static readonly Properties WhiteKing = new(PieceType.King, White);
    public static readonly Properties BlackPawn = new(PieceType.Pawn, Black);
    public static readonly Properties BlackRook = new(PieceType.Rook, Black);
    public static readonly Properties BlackKnight = new(PieceType.Knight, Black);
    public static readonly Properties BlackBishop = new(PieceType.Bishop, Black);
    public static readonly Properties BlackQueen = new(PieceType.Queen, Black);
    public static readonly Properties BlackKing = new(PieceType.King, Black);
}