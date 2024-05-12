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

    public void PlacePiece(Piece piece, Position position)
    {
        int row = position.Row;
        char column = position.Column;
        if (column < 'a' || column > 'g' || row < 1 || row > 8)
        {
            throw new ArgumentOutOfRangeException($"The '{column}{row}' is not a valid position");
        }
    }

    public Piece GetPiece(Position position) => new Pawn(PieceType.Pawn, PieceColor.White);

    public Piece this[char column, int row]
    {
        get => GetPiece(new Position(column, row));
        set => PlacePiece(value, new Position(column, row));
    }
}

public record Pawn(PieceType Type, PieceColor Color) : Piece(Type, Color);
