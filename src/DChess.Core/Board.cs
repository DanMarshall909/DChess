using DChess.Test.Unit;
using static DChess.Core.PieceColour;
using static DChess.Core.PieceType;

namespace DChess.Core;

public class Board
{
    public Cell[,] Cells { get; private set; } = null!;

    public Board(Cell[,]? cells = null)
    {
        if (cells == null
            || cells.GetLength(0) != 8
            || cells.GetLength(1) != 8)
        {
            ClearBoard();
        }
        else
            Cells = cells;
    }

    public void ClearBoard()
    {
        Cells = new Cell[8, 8];
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                Cells[i, j] = new Cell(null);
            }
        }
    }

    public void SetStandardLayout()
    {
        var place = (char column, int row, PieceType type, PieceColour colour) =>
            this[column, row].Piece = new Piece(type, colour);

        place('a', 8, Rook, Black);
        place('b', 8, Knight, Black);
        place('c', 8, Bishop, Black);
        place('d', 8, Queen, Black);
        place('e', 8, King, Black);
        place('f', 8, Bishop, Black);
        place('g', 8, Knight, Black);
        place('h', 8, Rook, Black);

        place('a', 7, Pawn, Black);
        place('b', 7, Pawn, Black);
        place('c', 7, Pawn, Black);
        place('d', 7, Pawn, Black);
        place('e', 7, Pawn, Black);
        place('f', 7, Pawn, Black);
        place('g', 7, Pawn, Black);
        place('h', 7, Pawn, Black);

        place('a', 2, Pawn, White);
        place('b', 2, Pawn, White);
        place('c', 2, Pawn, White);
        place('d', 2, Pawn, White);
        place('e', 2, Pawn, White);
        place('f', 2, Pawn, White);
        place('g', 2, Pawn, White);
        place('h', 2, Pawn, White);

        place('a', 1, Rook, White);
        place('b', 1, Knight, White);
        place('c', 1, Bishop, White);
        place('d', 1, Queen, White);
        place('e', 1, King, White);
        place('f', 1, Bishop, White);
        place('g', 1, Knight, White);
        place('h', 1, Rook, White);
    }

    public Cell CellAt(Coordinate coordinate) => Cells[coordinate.File - 'a', coordinate.Rank - 1];

    public Cell this[char column, int row] => CellAt(new Coordinate(column, row));

    public Cell this[string position] => CellAt(new Coordinate(position));
}

public class Cell
{
    public Cell(Piece? piece) => Piece = piece;

    public Piece? Piece { get; set; } = null;
}