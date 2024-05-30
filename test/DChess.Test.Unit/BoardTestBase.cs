using DChess.Core.Board;

namespace DChess.Test.Unit;

public abstract class BoardTestBase(BoardFixture fixture) : IClassFixture<BoardFixture>
{
    protected Board Board => Fixture.Board;
    public BoardFixture Fixture { get; set; } = fixture;
}