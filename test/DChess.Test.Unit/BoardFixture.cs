using DChess.Core.Board;
using DChess.Core.Moves;

namespace DChess.Test.Unit;

public class BoardFixture : IDisposable
{
    public BoardFixture()
    {
        InvalidMoveHandler = new TestInvalidMoveHandler();
        Board = new Board(InvalidMoveHandler);
    }

    public IInvalidMoveHandler InvalidMoveHandler { get; set; }

    public void Dispose()
    {
        Board.Dispose();
    }

    public Board Board { get; }
}