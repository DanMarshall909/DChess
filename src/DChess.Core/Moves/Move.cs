namespace DChess.Core.Moves;

public readonly record struct Move(Coordinate From, Coordinate To)
{
    public override string ToString() => $"{From} -> {To}";
    public MoveOffset Offset => new MoveOffset(To.File - From.File, To.Rank - From.Rank);
    public bool IsDiagonal => Math.Abs(To.File - From.File) == Math.Abs(To.Rank - From.Rank);
    public bool IsVertical => From.File == To.File;
    public bool IsHorizontal => From.Rank == To.Rank;
    public bool IsAdjacent => Offset.IsAdjacent;
    public MoveResult AsOkResult => new(true, this, null);
    public MoveResult AsInvalidResult(string because) => new(false, this, because);

    public int Distance => (int)Math.Floor(Math.Sqrt(Math.Abs(To.File - From.File) +
                                                     Math.Abs(To.Rank - From.Rank)));

    public IEnumerable<Coordinate> Path => PathFinder.GetPath(this);
}