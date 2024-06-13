using DChess.Core.Moves;
using DChess.Test.Unit;

namespace DChess.Core.Game;

public sealed class Game : IDisposable
{
    public Game(IErrorHandler errorHandler, GameOptions? gameOptions = null,
        BoardState? boardState = null)
    {
        _errorHandler = errorHandler;
        _piecePool = new PiecePool();
        GameState = new GameState(_piecePool, BoardState.CloneOrEmptyIfNull(boardState), errorHandler);
        _gameOptions = gameOptions ?? GameOptions.DefaultGameOptions;
    }

    public Colour CurrentPlayer { get; set; } = White;

    public GameState GameState { get; }

    public void Dispose()
    {
        _piecePool.Dispose();
    }

    private readonly IErrorHandler _errorHandler;
    private readonly PiecePool _piecePool;
    private readonly GameOptions _gameOptions;
    private readonly MoveHandler _moveHandler;

    public void Move(Coordinate from, Coordinate to)
    {
        GameState.Move(new Move(from, to));
    }
}