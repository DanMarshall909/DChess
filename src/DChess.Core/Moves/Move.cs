namespace DChess.Core.Moves;

public readonly struct Move(Coordinate from, Coordinate to)
{
    public readonly Coordinate From = from;
    public readonly Coordinate To = to;
    
    public override string ToString() => $"{From} -> {To}";
}