namespace DChess.Core.Pieces;

public readonly struct ChessPiece(PieceType type, Colour colour)
{
    public static readonly ChessPiece WhitePawn = new(PieceType.Pawn, White);
    public static readonly ChessPiece WhiteRook = new(PieceType.Rook, White);
    public static readonly ChessPiece WhiteKnight = new(PieceType.Knight, White);
    public static readonly ChessPiece WhiteBishop = new(PieceType.Bishop, White);
    public static readonly ChessPiece WhiteQueen = new(PieceType.Queen, White);
    public static readonly ChessPiece WhiteKing = new(PieceType.King, White);
    public static readonly ChessPiece BlackPawn = new(PieceType.Pawn, Black);
    public static readonly ChessPiece BlackRook = new(PieceType.Rook, Black);
    public static readonly ChessPiece BlackKnight = new(PieceType.Knight, Black);
    public static readonly ChessPiece BlackBishop = new(PieceType.Bishop, Black);
    public static readonly ChessPiece BlackQueen = new(PieceType.Queen, Black);
    public static readonly ChessPiece BlackKing = new(PieceType.King, Black);

    public PieceType Type { get; private init; } = type;
    public Colour Colour { get; private init; } = colour;
}