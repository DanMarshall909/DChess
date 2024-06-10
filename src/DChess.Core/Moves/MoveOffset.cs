﻿namespace DChess.Core.Moves;

/// <summary>
///     Represents an offset from a position on a chess board
/// </summary>
/// <param name="FileOffset"></param>
/// <param name="RankOffset"></param>
public readonly record struct MoveOffset(int FileOffset, int RankOffset)
{
    public override string ToString() => $"File: {FileOffset}, Rank: {RankOffset})";

    public static implicit operator MoveOffset((int FileOffset, int RankOffset) tuple) =>
        new(tuple.FileOffset, tuple.RankOffset);
}