namespace DChess.Core.Game;

/// <summary>
///     A square on a chess board denoted by a file and a rank
/// </summary>
public record struct Square
{
    /// <summary>
    ///     Creates a new Square from a string representation e.g. a1. Note that this is case sensitive
    /// </summary>
    /// <param name="squareAsString">The string representation of the square</param>
    /// <exception cref="InvalidSquareException">
    ///     Thrown if the string is not 2 characters long or if the file or rank is
    ///     out of bounds
    /// </exception>
    public Square(string squareAsString)
    {
        if (squareAsString.Length != 2)
            throw new InvalidSquareException("Square name must be 2 characters long");

        File = squareAsString[0];
        Rank = (byte)(squareAsString[1] - '0');
    }


    /// <summary>
    ///     Creates a new Square from
    /// </summary>
    /// <param name="File">The file of the square (a-h) running from left to right on a chess board</param>
    /// <param name="Rank">The rank of the square (1-8) running from bottom to top on a chess board</param>
    /// <exception cref="InvalidSquareException">
    ///     Thrown if the string is not 2 characters long or if the file or rank is
    ///     out of bounds
    /// </exception>
    public Square(char File, byte Rank)
    {
        if (!IsValid(File, Rank))
            throw new InvalidSquareException(File, Rank, "Invalid square");
        this.File = File;
        this.Rank = Rank;
    }

    public Square(byte fileIndex, byte rankIndex) => Value = (byte)(fileIndex + (rankIndex << 3));

    public Square(byte Value) => this.Value = Value;
    public static Square None => new(255);

    public static IEnumerable<Square> All { get; } = new[]
    {
        a1, b1, c1, d1, e1, f1, g1, h1,
        a2, b2, c2, d2, e2, f2, g2, h2,
        a3, b3, c3, d3, e3, f3, g3, h3,
        a4, b4, c4, d4, e4, f4, g4, h4,
        a5, b5, c5, d5, e5, f5, g5, h5,
        a6, b6, c6, d6, e6, f6, g6, h6,
        a7, b7, c7, d7, e7, f7, g7, h7,
        a8, b8, c8, d8, e8, f8, g8, h8
    };

    /// <summary>
    ///     The file of the square (a-h) running from left to right on a chess board
    /// </summary>
    /// <exception cref="InvalidSquareException">Thrown if out of bounds</exception>
    public char File
    {
        get => (char)('a' + (Value & 0b111));
        private init
        {
            if (value is < 'a' or > 'h')
                throw new InvalidSquareException(File, Rank,
                    $"File must be between 'a' and 'h' but found {File.ToString()}");

            Value = (byte)((Value & 0b11100000) | (value - 'a'));
        }
    }

    /// <summary>
    ///     The rank of the square (1-8) running from bottom to top on a chess board
    /// </summary>
    /// <exception cref="InvalidSquareException">Thrown if out of bounds</exception>
    public byte Rank
    {
        get => (byte)((Value >> 3) + 1);
        private init
        {
            if (value is < 1 or > 8)
                throw new InvalidSquareException(File, Rank, $"Rank must be between 1 and 8 but found {value}");

            Value = (byte)((Value & 0b00000111) | ((value - 1) << 3));
        }
    }

    public string AsBoard
    {
        get
        {
            const char WhiteSquare = '\u2588';
            const char BlackSquare = '\u2591';

            var sb = new StringBuilder();

            for (byte r = 8; r > 0; r--)
            {
                for (var file = 'a'; file <= 'h'; file++)
                {
                    var squares = new Square(file, r);
                    if (squares == this)
                    {
                        sb.Append('X');
                    }
                    else
                    {
                        bool isOddSquare = (r + file) % 2 == 0;
                        sb.Append(isOddSquare ? BlackSquare : WhiteSquare);
                    }
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    /// <summary>
    ///     The byte representation of the square. The first 3 bits are the rank and the last 3 bits are the file
    /// </summary>
    public byte AsByte => Value;

    public byte Value { get; init; }

    public bool Equals(Square other) =>
        Value == other.Value;


    public static Square FromZeroOffset(int fileArrayOffset, int rankArrayOffset)
        => new((byte)((fileArrayOffset & 0b111) | ((rankArrayOffset & 0b111) << 3)));

    public override int GetHashCode() => AsByte;

    /// <summary>
    ///     Creates a new Square from a byte representation
    /// </summary>
    /// <param name="byteSquare">The byte representation of the square</param>
    /// <returns></returns>
    public static Square From(byte byteSquare) =>
        new((char)('a' + (byteSquare & 0b111)), (byte)((byteSquare >> 3) + 1));

    public override string ToString() => this == NullSquare ? "Null Square" : $"{File}{Rank}";

    public static bool IsValid(char file, byte rank) => file is >= 'a' and <= 'h' && rank is >= 1 and <= 8;

    /// <summary>
    ///     Returns a new Square that is offset by the given offset
    /// </summary>
    /// <param name="offset">the offset to apply</param>
    /// <returns></returns>
    public Square OffsetBy(MoveOffset offset) =>
        new((char)(File + offset.FileOffset), (byte)(Rank + offset.RankOffset));


    /// <summary>
    ///     Returns true if the given offset is a valid square on a chess board
    /// </summary>
    /// <param name="offset">the offset to check</param>
    /// <returns></returns>
    public bool IsValidOffset(MoveOffset offset) =>
        IsValid((char)(File + offset.FileOffset), (byte)(Rank + offset.RankOffset));

    public bool TryApplyOffset(MoveOffset moveOffset, out Square newSquare)
    {
        newSquare = IsValidOffset(moveOffset)
            ? OffsetBy(moveOffset)
            : None;
        return newSquare != None;
    }

    public readonly void Deconstruct(out byte square)
    {
        square = Value;
    }
}

public static class squareExtenions
{
    public static Square ToSquare(this string colourString)
    {
        char file = char.ToLower(colourString[0]);
        byte rank = byte.Parse(colourString[1].ToString());

        return new Square(file, rank);
    }
}