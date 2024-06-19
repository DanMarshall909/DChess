using System.Runtime.CompilerServices;

namespace DChess.Core.Game;

public readonly record struct Board
{
    private const int TotalCellsOnBoard = 8 * 8;

    public static Board CloneOrEmptyIfNull(Board? state)
        => state is not null
            ? new Board(state!.Value.AsArray.Clone() as Properties[] ?? Array.Empty<Properties>())
            : new Board();

    public Board()
    {
        Clear();
    }

    private Board(Properties[] data)
        => AsArray = CloneOf(data);

    private static Properties[] CloneOf(Properties[] data)
        => data.Clone() as Properties[] ?? Array.Empty<Properties>();

    public Properties[] AsArray { get; } = new Properties[TotalCellsOnBoard];

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

    public Properties this[char file, int rank]
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
    private static int ToIndex(int file, int rank) => file + (rank << 3);

    public Coordinate Find(Func<Properties, bool> func)
    {
        for (byte f = 0; f < 8; f++)
        for (byte r = 0; r < 8; r++)
        {
            var props = this[f, r];
            if (func(props))
                return new Coordinate(f, r);
        }

        return NullCoordinate;
    }

    public bool HasPieceAt(Coordinate coordinate) => this[coordinate] != Properties.None;

    public void RemovePieceAt(Coordinate coordinate)
    {
        this[coordinate] = Properties.None;
    }

    public void SetPiece(Coordinate coordinate, Properties properties)
    {
        this[coordinate] = properties;
    }

    public void Place(Properties pieceProperties, Coordinate at)
    {
        this[at] = pieceProperties;
    }

    public bool TryGetProperties(Coordinate coordinate, out Properties properties)
    {
        properties = this[coordinate];
        return properties != Properties.None;
    }
    
    public Coordinate KingCoordinate(Colour colour)
        => Find(props => props.Type == PieceType.King && props.Colour == colour);
}