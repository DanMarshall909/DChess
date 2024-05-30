namespace DChess.Core.Board;

public static class BoardExtensions
{
    public static void SetStandardLayout(this Board board)
    {
        board.Clear();
        board[a8] = BlackRook;
        board[b8] = BlackKnight;
        board[c8] = BlackBishop;
        board[d8] = BlackQueen;
        board[e8] = BlackKing;
        board[f8] = BlackBishop;
        board[g8] = BlackKnight;
        board[h8] = BlackRook;

        board[a7] = BlackPawn;
        board[b7] = BlackPawn;
        board[c7] = BlackPawn;
        board[d7] = BlackPawn;
        board[e7] = BlackPawn;
        board[f7] = BlackPawn;
        board[g7] = BlackPawn;
        board[h7] = BlackPawn;

        board[a2] = WhitePawn;
        board[b2] = WhitePawn;
        board[c2] = WhitePawn;
        board[d2] = WhitePawn;
        board[e2] = WhitePawn;
        board[f2] = WhitePawn;
        board[g2] = WhitePawn;
        board[h2] = WhitePawn;

        board[a1] = WhiteRook;
        board[b1] = WhiteKnight;
        board[c1] = WhiteBishop;
        board[d1] = WhiteQueen;
        board[e1] = WhiteKing;
        board[f1] = WhiteBishop;
        board[g1] = WhiteKnight;
        board[h1] = WhiteRook;
    }
}