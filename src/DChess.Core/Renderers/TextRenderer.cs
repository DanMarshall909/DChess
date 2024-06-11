﻿using System.Text;
using DChess.Core.Game;

namespace DChess.Core.Renderers;

public class TextRenderer : IBoardRenderer
{
    private const char WhiteSquare = '\u2588';
    private const char BlackSquare = '\u2591';

    public string LastRender { get; private set; } = string.Empty;

    public void Render(GameState gameState)
    {
        var result = new StringBuilder(" abcdefgh" + Environment.NewLine);
        for (byte rank = 8; rank >= 1; rank--)
        {
            result.Append(rank);

            for (var file = 'a'; file <= 'h'; file++)
            {
                char cell = RenderCell(gameState, file, rank);

                result.Append(cell);
            }

            if (rank > 1) // Don't append newline on the last line
                result.AppendLine();
        }

        LastRender = result.ToString();
    }

    private static char RenderCell(GameState gameState, char file, byte rank) => PieceChar(file, rank, gameState);

    private static char PieceChar(char file, byte rank, GameState game)
    {
        if (game.TryGetProperties(new Coordinate(file, rank), out var chessPiece))
            return DisplayChar(chessPiece!);

        bool isOddSquare = (rank + file) % 2 == 0;
        return isOddSquare ? BlackSquare : WhiteSquare;
    }

    private static char DisplayChar(Properties properties)
    {
        return properties.Colour switch
        {
            Colour.Black => properties.Type switch
            {
                PieceType.Pawn => '♙',
                PieceType.Rook => '♖',
                PieceType.Knight => '♘',
                PieceType.Bishop => '♗',
                PieceType.Queen => '♕',
                PieceType.King => '♔',
                _ => throw new ArgumentOutOfRangeException(nameof(Type), properties.Type, "Unknown properties type")
            },
            _ => properties.Type switch
            {
                PieceType.Pawn => '♟',
                PieceType.Rook => '♜',
                PieceType.Knight => '♞',
                PieceType.Bishop => '♝',
                PieceType.Queen => '♛',
                PieceType.King => '♚',
                _ => throw new ArgumentOutOfRangeException(nameof(Type), properties.Type, "Unknown properties type")
            }
        };
    }
}