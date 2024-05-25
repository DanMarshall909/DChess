namespace DChess.Core;

public readonly record struct MoveResult(bool Valid, Move Move, string? Message)
{
    public override string ToString() => Valid ? "Valid move" : $"Invalid move: {Message}";
};