namespace DChess.Core.Moves;

public static class MoveExtensions
{
    public static Move AsMove(this string move)
        => new(move[..2].ToSquare(), move[2..].ToSquare());
}