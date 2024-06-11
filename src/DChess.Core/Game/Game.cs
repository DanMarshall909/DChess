using DChess.Core.Moves;
using DChess.Test.Unit;

namespace DChess.Core.Game;

public sealed class Game : IDisposable
{
    public Game(IInvalidMoveHandler invalidMoveHandler, GameOptions? gameOptions = null,
        BoardState? chessBoardState = null)
    {
        _moveHandler = new MoveHandler(this);
        _piecePool = new PiecePool(this, invalidMoveHandler);
        _gameState = new GameState(this, _piecePool, invalidMoveHandler,
            BoardState.CloneOrEmptyIfNull(chessBoardState));
        _gameOptions = gameOptions ?? GameOptions.DefaultGameOptions;
    }

    public Colour CurrentPlayer { get; set; } = White;


    /// <summary>
    /// Creates a new Coordinate from an array offset instead of a file and rank
    /// </summary>
    /// <param name="fileArrayOffset">The file array offset from 0-7</param>
    /// <param name="rankArrayOffset">The rank array offset from 0-7</param>
    /// <exception cref="NotImplementedException"></exception>

    public GameState GameState => _gameState;

    public GameOptions Options => _gameOptions;


    /// <summary>
    ///     The vertical ranks (rows) of the board, from 1 to 8
    /// </summary>
    public static byte[] Ranks = { 1, 2, 3, 4, 5, 6, 7, 8 };

    /// <summary>
    ///     The horizontal files (columns) of the board, from a to h
    /// </summary>
    public static char[] Files = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

    public void Dispose()
    {
        _piecePool.Dispose();
    }

    private readonly MoveHandler _moveHandler;
    private readonly GameState _gameState;
    private readonly PiecePool _piecePool;
    private readonly GameOptions _gameOptions;

    public void Move(Coordinate from, Coordinate to)
    {
        _moveHandler.Make(new Move(from, to));
        CurrentPlayer = CurrentPlayer == White ? Black : White;
    }

    public void Move(Move move)
    {
        Move(move.From, move.To);
    }
}