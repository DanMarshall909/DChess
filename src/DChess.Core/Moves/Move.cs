using DChess.Core.Board;

namespace DChess.Core.Moves;

public readonly record struct Move(Coordinate from, Coordinate to)
{
    public readonly Coordinate From = from;
    public readonly Coordinate To = to;
    public override string ToString() => $"{From} -> {To}";
    public MoveOffset Offset => new MoveOffset(To.File - From.File, To.Rank - From.Rank);
    public bool IsDiagonal => Math.Abs(To.File - From.File) == Math.Abs(To.Rank - From.Rank);
    public bool IsVertical => From.File == To.File;
    public bool IsHorizontal => From.Rank == To.Rank;
    public bool IsAdjacent => this.Offset.IsAdjacent;
    public MoveResult AsOkResult => new(true, this, null);

    private static readonly Memo<Move, int> DistanceMemo =
        new(move => (int)Math.Floor(Math.Sqrt(Math.Abs(move.To.File - move.From.File) +
                                              Math.Abs(move.To.Rank - move.From.Rank))));

    private static readonly Memo<Move, IEnumerable<Coordinate>> PathMemo = new(move => GetPath(move));

    public MoveResult AsInvalidResult(string because) => new(false, this, because);

    public int Distance => DistanceMemo.Execute(this);

    public IEnumerable<Coordinate> Path => PathMemo.Execute(this);

    private static IEnumerable<Coordinate> GetPath(Move move)
    {
        if (move.IsHorizontal)
            return HorizontalPath(move);

        if (move.IsVertical)
            return VerticalPath(move);

        if (move.IsDiagonal)
            return DiagonalPath(move);

        return Enumerable.Empty<Coordinate>();
    }

    private static IEnumerable<Coordinate> VerticalPath(Move move)
    {
        int step = Math.Sign(move.To.Rank - move.From.Rank);
        for (int r = (move.From.Rank + step); r != move.To.Rank; r += step)
            yield return new Coordinate(move.From.File, (byte)(move.From.Rank + r));
    }

    private static IEnumerable<Coordinate> HorizontalPath(Move move)
    {
        char from = move.From.File;
        char to = move.To.File;
        int step = Math.Sign(to - from);

        for (int file = from + step; file != to; file += step)
            yield return new Coordinate((char)(file), move.From.Rank);
    }

    private static IEnumerable<Coordinate> DiagonalPath(Move move)
    {
        int stepF = Math.Sign(move.To.File - move.From.File);
        int stepR = Math.Sign(move.To.Rank - move.From.Rank);

        var f = (byte)(move.From.File + stepF);
        var r = (byte)(move.From.Rank + stepR);

        while (f != move.To.File && r != move.To.Rank)
        {
            yield return new Coordinate((char)f, r);
            f += (byte)stepF;
            r += (byte)stepR;
        }
    }
}