using System.Text;
using static DChess.Core.Piece;
using static DChess.Core.Piece.PieceType;

namespace DChess.Core;

public class TextRenderer : IBoardRenderer
{
    private const char WhiteSquare = '\u2588';
    private const char BlackSquare = '\u2591';

    public string LastRender { get; private set; } = string.Empty;

    public void Render(Board board)
    {
        var result = new StringBuilder();
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                char square = PieceChar(j, i, board);

                result.Append(square);
            }

            if (i < 7) // Don't append newline on the last line
            {
                result.AppendLine();
            }
        }

        LastRender = result.ToString();
    }

    private static char PieceChar(int j, int i, Board board)
    {
        var piece = board.Cells[j, 7-i].Piece;
        if (piece != null)
            return DisplayChar(piece);

        bool isOddSquare = (i + j) % 2 == 0;
        
        return isOddSquare ? WhiteSquare : BlackSquare;
    }

    private static char DisplayChar(Piece piece)
    {
        return piece.Colour switch
        {
            PieceColour.Black => piece.Type switch
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