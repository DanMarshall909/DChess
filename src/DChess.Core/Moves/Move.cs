namespace DChess.Core.Moves;

public readonly record struct Move(Coordinate From, Coordinate To)
{
    private MoveOffset Offset => new(To.File - From.File, To.Rank - From.Rank);
    public bool IsDiagonal => Math.Abs(To.File - From.File) == Math.Abs(To.Rank - From.Rank);
    public bool IsVertical => From.File == To.File;
    public bool IsHorizontal => From.Rank == To.Rank;
    public bool IsAdjacent => Offset.IsAdjacent;
    public Distance Distance => new Memo<Move, Distance>(move => new Distance(move)).Execute(this);
    public int VerticalDistance => Distance.Vertical;
    public int HorizontalDistance => Distance.Horizontal;
    public int TotalDistance => Distance.Total;
    public IEnumerable<Coordinate> Path => new Memo<Move, IEnumerable<Coordinate>>(PathFinder.GetPath).Execute(this);
    public override string ToString() => $"{From} -> {To}";

    public bool IsForward(Colour colour) => colour == White
        ? To.Rank > From.Rank
        : To.Rank < From.Rank;

    public bool IsBackwards(Colour colour) => colour == White
        ? To.Rank < From.Rank
        : To.Rank > From.Rank;
}