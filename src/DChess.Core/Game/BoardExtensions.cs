namespace DChess.Core.Game;

public static class BoardExtensions
{
    public static void SetStandardLayout(this Board board)
    {
        board.Clear();
        board.Place(BlackRook, a8);
        board.Place(BlackKnight, b8);
        board.Place(BlackBishop, c8);
        board.Place(BlackQueen, d8);
        board.Place(BlackKing, e8);
        board.Place(BlackBishop, f8);
        board.Place(BlackKnight, g8);
        board.Place(BlackRook, h8);

        board.Place(BlackPawn, a7);
        board.Place(BlackPawn, b7);
        board.Place(BlackPawn, c7);
        board.Place(BlackPawn, d7);
        board.Place(BlackPawn, e7);
        board.Place(BlackPawn, f7);
        board.Place(BlackPawn, g7);
        board.Place(BlackPawn, h7);

        board.Place(WhitePawn, a2);
        board.Place(WhitePawn, b2);
        board.Place(WhitePawn, c2);
        board.Place(WhitePawn, d2);
        board.Place(WhitePawn, e2);
        board.Place(WhitePawn, f2);
        board.Place(WhitePawn, g2);
        board.Place(WhitePawn, h2);

        board.Place(WhiteRook, a1);
        board.Place(WhiteKnight, b1);
        board.Place(WhiteBishop, c1);
        board.Place(WhiteQueen, d1);
        board.Place(WhiteKing, e1);
        board.Place(WhiteBishop, f1);
        board.Place(WhiteKnight, g1);
        board.Place(WhiteRook, h1);
    }
}