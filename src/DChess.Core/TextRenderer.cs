﻿using System.Text;
using DChess.Core.Pieces;
using static DChess.Core.Pieces.Colour;

namespace DChess.Core;

public class TextRenderer : IBoardRenderer
{
    private const char WhiteSquare = '\u2588';
    private const char BlackSquare = '\u2591';

    public string LastRender { get; private set; } = string.Empty;

    public void Render(Board board)
    {
        var result = new StringBuilder(" abcdefgh" + Environment.NewLine);
        for (byte rank = 8; rank >= 1; rank--)
        {
            result.Append(rank);

            for (var file = 'a'; file <= 'h'; file++)
            {
                char square = PieceChar(file, rank, board);

                result.Append(square);
            }

            if (rank > 1) // Don't append newline on the last line
            {
                result.AppendLine();
            }
        }

        LastRender = result.ToString();
    }

    private static char PieceChar(char file, byte rank, Board board)
    {
        if (board.TryGetValue(new Coordinate(file, rank), out var pieceStruct))
            return DisplayChar(pieceStruct!);

        bool isOddSquare = (rank + file) % 2 == 0;
        return isOddSquare ? BlackSquare : WhiteSquare;
    }

    private static char DisplayChar(ChessPiece chessPiece)
    {
        return chessPiece.Colour switch
        {
            Black => chessPiece.Type switch
            {
                PieceType.Pawn => '♙',
                PieceType.Rook => '♖',
                PieceType.Knight => '♘',
                PieceType.Bishop => '♗',
                PieceType.Queen => '♕',
                PieceType.King => '♔',
                _ => throw new ArgumentOutOfRangeException(nameof(Type), chessPiece.Type, "Unknown piece type")
            },
            _ => chessPiece.Type switch
            {
                PieceType.Pawn => '♟',
                PieceType.Rook => '♜',
                PieceType.Knight => '♞',
                PieceType.Bishop => '♝',
                PieceType.Queen => '♛',
                PieceType.King => '♚',
                _ => throw new ArgumentOutOfRangeException(nameof(Type), chessPiece.Type, "Unknown piece type")
            }
        };
    }
}