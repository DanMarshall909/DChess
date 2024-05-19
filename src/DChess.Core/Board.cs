using static DChess.Core.Piece;

namespace DChess.Core;

using static PieceColour;
using static PieceType;

public class Board
{
    private const int MaxPieces = 32;
    public readonly PieceCollection Pieces = new();

    private readonly Cell[,] _emptyCells = GetEmptyCells();

    public Cell[,] Cells
    {
        get
        {
            var cells = _emptyCells;
            foreach (var piece in Pieces)
            {
                var coordinate = piece.Coordinate;
                cells[coordinate.File - 'a', coordinate.Rank - 1].Piece = piece;
            }

            return cells;
        }
        private
        init
        {
            Pieces.Clear();
            foreach (var cell in value)
                if (cell.Piece != null)
                    Pieces.Add(cell.Piece);
        }
    }


    public Board(Cell[,]? cells = null) => Cells = cells ?? _emptyCells;

    private static Cell[,] GetEmptyCells()
    {
        var emptyCells = new Cell[8, 8];
        for (var file = 0; file < 8; file++)
        {
            for (var rank = 0; rank < 8; rank++)
            {
                emptyCells[file, rank] = new Cell(null);
            }
        }

        return emptyCells;
    }

    public static void SetStandardLayout(Board board)
    {
        Place('a', 8, PieceType.Rook, Black);
        Place('b', 8, Knight, Black);
        Place('c', 8, Bishop, Black);
        Place('d', 8, Queen, Black);
        Place('e', 8, King, Black);
        Place('f', 8, Bishop, Black);
        Place('g', 8, Knight, Black);
        Place('h', 8, Rook, Black);

        Place('a', 7, Pawn, Black);
        Place('b', 7, Pawn, Black);
        Place('c', 7, Pawn, Black);
        Place('d', 7, Pawn, Black);
        Place('e', 7, Pawn, Black);
        Place('f', 7, Pawn, Black);
        Place('g', 7, Pawn, Black);
        Place('h', 7, Pawn, Black);

        Place('a', 2, Pawn, White);
        Place('b', 2, Pawn, White);
        Place('c', 2, Pawn, White);
        Place('d', 2, Pawn, White);
        Place('e', 2, Pawn, White);
        Place('f', 2, Pawn, White);
        Place('g', 2, Pawn, White);
        Place('h', 2, Pawn, White);

        Place('a', 1, Rook, White);
        Place('b', 1, Knight, White);
        Place('c', 1, Bishop, White);
        Place('d', 1, Queen, White);
        Place('e', 1, King, White);
        Place('f', 1, Bishop, White);
        Place('g', 1, Knight, White);
        Place('h', 1, Rook, White);
        return;

        void Place(char file, byte rank, PieceType type, PieceColour colour)
        {
            board[file, rank].Piece = new Piece(type, colour, new Coordinate(file, rank));
        }
    }

    private Cell CellAt(Coordinate coordinate) => Cells[coordinate.File - 'a', coordinate.Rank - 1];

    public Cell this[Coordinate coordinate] => CellAt(coordinate);

    public Cell this[char file, byte rank] => CellAt(new Coordinate(file, rank));

    public Cell this[string position] => CellAt(new Coordinate(position));
}