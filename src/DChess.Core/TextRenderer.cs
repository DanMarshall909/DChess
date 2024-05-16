using System.Text;

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
        char? displayChar = board.Cells[j, 7-i].Piece?.DisplayChar();
        if (displayChar is not null)
            return displayChar.Value;

        bool isOddSquare = (i + j) % 2 == 0;
        return isOddSquare ? WhiteSquare : BlackSquare;
    }
}