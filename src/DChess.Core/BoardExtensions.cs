using DChess.Core.Pieces;

namespace DChess.Core;

public static class BoardExtensions
{
    public static void SetStandardLayout(this Board board)
    {
        board.Clear();
        board[a8] = ChessPiece.BlackRook;
        board[b8] = ChessPiece.BlackKnight;
        board[c8] = ChessPiece.BlackBishop;
        board[d8] = ChessPiece.BlackQueen;
        board[e8] = ChessPiece.BlackKing;
        board[f8] = ChessPiece.BlackBishop;
        board[g8] = ChessPiece.BlackKnight;
        board[h8] = ChessPiece.BlackRook;

        board[a7] = ChessPiece.BlackPawn;
        board[b7] = ChessPiece.BlackPawn;
        board[c7] = ChessPiece.BlackPawn;
        board[d7] = ChessPiece.BlackPawn;
        board[e7] = ChessPiece.BlackPawn;
        board[f7] = ChessPiece.BlackPawn;
        board[g7] = ChessPiece.BlackPawn;
        board[h7] = ChessPiece.BlackPawn;

        board[a2] = ChessPiece.WhitePawn;
        board[b2] = ChessPiece.WhitePawn;
        board[c2] = ChessPiece.WhitePawn;
        board[d2] = ChessPiece.WhitePawn;
        board[e2] = ChessPiece.WhitePawn;
        board[f2] = ChessPiece.WhitePawn;
        board[g2] = ChessPiece.WhitePawn;
        board[h2] = ChessPiece.WhitePawn;

        board[a1] = ChessPiece.WhiteRook;
        board[b1] = ChessPiece.WhiteKnight;
        board[c1] = ChessPiece.WhiteBishop;
        board[d1] = ChessPiece.WhiteQueen;
        board[e1] = ChessPiece.WhiteKing;
        board[f1] = ChessPiece.WhiteBishop;
        board[g1] = ChessPiece.WhiteKnight;
        board[h1] = ChessPiece.WhiteRook;
    }
}