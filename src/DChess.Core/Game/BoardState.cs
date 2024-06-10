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
        for (var f = 0; f < 8; f++)
        {
            for (var r = 0; r < 8; r++)
            {
                var props = this[f, r];
                if (func(props))
                    return new Coordinate(Game.Files[f], Game.Ranks[r]);
            }
        }

        return NullCoordinate;
    }

    public bool HasPieceAt(Coordinate coordinate) => this[coordinate] != Properties.None;
}