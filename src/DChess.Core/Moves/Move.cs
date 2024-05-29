namespace DChess.Core.Moves;

public readonly struct Move(Coordinate from, Coordinate to)
{
    public readonly Coordinate From = from;
    public readonly Coordinate To = to;
    public override string ToString() => $"{From} -> {To}";
    public MoveResult AsOkResult => new(true, this, null);
    public MoveResult AsInvalidResult(string because) => new(false, this, because);
}