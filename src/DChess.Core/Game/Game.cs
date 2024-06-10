using DChess.Core.Moves;

namespace DChess.Core.Game;

public sealed class Game : IDisposable
{
    public Game(IInvalidMoveHandler invalidMoveHandler, BoardState? chessBoardState = null)
    { 
        _moveHandler = new MoveHandler(this);
        _piecePool = new PiecePool(this, invalidMoveHandler);
        _gameState = new GameState(this, _piecePool, invalidMoveHandler, BoardState.CloneOrEmptyIfNull(chessBoardState));
    }

    /// <summary>
    /// Creates a new Coordinate from an array offset instead of a file and rank
    /// </summary>
    /// <param name="fileArrayOffset">The file array offset from 0-7</param>
    /// <param name="rankArrayOffset">The rank array offset from 0-7</param>
    /// <exception cref="NotImplementedException"></exception>

    public GameState GameState
    {
        get { return _gameState; }
    }


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

    public void Move(Coordinate from, Coordinate to)
    {
        _moveHandler.Make(new Move(from, to));
    }
    
    public void Move(Move move)
    {
        _moveHandler.Make(move);
    }
}