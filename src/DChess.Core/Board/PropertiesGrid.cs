using System.Runtime.CompilerServices;

namespace DChess.Core.Board;

public readonly record struct PropertiesGrid
{
    public const int TotalPropertiesInGrid = 8 * 8;

    public static PropertiesGrid CloneOrEmptyIfNull(PropertiesGrid? properties)
        => properties is not null
            ? new PropertiesGrid(properties!.Value.AsArray.Clone() as Properties[] ?? Array.Empty<Properties>())
            : new PropertiesGrid();

    public PropertiesGrid()
    {
        Clear();
    }

    private PropertiesGrid(Properties[] data)
        => AsArray = CloneOf(data);

    private static Properties[] CloneOf(Properties[] data)
        => data.Clone() as Properties[] ?? Array.Empty<Properties>();

    public Properties[] AsArray { get; } = new Properties[TotalPropertiesInGrid];

    public void Clear()
    {
        var rowSpan = new Span<Properties>(AsArray, 0, TotalPropertiesInGrid);
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
}