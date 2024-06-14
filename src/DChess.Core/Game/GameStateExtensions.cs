namespace DChess.Core.Game;

public static class GameStateExtensions
{
    public static void SetStandardLayout(this GameState gameState)
    {
        gameState.Clear();
        gameState.Place(BlackRook, a8);
        gameState.Place(BlackKnight, b8);
        gameState.Place(BlackBishop, c8);
        gameState.Place(BlackQueen, d8);
        gameState.Place(BlackKing, e8);
        gameState.Place(BlackBishop, f8);
        gameState.Place(BlackKnight, g8);
        gameState.Place(BlackRook, h8);

        gameState.Place(BlackPawn, a7);
        gameState.Place(BlackPawn, b7);
        gameState.Place(BlackPawn, c7);
        gameState.Place(BlackPawn, d7);
        gameState.Place(BlackPawn, e7);
        gameState.Place(BlackPawn, f7);
        gameState.Place(BlackPawn, g7);
        gameState.Place(BlackPawn, h7);

        gameState.Place(WhitePawn, a2);
        gameState.Place(WhitePawn, b2);
        gameState.Place(WhitePawn, c2);
        gameState.Place(WhitePawn, d2);
        gameState.Place(WhitePawn, e2);
        gameState.Place(WhitePawn, f2);
        gameState.Place(WhitePawn, g2);
        gameState.Place(WhitePawn, h2);

        gameState.Place(WhiteRook, a1);
        gameState.Place(WhiteKnight, b1);
        gameState.Place(WhiteBishop, c1);
        gameState.Place(WhiteQueen, d1);
        gameState.Place(WhiteKing, e1);
        gameState.Place(WhiteBishop, f1);
        gameState.Place(WhiteKnight, g1);
        gameState.Place(WhiteRook, h1);
    }
}