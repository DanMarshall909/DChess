using System.Text;
using DChess.Test.Unit;

namespace DChess.Core;

public class Board
{
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

    public void PlacePiece(Piece? piece, Position position)
    {
    }

    public Piece? GetPiece(Position position)
    {
        int fileIndex = position.File - 'a';
        int rankIndex = position.Rank - 1;
        var cell = Cells[fileIndex, rankIndex];
        return cell.Piece;
    }

    public Cell[,]  Cells { get; set; } = new Cell[8, 8];

    public Piece? this[char column, int row]
    {
        get => GetPiece(new Position(column, row));
        set => PlacePiece(value, new Position(column, row));
    }
}

public record struct Cell
{
    public Piece? Piece { get; set; }
}

public record Pawn(PieceType Type, PieceColor Color) : Piece(Type, Color);
