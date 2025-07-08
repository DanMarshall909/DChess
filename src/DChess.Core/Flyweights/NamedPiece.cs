namespace DChess.Core.Flyweights;

public static class NamedPiece
{
    public static readonly PieceAttributes WhitePawn = new(White, Kind.Pawn);
    public static readonly PieceAttributes WhiteRook = new(White, Kind.Rook);
    public static readonly PieceAttributes WhiteKnight = new(White, Kind.Knight);
    public static readonly PieceAttributes WhiteBishop = new(White, Kind.Bishop);
    public static readonly PieceAttributes WhiteQueen = new(White, Kind.Queen);
    public static readonly PieceAttributes WhiteKing = new(White, Kind.King);
    public static readonly PieceAttributes BlackPawn = new(Black, Kind.Pawn);
    public static readonly PieceAttributes BlackRook = new(Black, Kind.Rook);
    public static readonly PieceAttributes BlackKnight = new(Black, Kind.Knight);
    public static readonly PieceAttributes BlackBishop = new(Black, Kind.Bishop);
    public static readonly PieceAttributes BlackQueen = new(Black, Kind.Queen);
    public static readonly PieceAttributes BlackKing = new(Black, Kind.King);
}