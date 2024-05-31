namespace DChess.Core.Moves;

/// <summary>
///  Represents an offset from a position on a chess board
/// </summary>
/// <param name="fileOffset"></param>
/// <param name="rankOffset"></param>
public readonly struct MoveOffset(int fileOffset, int rankOffset)
{
    public int FileOffset { get; } = fileOffset;
    public int RankOffset { get; } = rankOffset;
    public override string ToString() => $"File: {FileOffset}, Rank: {RankOffset})";

    public static implicit operator MoveOffset((int FileOffset, int RankOffset) tuple) =>
        new(tuple.FileOffset, tuple.RankOffset);

    public void Deconstruct(out int fileOffset, out int rankOffset)
    {
        fileOffset = FileOffset;
        rankOffset = RankOffset;
    }
}