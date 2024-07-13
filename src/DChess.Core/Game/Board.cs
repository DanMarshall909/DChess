namespace DChess.Core.Game;

public readonly record struct Board
{
    private const int TotalCellsOnBoard = 8 * 8;

    public string AsText => this.RenderToText();

    public Board()
        => Clear();

    private Board(PieceAttributes[] data)
        => Data = CloneOf(data);

    private PieceAttributes[] Data { get; } = new PieceAttributes[TotalCellsOnBoard];

    /// <summary>
    ///     Gets or sets the pieceAttributes at the specified square.
    /// </summary>
    /// <param name="square"></param>
    public PieceAttributes this[Square square]
    {
        get => this[square.File, square.Rank];
        set => this[square.File, square.Rank] = value;
    }

    /// <summary>
    ///     Gets or sets the pieceAttributes at the specified file and rank.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="rank"></param>
    public PieceAttributes this[char file, int rank]
    {
        get => Data[ToIndex(file - 'a', rank - 1)];
        set => Data[ToIndex(file - 'a', rank - 1)] = value;
    }

    /// <summary>
    ///     Gets or sets the pieceAttributes at the specified zero-indexed file and rank.
    /// </summary>
    /// <param name="fileIndex"></param>
    /// <param name="rankIndex"></param>
    public PieceAttributes this[int fileIndex, int rankIndex]
    {
        get => Data[ToIndex(fileIndex, rankIndex)];
        set => Data[ToIndex(fileIndex, rankIndex)] = value;
    }

    public static Board CloneOrEmptyIfNull(Board? board)
        => board is not null
            ? new Board(board.Value.Data.Clone() as PieceAttributes[] ?? [])
            : new Board();

    private static PieceAttributes[] CloneOf(PieceAttributes[] properties)
        => properties.Clone() as PieceAttributes[] ?? [];

    /// <summary>
    ///     Converts a file and rank to a zero-indexed index for accessing the data array.
    /// </summary>
    /// <param name="file"></param>
    /// <param name="rank"></param>
    /// <returns></returns>
    private static int ToIndex(int file, int rank) => file + (rank << 3);

    public void Clear()
    {
        var rowSpan = new Span<PieceAttributes>(Data, 0, TotalCellsOnBoard);
        rowSpan.Fill(PieceAttributes.None);
    }

    /// <summary>
    ///     Finds the first square that matches the predicate.
    /// </summary>
    /// <param name="predicate"></param>
    /// <returns>The first square that matches the predicate, or NullSquare if none match.</returns>
    public Square Find(Func<PieceAttributes, bool> predicate)
    {
        for (byte f = 0; f < 8; f++)
        for (byte r = 0; r < 8; r++)
        {
            var props = this[f, r];
            if (predicate(props))
                return new Square(f, r);
        }

        return NullSquare;
    }

    public bool HasPieceAt(Square square) => this[square] != PieceAttributes.None;

    public void RemovePieceAt(Square square)
    {
        this[square] = PieceAttributes.None;
    }

    public void SetPiece(Square square, PieceAttributes pieceAttributes)
    {
        this[square] = pieceAttributes;
    }

    public void Place(PieceAttributes piecePieceAttributes, Square at)
    {
        this[at] = piecePieceAttributes;
    }

    public bool TryGetAtributes(Square square, out PieceAttributes pieceAttributes)
    {
        pieceAttributes = this[square];
        return pieceAttributes != PieceAttributes.None;
    }

    public Square KingSquare(Colour colour)
        => Find(props => props.Kind == Kind.King && props.Colour == colour);
}