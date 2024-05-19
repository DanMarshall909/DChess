using System.Collections;
using static DChess.Core.Piece;
using static DChess.Core.Piece.PieceColour;

namespace DChess.Core;

/// <summary>
/// A pool of pieces that can be reused to reduce memory allocation / GC pressure
/// </summary>
public class PiecePool
{
    // A stack ensures that the most recently used object is the first to be reused.
    // This is efficient for memory caching and locality, as the objects that were
    // used recently are likely to be still in the CPU cache, making their reuse faster
    private readonly Stack<Piece> _pool = new();

    /// <summary>
    /// Get a piece from the pool, or create a new one if the pool is empty
    /// </summary>
    /// <param name="type"></param>
    /// <param name="colour"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public Piece GetPiece(PieceType type, PieceColour colour, Coordinate position)
    {
        if (_pool.Count <= 0) return new Piece(type, colour, position);

        var piece = _pool.Pop();
        piece.Initialize(type, colour, position);
        return piece;
    }

    public void ReturnPiece(Piece piece)
    {
        _pool.Push(piece);
    }
}

public struct PieceCollection : IEnumerable<Piece>
{
    private readonly PiecePool pool = new();

    private BitBoard[,] _bitBoards = new BitBoard[Enum.GetValues(typeof(PieceColour)).Length, Enum.GetValues(typeof(PieceType)).Length];

    public PieceCollection()
    {
    }

    public readonly void Add(Piece p)
    {
        var bitBoard = _bitBoards[(int)p.Colour, (int)p.Type];
        bitBoard.Set(p.Coordinate);
    }
     
    public void Clear(Piece p)
    {
        _bitBoards[(int)p.Colour, (int)p.Type].Clear(p.Coordinate);
    }

    public void Clear() => _bitBoards = new BitBoard[Enum.GetValues(typeof(PieceColour)).Length, Enum.GetValues(typeof(PieceType)).Length];

    public IEnumerator<Piece> GetEnumerator()
    {
        for (PieceColour i = White; i < Black; i++)
        {
            for (PieceType j = PieceType.Pawn; j < PieceType.King; j++)
            {
                BitBoard board = _bitBoards[(int)i, (int)j];
                
                    foreach(var coordinate in board.GetSetBits())
                    {
                        yield return pool.GetPiece(j, i, coordinate);
                    };
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}