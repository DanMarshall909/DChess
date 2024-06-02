using DChess.Core.Board;

namespace DChess.Test.Unit;

public class BoardFixture : IDisposable
{
    public BoardFixture()
    {
        InvalidMoveHandler = new TestInvalidMoveHandler();
        Board = new Board(InvalidMoveHandler);
    }

    public IInvalidMoveHandler InvalidMoveHandler { get; set; }

    public Board Board { get; }

    public void Dispose()
    {
        Board.Dispose();
    }
}