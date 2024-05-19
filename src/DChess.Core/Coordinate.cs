using System.Runtime.CompilerServices;

namespace DChess.Core;

public readonly record struct Coordinate
{
    private readonly char _file;
    private readonly byte _rank;

    public override string ToString() => $"{File}{Rank}";
    
    public byte Index => IndexOf(_file, _rank);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte IndexOf(char file, byte rank) => (byte)((file - 'a' << 3) + rank - 1);

    public Coordinate(string positionName)
    {
        if (positionName.Length != 2)
        {
            throw new ArgumentException("Position must be 2 characters long");
        }

        File = positionName[0];
        Rank = (byte)(positionName[1] - '0');
    }

    public Coordinate(char File, byte Rank)
    {
        this.File = File;
        this.Rank = Rank;
    }

    public char File
    {
        get => _file;
        private init
        {
            if (value is < 'a' or > 'h')
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"File must be between 'a' and 'g' but found {_file.ToString()}");
            }

            _file = value;
        }
    }

    public byte Rank
    {
        get => _rank;
        private init
        {
            if (value is < 1 or > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Rank must be between 1 and 8");
            }

            _rank = value;
        }
    }
}

public static class CoordinateExtensions
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable UnusedMember.Global
    public static readonly Coordinate a1 = new('a', 1);
    public static readonly Coordinate a2 = new('a', 2);
    public static readonly Coordinate a3 = new('a', 3);
    public static readonly Coordinate a4 = new('a', 4);
    public static readonly Coordinate a5 = new('a', 5);
    public static readonly Coordinate a6 = new('a', 6);
    public static readonly Coordinate a7 = new('a', 7);
    public static readonly Coordinate a8 = new('a', 8);
    public static readonly Coordinate b1 = new('b', 1);
    public static readonly Coordinate b2 = new('b', 2);
    public static readonly Coordinate b3 = new('b', 3);
    public static readonly Coordinate b4 = new('b', 4);
    public static readonly Coordinate b5 = new('b', 5);
    public static readonly Coordinate b6 = new('b', 6);
    public static readonly Coordinate b7 = new('b', 7);
    public static readonly Coordinate b8 = new('b', 8);
    public static readonly Coordinate c1 = new('c', 1);
    public static readonly Coordinate c2 = new('c', 2);
    public static readonly Coordinate c3 = new('c', 3);
    public static readonly Coordinate c4 = new('c', 4);
    public static readonly Coordinate c5 = new('c', 5);
    public static readonly Coordinate c6 = new('c', 6);
    public static readonly Coordinate c7 = new('c', 7);
    public static readonly Coordinate c8 = new('c', 8);
    public static readonly Coordinate d1 = new('d', 1);
    public static readonly Coordinate d2 = new('d', 2);
    public static readonly Coordinate d3 = new('d', 3);
    public static readonly Coordinate d4 = new('d', 4);
    public static readonly Coordinate d5 = new('d', 5);
    public static readonly Coordinate d6 = new('d', 6);
    public static readonly Coordinate d7 = new('d', 7);
    public static readonly Coordinate d8 = new('d', 8);
    public static readonly Coordinate e1 = new('e', 1);
    public static readonly Coordinate e2 = new('e', 2);
    public static readonly Coordinate e3 = new('e', 3);
    public static readonly Coordinate e4 = new('e', 4);
    public static readonly Coordinate e5 = new('e', 5);
    public static readonly Coordinate e6 = new('e', 6);
    public static readonly Coordinate e7 = new('e', 7);
    public static readonly Coordinate e8 = new('e', 8);
    public static readonly Coordinate f1 = new('f', 1);
    public static readonly Coordinate f2 = new('f', 2);
    public static readonly Coordinate f3 = new('f', 3);
    public static readonly Coordinate f4 = new('f', 4);
    public static readonly Coordinate f5 = new('f', 5);
    public static readonly Coordinate f6 = new('f', 6);
    public static readonly Coordinate f7 = new('f', 7);
    public static readonly Coordinate f8 = new('f', 8);
    public static readonly Coordinate g1 = new('g', 1);
    public static readonly Coordinate g2 = new('g', 2);
    public static readonly Coordinate g3 = new('g', 3);
    public static readonly Coordinate g4 = new('g', 4);
    public static readonly Coordinate g5 = new('g', 5);
    public static readonly Coordinate g6 = new('g', 6);
    public static readonly Coordinate g7 = new('g', 7);
    public static readonly Coordinate g8 = new('g', 8);
    public static readonly Coordinate h1 = new('h', 1);
    public static readonly Coordinate h2 = new('h', 2);
    public static readonly Coordinate h3 = new('h', 3);
    public static readonly Coordinate h4 = new('h', 4);
    public static readonly Coordinate h5 = new('h', 5);
    public static readonly Coordinate h6 = new('h', 6);
    public static readonly Coordinate h7 = new('h', 7);
    public static readonly Coordinate h8 = new('h', 8);
}