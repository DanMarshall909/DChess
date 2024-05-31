using DChess.Core.Board;

namespace DChess.Core.Moves;

public readonly struct Move(Coordinate from, Coordinate to)
{
    public readonly Coordinate From = from;
    public readonly Coordinate To = to;
    
    public bool IsDiagonal => Math.Abs(To.File - From.File) == Math.Abs(To.Rank - From.Rank);
    public bool IsHorizontal => From.File == To.File;
    public bool IsVertical => From.Rank == To.Rank;
    public override string ToString() => $"{From} -> {To}";
    public MoveResult AsOkResult => new(true, this, null);
    public MoveResult AsInvalidResult(string because) => new(false, this, because);
}