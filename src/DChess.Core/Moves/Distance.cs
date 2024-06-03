namespace DChess.Core.Moves;

public readonly struct Distance
{
    public Distance(Move move)
    {
        Vertical = Math.Abs(move.To.Rank - move.From.Rank);
        Horizontal = Math.Abs(move.To.File - move.From.File);
        Total = (int)Math.Round(Math.Sqrt(Horizontal + Vertical));
    }

    public int Total { get; }
    public int Horizontal { get; }
    public int Vertical { get; }
}