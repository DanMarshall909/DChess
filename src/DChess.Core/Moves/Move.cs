namespace DChess.Core.Moves;

public readonly record struct Move(Square From, Square To)
{
    public static Move NullMove => new(NullSquare, NullSquare);
    public Distance Distance => new Memo<Move, Distance>(move => new Distance(move)).Execute(this);
    public bool HasPath => SquaresAlongPath.Any();
    public bool IsDiagonal => Math.Abs(To.File - From.File) == Math.Abs(To.Rank - From.Rank);
    public bool IsVertical => From.File == To.File;
    public bool IsHorizontal => From.Rank == To.Rank;
    public bool IsAdjacent => Distance.Total == 1;

    public IEnumerable<Square> SquaresAlongPath =>
        new Memo<Move, IEnumerable<Square>>(PathFinder.GetPath).Execute(this);

    public bool IsBackwards(Colour colour) => colour == White
        ? To.Rank < From.Rank
        : To.Rank > From.Rank;

    public bool IsBlocked(Board board) =>
        SquaresAlongPath.SkipLast(1).Any(square => board.HasPieceAt(square));

    public override string ToString() => $"{From} -> {To}";
    public MoveResult AsOkResult() => new(this, Ok);
    public MoveResult AsInvalidBecause(MoveValidity invalidReason) => new(this, invalidReason);
}