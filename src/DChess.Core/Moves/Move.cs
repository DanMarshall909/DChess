namespace DChess.Core.Moves;

public readonly record struct Move(Coordinate From, Coordinate To)
{
    public static Move NullMove => new(NullCoordinate, NullCoordinate);
    public Distance Distance => new Memo<Move, Distance>(move => new Distance(move)).Execute(this);
    public bool HasPath => CoordinatesAlongPath.Any();
    public bool IsDiagonal => Math.Abs(To.File - From.File) == Math.Abs(To.Rank - From.Rank);
    public bool IsVertical => From.File == To.File;
    public bool IsHorizontal => From.Rank == To.Rank;
    public bool IsAdjacent => Distance.Total == 1;

    public IEnumerable<Coordinate> CoordinatesAlongPath =>
        new Memo<Move, IEnumerable<Coordinate>>(PathFinder.GetPath).Execute(this);

    private MoveOffset Offset => new(To.File - From.File, To.Rank - From.Rank);

    public bool IsBackwards(Colour colour) => colour == White
        ? To.Rank < From.Rank
        : To.Rank > From.Rank;

    public bool IsBlocked(Board board) =>
        CoordinatesAlongPath.SkipLast(1).Any(coordinate => board.HasPieceAt(coordinate));

    public override string ToString() => $"{From} -> {To}";
    public MoveResult AsOkResult() => new(this, Ok);
    public MoveResult AsInvalidBecause(MoveValidity invalidReason) => new(this, invalidReason);
}