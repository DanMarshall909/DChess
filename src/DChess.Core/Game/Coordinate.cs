using System.Text;
using DChess.Core.Errors;
using DChess.Core.Moves;

namespace DChess.Core.Game;

public record struct Coordinate
{
    /// <summary>
    ///     Creates a new Coordinate from a string representation e.g. a1. Note that this is case sensitive
    /// </summary>
    /// <param name="coordinateAsString">The string representation of the coordinate</param>
    /// <exception cref="InvalidCoordinateException">
    ///     Thrown if the string is not 2 characters long or if the file or rank is
    ///     out of bounds
    /// </exception>
    public Coordinate(string coordinateAsString)
    {
        if (coordinateAsString.Length != 2)
            throw new InvalidCoordinateException("Coordinate name must be 2 characters long");

        File = coordinateAsString[0];
        Rank = (byte)(coordinateAsString[1] - '0');
    }


    /// <summary>
    ///     Creates a new Coordinate from
    /// </summary>
    /// <param name="File">The file of the coordinate (a-h) running from left to right on a chess board</param>
    /// <param name="Rank">The rank of the coordinate (1-8) running from bottom to top on a chess board</param>
    /// <exception cref="InvalidCoordinateException">
    ///     Thrown if the string is not 2 characters long or if the file or rank is
    ///     out of bounds
    /// </exception>
    public Coordinate(char File, byte Rank)
    {
        if (!IsValid(File, Rank))
            throw new InvalidCoordinateException(File, Rank, "Invalid coordinate");
        this.File = File;
        this.Rank = Rank;
    }

    public Coordinate(byte fileIndex, byte rankIndex) => Value = (byte)(fileIndex + (rankIndex << 3));

    public Coordinate(byte Value) => this.Value = Value;
    public static Coordinate None => new(255);

    public static IEnumerable<Coordinate> All { get; } = new[]
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
    ///     The file of the coordinate (a-h) running from left to right on a chess board
    /// </summary>
    /// <exception cref="InvalidCoordinateException">Thrown if out of bounds</exception>
    public char File
    {
        get => (char)('a' + (Value & 0b111));
        private init
        {
            if (value is < 'a' or > 'h')
                throw new InvalidCoordinateException(File, Rank,
                    $"File must be between 'a' and 'h' but found {File.ToString()}");

            Value = (byte)((Value & 0b11100000) | (value - 'a'));
        }
    }

    /// <summary>
    ///     The rank of the coordinate (1-8) running from bottom to top on a chess board
    /// </summary>
    /// <exception cref="InvalidCoordinateException">Thrown if out of bounds</exception>
    public byte Rank
    {
        get => (byte)((Value >> 3) + 1);
        private init
        {
            if (value is < 1 or > 8)
                throw new InvalidCoordinateException(File, Rank, $"Rank must be between 1 and 8 but found {value}");

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
                    var coordinates = new Coordinate(file, r);
                    if (coordinates == this)
                    {
                        sb.Append("X");
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
    ///     The byte representation of the coordinate. The first 3 bits are the rank and the last 3 bits are the file
    /// </summary>
    public byte AsByte => Value;

    public byte Value { get; set; }

    public bool Equals(Coordinate other) =>
        Value == other.Value;


    public static Coordinate FromZeroOffset(int fileArrayOffset, int rankArrayOffset)
        => new((byte)((fileArrayOffset & 0b111) | ((rankArrayOffset & 0b111) << 3)));

    public override int GetHashCode() => AsByte;

    /// <summary>
    ///     Creates a new Coordinate from a byte representation
    /// </summary>
    /// <param name="byteCoordinate">The byte representation of the coordinate</param>
    /// <returns></returns>
    public static Coordinate From(byte byteCoordinate) =>
        new((char)('a' + (byteCoordinate & 0b111)), (byte)((byteCoordinate >> 3) + 1));

    public override string ToString() => this == NullCoordinate ? "Null Coordinate" : $"{File}{Rank}";

    public static bool IsValid(char file, byte rank) => file is >= 'a' and <= 'h' && rank is >= 1 and <= 8;
    public bool IsValid() => IsValid(File, Rank);

    /// <summary>
    ///     Returns a new Coordinate that is offset by the given offset
    /// </summary>
    /// <param name="offset">the offset to apply</param>
    /// <returns></returns>
    public Coordinate OffsetBy(MoveOffset moveOffset) =>
        new((char)(File + moveOffset.FileOffset), (byte)(Rank + moveOffset.RankOffset));


    /// <summary>
    ///     Returns true if the given offset is a valid coordinate on a chess board
    /// </summary>
    /// <param name="dFile"></param>
    /// <param name="dRank"></param>
    /// <returns></returns>
    public bool IsValidOffset(MoveOffset moveOffset) =>
        IsValid((char)(File + moveOffset.FileOffset), (byte)(Rank + moveOffset.RankOffset));

    public bool TryApplyOffset(MoveOffset moveOffset, out Coordinate newCoordinate)
    {
        if (IsValidOffset(moveOffset))
            newCoordinate = OffsetBy(moveOffset);
        else
            newCoordinate = None;
        return newCoordinate != None;
    }

    public readonly void Deconstruct(out byte Value)
    {
        Value = this.Value;
    }
}

public static class CoordinateExtenions
{
    public static Coordinate ToCoordinate(this string colourString)
    {
        char file = char.ToLower(colourString[0]);
        byte rank = byte.Parse(colourString[1].ToString());

        return new Coordinate(file, rank);
    }
}