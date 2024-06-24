using DChess.Core.Game;

namespace DChess.Core.Pieces;

public static class NamedPiece
{
    public static readonly PieceAttributes WhitePawn = new(PieceType.Pawn, White);
    public static readonly PieceAttributes WhiteRook = new(PieceType.Rook, White);
    public static readonly PieceAttributes WhiteKnight = new(PieceType.Knight, White);
    public static readonly PieceAttributes WhiteBishop = new(PieceType.Bishop, White);
    public static readonly PieceAttributes WhiteQueen = new(PieceType.Queen, White);
    public static readonly PieceAttributes WhiteKing = new(PieceType.King, White);
    public static readonly PieceAttributes BlackPawn = new(PieceType.Pawn, Black);
    public static readonly PieceAttributes BlackRook = new(PieceType.Rook, Black);
    public static readonly PieceAttributes BlackKnight = new(PieceType.Knight, Black);
    public static readonly PieceAttributes BlackBishop = new(PieceType.Bishop, Black);
    public static readonly PieceAttributes BlackQueen = new(PieceType.Queen, Black);
    public static readonly PieceAttributes BlackKing = new(PieceType.King, Black);
}