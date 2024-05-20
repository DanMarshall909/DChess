using System.Text;
using static DChess.Core.Piece.PieceColour;
using static DChess.Core.Piece.PieceType;

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
        if (board.Pieces.TryGetPiece(new Coordinate(file, rank), out var piece))
            return DisplayChar(piece);

        bool isOddSquare = (rank + file) % 2 == 0;
        return isOddSquare ? BlackSquare : WhiteSquare;
    }

    private static char DisplayChar(Piece piece)
    {
        return piece.Colour switch
        {
            Black => piece.Type switch
            {
                Pawn => '♙',
                Rook => '♖',
                Knight => '♘',
                Bishop => '♗',
                Queen => '♕',
                King => '♔',
                _ => throw new ArgumentOutOfRangeException(nameof(Type), piece.Type, "Unknown piece type")
            },
            _ => piece.Type switch
            {
                Pawn => '♟',
                Rook => '♜',
                Knight => '♞',
                Bishop => '♝',
                Queen => '♛',
                King => '♚',
                _ => throw new ArgumentOutOfRangeException(nameof(Type), piece.Type, "Unknown piece type")
            }
        };
    }
}