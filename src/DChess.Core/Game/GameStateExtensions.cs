namespace DChess.Core.Game;

public static class GameStateExtensions
{
    public static void SetStandardLayout(this GameState gameState)
    {
        gameState.Board.Clear();
        gameState.Board.Place(BlackRook, a8);
        gameState.Board.Place(BlackKnight, b8);
        gameState.Board.Place(BlackBishop, c8);
        gameState.Board.Place(BlackQueen, d8);
        gameState.Board.Place(BlackKing, e8);
        gameState.Board.Place(BlackBishop, f8);
        gameState.Board.Place(BlackKnight, g8);
        gameState.Board.Place(BlackRook, h8);

        gameState.Board.Place(BlackPawn, a7);
        gameState.Board.Place(BlackPawn, b7);
        gameState.Board.Place(BlackPawn, c7);
        gameState.Board.Place(BlackPawn, d7);
        gameState.Board.Place(BlackPawn, e7);
        gameState.Board.Place(BlackPawn, f7);
        gameState.Board.Place(BlackPawn, g7);
        gameState.Board.Place(BlackPawn, h7);

        gameState.Board.Place(WhitePawn, a2);
        gameState.Board.Place(WhitePawn, b2);
        gameState.Board.Place(WhitePawn, c2);
        gameState.Board.Place(WhitePawn, d2);
        gameState.Board.Place(WhitePawn, e2);
        gameState.Board.Place(WhitePawn, f2);
        gameState.Board.Place(WhitePawn, g2);
        gameState.Board.Place(WhitePawn, h2);

        gameState.Board.Place(WhiteRook, a1);
        gameState.Board.Place(WhiteKnight, b1);
        gameState.Board.Place(WhiteBishop, c1);
        gameState.Board.Place(WhiteQueen, d1);
        gameState.Board.Place(WhiteKing, e1);
        gameState.Board.Place(WhiteBishop, f1);
        gameState.Board.Place(WhiteKnight, g1);
        gameState.Board.Place(WhiteRook, h1);
    }
}