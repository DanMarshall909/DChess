namespace DChess.Core.Game;

public static class GameExtensions
{
    public static void SetStandardLayout(this Game game)
    {
        game.GameState.ClearProperties();
        game.GameState.Set(a8, BlackRook);
        game.GameState.Set(b8, BlackKnight);
        game.GameState.Set(c8, BlackBishop);
        game.GameState.Set(d8, BlackQueen);
        game.GameState.Set(e8, BlackKing);
        game.GameState.Set(f8, BlackBishop);
        game.GameState.Set(g8, BlackKnight);
        game.GameState.Set(h8, BlackRook);

        game.GameState.Set(a7, BlackPawn);
        game.GameState.Set(b7, BlackPawn);
        game.GameState.Set(c7, BlackPawn);
        game.GameState.Set(d7, BlackPawn);
        game.GameState.Set(e7, BlackPawn);
        game.GameState.Set(f7, BlackPawn);
        game.GameState.Set(g7, BlackPawn);
        game.GameState.Set(h7, BlackPawn);

        game.GameState.Set(a2, WhitePawn);
        game.GameState.Set(b2, WhitePawn);
        game.GameState.Set(c2, WhitePawn);
        game.GameState.Set(d2, WhitePawn);
        game.GameState.Set(e2, WhitePawn);
        game.GameState.Set(f2, WhitePawn);
        game.GameState.Set(g2, WhitePawn);
        game.GameState.Set(h2, WhitePawn);

        game.GameState.Set(a1, WhiteRook);
        game.GameState.Set(b1, WhiteKnight);
        game.GameState.Set(c1, WhiteBishop);
        game.GameState.Set(d1, WhiteQueen);
        game.GameState.Set(e1, WhiteKing);
        game.GameState.Set(f1, WhiteBishop);
        game.GameState.Set(g1, WhiteKnight);
        game.GameState.Set(h1, WhiteRook);
    }
}