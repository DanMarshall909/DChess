using System.Text;
using DChess.Test.Unit;
using static DChess.Core.PieceColour;
using static DChess.Core.PieceType;

namespace DChess.Core;

public partial class Board
{
    private readonly Cell[,] _cells = EmptyBoard();

    private static Cell[,] EmptyBoard()
    {
        Cell[,] cells = new Cell[8, 8];
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                cells[i, j] = new Cell(null);
            }
        }

        return cells;
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

    public static Board StandardLayout()
    {
        var board = new Board();
        
        var place = (char column, int row, PieceType type, PieceColour colour) => board[column, row].Piece = new Piece(type, colour);
        
        place('a', 1, Rook, White);
        place('b', 1, Knight, White);
        place('c', 1, Bishop, White);
        place('d', 1, Queen, White);
        place('e', 1, King, White);
        place('f', 1, Bishop, White);
        place('g', 1, Knight, White);
        
        place('a', 2, Pawn, White);
        place('b', 2, Pawn, White);
        place('c', 2, Pawn, White);
        place('d', 2, Pawn, White);
        place('e', 2, Pawn, White);
        place('f', 2, Pawn, White);
        place('g', 2, Pawn, White);
        
        place('a', 7, Pawn, Black);
        place('b', 7, Pawn, Black);
        place('c', 7, Pawn, Black);
        place('d', 7, Pawn, Black);
        place('e', 7, Pawn, Black);
        place('f', 7, Pawn, Black);
        place('g', 7, Pawn, Black);
                
        place('a', 8, Rook, Black);
        place('b', 8, Knight, Black);
        place('c', 8, Bishop, Black);
        place('d', 8, Queen, Black);
        place('e', 8, King, Black);
        place('f', 8, Bishop, Black);
        place('g', 8, Knight, Black);
        

        return board;
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
