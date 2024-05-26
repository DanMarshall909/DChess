namespace DChess.Core.Moves;

public readonly record struct MoveResult(bool Valid, Move Move, string? Message)
{
    public override string ToString() => Valid ? $"Valid move {Move}" : $"Invalid move {Move}: {Message}";
};