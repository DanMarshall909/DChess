using DChess.Core.Game;

namespace DChess.Test.Unit;

public class PathFinderTests
{
    [Theory(DisplayName = "A vertical move has a path")]
    [InlineData("a1", "a4", 2)]
    [InlineData("a4", "a1", 2)]
    [InlineData("a1", "a2", 0)]
    public void a_vertical_move_has_a_path(string from, string to, int expectedLength)
    {
        var move = new Move(new Coordinate(from), new Coordinate(to));
        var path = move.Path.ToList();

        Assert.Equal(expectedLength, path.Count());
    }

    [Theory(DisplayName = "A horizontal move has a path")]
    [InlineData("a1", "d1", 2)]
    [InlineData("d1", "a1", 2)]
    [InlineData("a1", "b1", 0)]
    public void a_horizontal_move_has_a_path(string from, string to, int expectedLength)
    {
        var move = new Move(new Coordinate(from), new Coordinate(to));
        var path = move.Path.ToList();

        Assert.Equal(expectedLength, path.Count());
    }

    [Theory(DisplayName = "A diagonal move has a path")]
    [InlineData("a1", "d4", 2)]
    [InlineData("d4", "a1", 2)]
    [InlineData("a1", "b2", 0)]
    public void a_diagonal_move_has_a_path(string from, string to, int expectedLength)
    {
        var move = new Move(new Coordinate(from), new Coordinate(to));
        var path = move.Path.ToList();

        Assert.Equal(expectedLength, path.Count());
    }
}