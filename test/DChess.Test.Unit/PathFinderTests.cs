using DChess.Core.Game;

namespace DChess.Test.Unit;

public class PathFinderTests
{
    [Theory(DisplayName = "A vertical move has a path")]
    [InlineData("a1", "a4", 3)]
    [InlineData("a4", "a1", 3)]
    [InlineData("a1", "a2", 1)]
    public void a_vertical_move_has_a_path(string from, string to, int expectedLength)
    {
        var move = new Move(new Square(from), new Square(to));
        var path = move.SquaresAlongPath.ToList();

        Assert.Equal(expectedLength, path.Count);
    }

    [Theory(DisplayName = "A horizontal move has a path")]
    [InlineData("a1", "d1", 3)]
    [InlineData("d1", "a1", 3)]
    [InlineData("a1", "b1", 1)]
    public void a_horizontal_move_has_a_path(string from, string to, int expectedLength)
    {
        var move = new Move(new Square(from), new Square(to));
        var path = move.SquaresAlongPath.ToList();

        Assert.Equal(expectedLength, path.Count);
    }

    [Theory(DisplayName = "A diagonal move has a path")]
    [InlineData("a1", "d4", 3)]
    [InlineData("d4", "a1", 3)]
    [InlineData("a1", "b2", 1)]
    public void a_diagonal_move_has_a_path(string from, string to, int expectedLength)
    {
        var move = new Move(new Square(from), new Square(to));
        var path = move.SquaresAlongPath.ToList();

        Assert.Equal(expectedLength, path.Count);
    }
}