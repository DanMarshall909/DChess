namespace DChess.Core.Board;

public static class BoardExtensions
{
    public static void SetStandardLayout(this Board board)
    {
        board.Clear();
        board.Set(a8, BlackRook);
        board.Set(b8, BlackKnight);
        board.Set(c8, BlackBishop);
        board.Set(d8, BlackQueen);
        board.Set(e8, BlackKing);
        board.Set(f8, BlackBishop);
        board.Set(g8, BlackKnight);
        board.Set(h8, BlackRook);

        board.Set(a7, BlackPawn);
        board.Set(b7, BlackPawn);
        board.Set(c7, BlackPawn);
        board.Set(d7, BlackPawn);
        board.Set(e7, BlackPawn);
        board.Set(f7, BlackPawn);
        board.Set(g7, BlackPawn);
        board.Set(h7, BlackPawn);

        board.Set(a2, WhitePawn);
        board.Set(b2, WhitePawn);
        board.Set(c2, WhitePawn);
        board.Set(d2, WhitePawn);
        board.Set(e2, WhitePawn);
        board.Set(f2, WhitePawn);
        board.Set(g2, WhitePawn);
        board.Set(h2, WhitePawn);

        board.Set(a1, WhiteRook);
        board.Set(b1, WhiteKnight);
        board.Set(c1, WhiteBishop);
        board.Set(d1, WhiteQueen);
        board.Set(e1, WhiteKing);
        board.Set(f1, WhiteBishop);
        board.Set(g1, WhiteKnight);
        board.Set(h1, WhiteRook);
    }
}