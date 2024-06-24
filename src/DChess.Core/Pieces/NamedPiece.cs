using DChess.Core.Game;

namespace DChess.Core.Pieces;

public static class NamedPiece
{
    public static readonly PieceAttributes WhitePawn = new(ChessPiece.Type.Pawn, White);
    public static readonly PieceAttributes WhiteRook = new(ChessPiece.Type.Rook, White);
    public static readonly PieceAttributes WhiteKnight = new(ChessPiece.Type.Knight, White);
    public static readonly PieceAttributes WhiteBishop = new(ChessPiece.Type.Bishop, White);
    public static readonly PieceAttributes WhiteQueen = new(ChessPiece.Type.Queen, White);
    public static readonly PieceAttributes WhiteKing = new(ChessPiece.Type.King, White);
    public static readonly PieceAttributes BlackPawn = new(ChessPiece.Type.Pawn, Black);
    public static readonly PieceAttributes BlackRook = new(ChessPiece.Type.Rook, Black);
    public static readonly PieceAttributes BlackKnight = new(ChessPiece.Type.Knight, Black);
    public static readonly PieceAttributes BlackBishop = new(ChessPiece.Type.Bishop, Black);
    public static readonly PieceAttributes BlackQueen = new(ChessPiece.Type.Queen, Black);
    public static readonly PieceAttributes BlackKing = new(ChessPiece.Type.King, Black);
}