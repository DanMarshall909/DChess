namespace DChess.Core;

public static class PieceDefinitions
{
    public static Piece WhitePawn = new(PieceType.Pawn, PieceColour.White);
    public static Piece WhiteRook = new(PieceType.Rook, PieceColour.White);
    public static Piece WhiteKnight = new(PieceType.Knight, PieceColour.White);
    public static Piece WhiteBishop = new(PieceType.Bishop, PieceColour.White);
    public static Piece WhiteQueen = new(PieceType.Queen, PieceColour.White);
    public static Piece WhiteKing = new(PieceType.King, PieceColour.White);

    public static Piece BlackPawn = new(PieceType.Pawn, PieceColour.Black);
    public static Piece BlackRook = new(PieceType.Rook, PieceColour.Black);
    public static Piece BlackKnight = new(PieceType.Knight, PieceColour.Black);
    public static Piece BlackBishop = new(PieceType.Bishop, PieceColour.Black);
    public static Piece BlackQueen = new(PieceType.Queen, PieceColour.Black);
    public static Piece BlackKing = new(PieceType.King, PieceColour.Black);
}