namespace DChess.Core.Game;

public static class GameExtensions
{
    public static void SetStandardLayout(this Game game)
    {
        game.GameState.ClearProperties();
        game.GameState.Place(BlackRook, a8);
        game.GameState.Place(BlackKnight, b8);
        game.GameState.Place(BlackBishop, c8);
        game.GameState.Place(BlackQueen, d8);
        game.GameState.Place(BlackKing, e8);
        game.GameState.Place(BlackBishop, f8);
        game.GameState.Place(BlackKnight, g8);
        game.GameState.Place(BlackRook, h8);

        game.GameState.Place(BlackPawn, a7);
        game.GameState.Place(BlackPawn, b7);
        game.GameState.Place(BlackPawn, c7);
        game.GameState.Place(BlackPawn, d7);
        game.GameState.Place(BlackPawn, e7);
        game.GameState.Place(BlackPawn, f7);
        game.GameState.Place(BlackPawn, g7);
        game.GameState.Place(BlackPawn, h7);

        game.GameState.Place(WhitePawn, a2);
        game.GameState.Place(WhitePawn, b2);
        game.GameState.Place(WhitePawn, c2);
        game.GameState.Place(WhitePawn, d2);
        game.GameState.Place(WhitePawn, e2);
        game.GameState.Place(WhitePawn, f2);
        game.GameState.Place(WhitePawn, g2);
        game.GameState.Place(WhitePawn, h2);

        game.GameState.Place(WhiteRook, a1);
        game.GameState.Place(WhiteKnight, b1);
        game.GameState.Place(WhiteBishop, c1);
        game.GameState.Place(WhiteQueen, d1);
        game.GameState.Place(WhiteKing, e1);
        game.GameState.Place(WhiteBishop, f1);
        game.GameState.Place(WhiteKnight, g1);
        game.GameState.Place(WhiteRook, h1);
    }
}