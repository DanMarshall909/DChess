namespace DChess.Core.Board;

public static class BoardExtensions
{
    public static void SetStandardLayout(this Board board)
    {
        board.Clear();
        board.ChessPieces[a8] = BlackRook;
        board.ChessPieces[b8] = BlackKnight;
        board.ChessPieces[c8] = BlackBishop;
        board.ChessPieces[d8] = BlackQueen;
        board.ChessPieces[e8] = BlackKing;
        board.ChessPieces[f8] = BlackBishop;
        board.ChessPieces[g8] = BlackKnight;
        board.ChessPieces[h8] = BlackRook;

        board.ChessPieces[a7] = BlackPawn;
        board.ChessPieces[b7] = BlackPawn;
        board.ChessPieces[c7] = BlackPawn;
        board.ChessPieces[d7] = BlackPawn;
        board.ChessPieces[e7] = BlackPawn;
        board.ChessPieces[f7] = BlackPawn;
        board.ChessPieces[g7] = BlackPawn;
        board.ChessPieces[h7] = BlackPawn;

        board.ChessPieces[a2] = WhitePawn;
        board.ChessPieces[b2] = WhitePawn;
        board.ChessPieces[c2] = WhitePawn;
        board.ChessPieces[d2] = WhitePawn;
        board.ChessPieces[e2] = WhitePawn;
        board.ChessPieces[f2] = WhitePawn;
        board.ChessPieces[g2] = WhitePawn;
        board.ChessPieces[h2] = WhitePawn;

        board.ChessPieces[a1] = WhiteRook;
        board.ChessPieces[b1] = WhiteKnight;
        board.ChessPieces[c1] = WhiteBishop;
        board.ChessPieces[d1] = WhiteQueen;
        board.ChessPieces[e1] = WhiteKing;
        board.ChessPieces[f1] = WhiteBishop;
        board.ChessPieces[g1] = WhiteKnight;
        board.ChessPieces[h1] = WhiteRook;
    }
}