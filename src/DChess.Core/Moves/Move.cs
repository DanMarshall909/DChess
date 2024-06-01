namespace DChess.Core.Moves;

public readonly record struct Move(Coordinate From, Coordinate To)
{
    public override string ToString() => $"{From} -> {To}";
    public MoveOffset Offset => new MoveOffset(To.File - From.File, To.Rank - From.Rank);
    public bool IsDiagonal => Math.Abs(To.File - From.File) == Math.Abs(To.Rank - From.Rank);
    public bool IsVertical => From.File == To.File;
    public bool IsHorizontal => From.Rank == To.Rank;
    public bool IsAdjacent => Offset.IsAdjacent;
    public bool IsForward(Colour colour) => colour == White
        ? To.Rank > From.Rank
        : To.Rank < From.Rank;
    
    public bool IsBackwards(Colour colour) => colour == White
        ? To.Rank < From.Rank
        : To.Rank > From.Rank;
    
    public MoveResult AsOkResult => new(true, this, null);
    public MoveResult AsInvalidResult(string because) => new(false, this, because);

    
    public int VerticalDistance => Math.Abs(To.Rank - From.Rank);
    public int HorizontalDistance => Math.Abs(To.File - From.File);
    public int TotalDistance => (int)Math.Floor(Math.Sqrt(HorizontalDistance + VerticalDistance));

    public IEnumerable<Coordinate> Path => PathFinder.GetPath(this);
}