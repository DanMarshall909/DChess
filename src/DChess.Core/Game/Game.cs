using DChess.Core.Moves;

namespace DChess.Core.Game;

public sealed class Game
{
    public Game(IErrorHandler errorHandler, GameOptions? gameOptions = null, BoardState? boardState = null)
    {
        GameState = new GameState(BoardState.CloneOrEmptyIfNull(boardState), errorHandler);
        _gameOptions = gameOptions ?? GameOptions.DefaultGameOptions;
    }

    public GameState GameState { get; }

    private readonly IErrorHandler _errorHandler;
    private readonly PiecePool _piecePool;
    private readonly GameOptions _gameOptions;

    public void Move(Coordinate from, Coordinate to)
    {
        GameState.Move(new Move(from, to));
    }
}