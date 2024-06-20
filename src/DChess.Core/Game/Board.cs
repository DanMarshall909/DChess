namespace DChess.Core.Game;

public readonly record struct Board
{
    private const int TotalCellsOnBoard = 8 * 8;

    public Board()
        => Clear();

    private Board(Properties[] data)
        => Data = CloneOf(data);

    private Properties[] Data { get; } = new Properties[TotalCellsOnBoard];

    /// <summary>
    ///     Gets or sets the properties at the specified coordinate.
    /// </summary>
    /// <param name="coordinate"></param>
    public Properties this[Coordinate coordinate]
    {
        get => this[coordinate.File, coordinate.Rank];
        set => this[coordinate.File, coordinate.Rank] = value;
    }

    /// <summary>
    ///     Gets or sets the properties at the specified file and rank.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="rank"></param>
    public Properties this[char file, int rank]
    {
        get => Data[ToIndex(file - 'a', rank - 1)];
        set => Data[ToIndex(file - 'a', rank - 1)] = value;
    }

    /// <summary>
    ///     Gets or sets the properties at the specified zero-indexed file and rank.
    /// </summary>
    /// <param name="fileIndex"></param>
    /// <param name="rankIndex"></param>
    public Properties this[int fileIndex, int rankIndex]
    {
        get => Data[ToIndex(fileIndex, rankIndex)];
        set => Data[ToIndex(fileIndex, rankIndex)] = value;
    }

    public static Board CloneOrEmptyIfNull(Board? board)
        => board is not null
            ? new Board(board!.Value.Data.Clone() as Properties[] ?? Array.Empty<Properties>())
            : new Board();

    private static Properties[] CloneOf(Properties[] properties)
        => properties.Clone() as Properties[] ?? Array.Empty<Properties>();

    /// <summary>
    ///     Converts a file and rank to a zero-indexed index for accessing the data array.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="rank"></param>
    /// <returns></returns>
    private static int ToIndex(int file, int rank) => file + (rank << 3);

    public void Clear()
    {
        var rowSpan = new Span<Properties>(Data, 0, TotalCellsOnBoard);
        rowSpan.Fill(Properties.None);
    }

    /// <summary>
    ///     Finds the first coordinate that matches the predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns>The first coordinate that matches the predicate, or NullCoordinate if none match.</returns>
    public Coordinate Find(Func<Properties, bool> predicate)
    {
        for (byte f = 0; f < 8; f++)
        for (byte r = 0; r < 8; r++)
        {
            var props = this[f, r];
            if (predicate(props))
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