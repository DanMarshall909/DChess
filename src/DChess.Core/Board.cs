using System.Text;
using DChess.Test.Unit;

namespace DChess.Core;

public partial class Board
{
    private readonly Cell[,] _cells = new Cell[8, 8];

    public Board()
    {
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                _cells[i, j] = new Cell(null);
            }
        }
    }

    public string PrettyText
    {
        get
        {
            var result = new StringBuilder();
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    const char white = '\u2588';
                    const char black = '\u2591';
                    char square = (i + j) % 2 == 0 ? white : black;
                    result.Append(square);
                    if (j < 7) // Don't append space on the last character of each line
                    {
                        result.Append(' ');
                    }
                }

                if (i < 7) // Don't append newline on the last line
                {
                    result.AppendLine();
                }
            }

            return result.ToString();
        }
    }

    public Cell CellAt(Position position) => _cells[position.File - 'a', position.Rank - 1];

    public Cell this[char column, int row] => CellAt(new Position(column, row));

    public Cell this[string position] => CellAt(new Position(position));
}

public class Cell
{
    public Cell(Piece? piece) => Piece = piece;

    public Piece? Piece { get; set; } = null;
}
