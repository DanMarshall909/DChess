namespace DChess.Core.Board;

public static class BoardExtensions
{
    public static void SetStandardLayout(this Board board)
    {
        board.Clear();
        board.PieceAt[a8] = BlackRook;
        board.PieceAt[b8] = BlackKnight;
        board.PieceAt[c8] = BlackBishop;
        board.PieceAt[d8] = BlackQueen;
        board.PieceAt[e8] = BlackKing;
        board.PieceAt[f8] = BlackBishop;
        board.PieceAt[g8] = BlackKnight;
        board.PieceAt[h8] = BlackRook;

        board.PieceAt[a7] = BlackPawn;
        board.PieceAt[b7] = BlackPawn;
        board.PieceAt[c7] = BlackPawn;
        board.PieceAt[d7] = BlackPawn;
        board.PieceAt[e7] = BlackPawn;
        board.PieceAt[f7] = BlackPawn;
        board.PieceAt[g7] = BlackPawn;
        board.PieceAt[h7] = BlackPawn;

        board.PieceAt[a2] = WhitePawn;
        board.PieceAt[b2] = WhitePawn;
        board.PieceAt[c2] = WhitePawn;
        board.PieceAt[d2] = WhitePawn;
        board.PieceAt[e2] = WhitePawn;
        board.PieceAt[f2] = WhitePawn;
        board.PieceAt[g2] = WhitePawn;
        board.PieceAt[h2] = WhitePawn;

        board.PieceAt[a1] = WhiteRook;
        board.PieceAt[b1] = WhiteKnight;
        board.PieceAt[c1] = WhiteBishop;
        board.PieceAt[d1] = WhiteQueen;
        board.PieceAt[e1] = WhiteKing;
        board.PieceAt[f1] = WhiteBishop;
        board.PieceAt[g1] = WhiteKnight;
        board.PieceAt[h1] = WhiteRook;
    }
}