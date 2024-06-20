using DChess.Core.Game;

namespace DChess.Core.Moves;

public static class MoveExtensions
{
    public static Move AsMove(this string move)
        => new(move[..2].ToCoordinate(), move[2..].ToCoordinate());
}