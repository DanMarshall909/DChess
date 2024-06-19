namespace DChess.Core.Game;

public static class GameExtensions
{
    public static void SetStandardLayout(this Game game)
    {
        game.Board.Clear();
        game.Board.Place(BlackRook, a8);
        game.Board.Place(BlackKnight, b8);
        game.Board.Place(BlackBishop, c8);
        game.Board.Place(BlackQueen, d8);
        game.Board.Place(BlackKing, e8);
        game.Board.Place(BlackBishop, f8);
        game.Board.Place(BlackKnight, g8);
        game.Board.Place(BlackRook, h8);

        game.Board.Place(BlackPawn, a7);
        game.Board.Place(BlackPawn, b7);
        game.Board.Place(BlackPawn, c7);
        game.Board.Place(BlackPawn, d7);
        game.Board.Place(BlackPawn, e7);
        game.Board.Place(BlackPawn, f7);
        game.Board.Place(BlackPawn, g7);
        game.Board.Place(BlackPawn, h7);

        game.Board.Place(WhitePawn, a2);
        game.Board.Place(WhitePawn, b2);
        game.Board.Place(WhitePawn, c2);
        game.Board.Place(WhitePawn, d2);
        game.Board.Place(WhitePawn, e2);
        game.Board.Place(WhitePawn, f2);
        game.Board.Place(WhitePawn, g2);
        game.Board.Place(WhitePawn, h2);

        game.Board.Place(WhiteRook, a1);
        game.Board.Place(WhiteKnight, b1);
        game.Board.Place(WhiteBishop, c1);
        game.Board.Place(WhiteQueen, d1);
        game.Board.Place(WhiteKing, e1);
        game.Board.Place(WhiteBishop, f1);
        game.Board.Place(WhiteKnight, g1);
        game.Board.Place(WhiteRook, h1);
    }
}