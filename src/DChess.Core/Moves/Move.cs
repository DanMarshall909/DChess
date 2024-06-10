using DChess.Core.Game;

namespace DChess.Core.Moves;

public readonly record struct Move(Coordinate From, Coordinate To)
{
    public static Move InvalidMove => default;
    public bool IsLegalIfNotBlocked => CoordinatesAlongPath.Any();
    public bool IsDiagonal => Math.Abs(To.File - From.File) == Math.Abs(To.Rank - From.Rank);
    public bool IsVertical => From.File == To.File;
    public bool IsHorizontal => From.Rank == To.Rank;
    public bool IsAdjacent => Distance.Total == 1;
    public bool IsForwards(Colour colour) => colour == White
        ? To.Rank > From.Rank
        : To.Rank < From.Rank;
    public bool IsBackwards(Colour colour) => colour == White
        ? To.Rank < From.Rank
        : To.Rank > From.Rank;
    public IEnumerable<Coordinate> CoordinatesAlongPath => new Memo<Move, IEnumerable<Coordinate>>(PathFinder.GetPath).Execute(this);
    public Distance Distance => new Memo<Move, Distance>(move => new Distance(move)).Execute(this);
    public int VerticalDistance => Distance.Vertical;
    public int HorizontalDistance => Distance.Horizontal;
    public int TotalDistance => Distance.Total;
    public bool IsBlocked(BoardState boardState) => CoordinatesAlongPath.Any(coordinate => boardState.HasPieceAt(coordinate));
    public override string ToString() => $"{From} -> {To}";
    private MoveOffset Offset => new(To.File - From.File, To.Rank - From.Rank);
    public MoveResult OkResult() => new(this, Ok);
    public MoveResult InvalidResult(MoveValidity invalidReason) => new(this, invalidReason);
}
