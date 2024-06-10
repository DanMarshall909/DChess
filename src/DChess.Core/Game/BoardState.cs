using System.Runtime.CompilerServices;

namespace DChess.Core.Game;

public readonly record struct BoardState
{
    public const int TotalCellsOnBoard = 8 * 8;

    public static BoardState CloneOrEmptyIfNull(BoardState? state)
        => state is not null
            ? new BoardState(state!.Value.AsArray.Clone() as Properties[] ?? Array.Empty<Properties>())
            : new BoardState();

    public BoardState()
    {
        Clear();
    }

    private BoardState(Properties[] data)
        => AsArray = CloneOf(data);

    private static Properties[] CloneOf(Properties[] data)
        => data.Clone() as Properties[] ?? Array.Empty<Properties>();

    public Properties[] AsArray { get; } = new Properties[TotalCellsOnBoard];

    public static IEnumerable<Coordinate> AllCoordinates { get; } = new[]
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

    public void Clear()
    {
        var rowSpan = new Span<Properties>(AsArray, 0, TotalCellsOnBoard);
        rowSpan.Fill(Properties.None);
    }

    public Properties this[Coordinate coordinate]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this[coordinate.File, coordinate.Rank];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => this[coordinate.File, coordinate.Rank] = value;
    }

    public Properties this[char file, byte rank]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => AsArray[ToIndex(file - 'a', rank - 1)];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => AsArray[ToIndex(file - 'a', rank - 1)] = value;
    }

    public Properties this[int fileIndex, int rankIndex]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => AsArray[ToIndex(fileIndex, rankIndex)];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => AsArray[ToIndex(fileIndex, rankIndex)] = value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ToIndex(int file, int rank) => (file + (rank << 3));

    public Coordinate Find(Func<Properties, bool> func)
    {
        for (byte f = 0; f < 8; f++)
        {
            for (byte r = 0; r < 8; r++)
            {
                var props = this[f, r];
                if (func(props))
                    return new Coordinate(f, r);
            }
        }

        return NullCoordinate;
    }

    public bool HasPieceAt(Coordinate coordinate) => this[coordinate] != Properties.None;
}