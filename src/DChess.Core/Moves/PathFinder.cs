using DChess.Core.Game;

namespace DChess.Core.Moves;

public static class PathFinder
{
    public static IEnumerable<Coordinate> GetPath(Move move)
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
        for (int r = move.From.Rank + step; r != move.To.Rank; r += step)
            yield return new Coordinate(move.From.File, (byte)r);
    }

    private static IEnumerable<Coordinate> HorizontalPath(Move move)
    {
        var step = (char)Math.Sign(move.To.File - move.From.File);
        for (var f = (char)(move.From.File + step); f != move.To.File; f += step)
            yield return new Coordinate(f, move.From.Rank);
    }

    private static IEnumerable<Coordinate> DiagonalPath(Move move)
    {
        int stepFile = Math.Sign(move.To.File - move.From.File);
        int stepRank = Math.Sign(move.To.Rank - move.From.Rank);
        var f = (char)(move.From.File + stepFile);
        var r = (byte)(move.From.Rank + stepRank);

        while (f != move.To.File && r != move.To.Rank)
        {
            yield return new Coordinate(f, r);
            f += (char)stepFile;
            r += (byte)stepRank;
        }
    }
}