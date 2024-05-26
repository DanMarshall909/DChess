namespace DChess.Core.Pieces;

public readonly struct ChessPiece(PieceType type, Colour colour)
{
    public static readonly ChessPiece WhitePawn = new ChessPiece(PieceType.Pawn, Colour.White);
    public static readonly ChessPiece WhiteRook = new ChessPiece(PieceType.Rook, Colour.White);
    public static readonly ChessPiece WhiteKnight = new ChessPiece(PieceType.Knight, Colour.White);
    public static readonly ChessPiece WhiteBishop = new ChessPiece(PieceType.Bishop, Colour.White);
    public static readonly ChessPiece WhiteQueen = new ChessPiece(PieceType.Queen, Colour.White);
    public static readonly ChessPiece WhiteKing = new ChessPiece(PieceType.King, Colour.White);
    public static readonly ChessPiece BlackPawn = new ChessPiece(PieceType.Pawn, Colour.Black);
    public static readonly ChessPiece BlackRook = new ChessPiece(PieceType.Rook, Colour.Black);
    public static readonly ChessPiece BlackKnight = new ChessPiece(PieceType.Knight, Colour.Black);
    public static readonly ChessPiece BlackBishop = new ChessPiece(PieceType.Bishop, Colour.Black);
    public static readonly ChessPiece BlackQueen = new ChessPiece(PieceType.Queen, Colour.Black);
    public static readonly ChessPiece BlackKing = new ChessPiece(PieceType.King, Colour.Black);

    public PieceType Type { get; private init; } = type;
    public Colour Colour { get; private init; } = colour;
}
