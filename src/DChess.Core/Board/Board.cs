using System.Collections.ObjectModel;
using DChess.Core.Exceptions;
using DChess.Core.Moves;
using DChess.Core.Pieces;

namespace DChess.Core.Board;

public sealed class Board : IDisposable
{
    private bool IsEmpty => _properties == EmptyProperties;
    private static Properties[,] EmptyProperties => new Properties[8, 8];

    public Board(IInvalidMoveHandler invalidMoveHandler)
    {
        _invalidMoveHandler = invalidMoveHandler;
        _pool = new PiecePool(this, invalidMoveHandler);
        _moveHandler = new MoveHandler(this);
        _properties = EmptyProperties;
    }

    public Board(IInvalidMoveHandler invalidMoveHandler,
        Dictionary<Coordinate, Properties> piecesByCoordinate) : this(invalidMoveHandler)
    {
        var pieces = new Properties[8, 8];
        foreach (var (coordinate, props) in piecesByCoordinate)
        {
            pieces[coordinate.File - 'a', coordinate.Rank - 1] = props;
        }

        _properties = pieces;
    }

    private Board(IInvalidMoveHandler invalidMoveHandler, Properties[,]? properties) : this(invalidMoveHandler)
        => _properties = (properties?.Clone() as Properties[,]) ?? EmptyProperties;

    internal static Board CreateInstance(IInvalidMoveHandler invalidMoveHandler, Properties[,]? properties = null)
        => new(invalidMoveHandler, properties);

    public bool HasPieceAt(Coordinate coordinate)
    {
        var properties = _properties[coordinate.File - 'a', coordinate.Rank - 1];
        return properties != Properties.None;
    }


    public void Set(Coordinate coordinate, Properties properties)
        => _properties[coordinate.File - 'a', coordinate.Rank - 1] = properties;

    public void Move(Move move)
    {
        _moveHandler.Make(move);
    }

    /// <summary>
    /// Creates a new Coordinate from an array offset instead of a file and rank
    /// </summary>
    /// <param name="fileArrayOffset">The file array offset from 0-7</param>
    /// <param name="rankArrayOffset">The rank array offset from 0-7</param>
    /// <exception cref="NotImplementedException"></exception>
    private Coordinate CoordinateFromZeroOffset(int fileArrayOffset, int rankArrayOffset)
        => new((byte)((fileArrayOffset & 0b111) | ((rankArrayOffset & 0b111) << 3)));

    public IEnumerable<Piece> FriendlyPiecesByCoordinate(Colour colour)
    {
        for (int f = 0; f < 8; f++)
        {
            for (int r = 0; r < 8; r++)
            {
                var props = _properties[f, r];
                if (props == Properties.None) continue;
                if (props.Colour == colour)
                    yield return _pool.GetPiece(CoordinateFromZeroOffset(f, r), props);
            }
        }
    }

    public IEnumerable<Piece> OpposingPiecesByCoordinate(Colour colour)
        => FriendlyPiecesByCoordinate(colour == White ? Black : White);

    public ReadOnlyDictionary<Coordinate, Piece> Pieces
    {
        get
        {
            var pieces = new Dictionary<Coordinate, Piece>();
            for (int f = 0; f < 8; f++)
            {
                for (int r = 0; r < 8; r++)
                {
                    var props = _properties[f, r];
                    if (props == Properties.None) continue;
                    var val = props;
                    var coordinateFromZeroOffset = CoordinateFromZeroOffset(f, r);
                    pieces.Add(coordinateFromZeroOffset, _pool.GetPiece(coordinateFromZeroOffset, val));
                }
            }

            return new ReadOnlyDictionary<Coordinate, Piece>(pieces);
        }
    }


    public bool TryGetProperties(Coordinate coordinate, out Properties properties)
    {
        properties = _properties[coordinate.File - 'a', coordinate.Rank - 1];
        return properties != Properties.None;
    }

    public bool TryGetPiece(Coordinate at, out Piece piece)
    {
        var p = TryGetProperties(at, out var properties) ? properties : Properties.None;
        if (p == Properties.None)
        {
            piece = new NullPiece(new(p, at, this, _invalidMoveHandler));
            return false;
        }

        piece = _pool.GetPiece(at, properties);
        return true;
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
        _pool.Dispose();
    }

    public void Clear()
    {
        _properties = new Properties[8, 8];
    }

    public Board Clone()
    {
        return new Board(_invalidMoveHandler, _properties);
    }

    public Coordinate GetKingCoordinate(Colour colour)
    {
        // get index of king
        for (int f = 0; f < 8; f++)
        {
            for (int r = 0; r < 8; r++)
            {
                var props = _properties[f, r];
                if (props.Type == PieceType.King && props.Colour == colour)
                    return new Coordinate(Files[f], Ranks[r]);
            }
        }

        return NullCoordinate;
    }

    private readonly IInvalidMoveHandler _invalidMoveHandler;

    private Properties[,] _properties;

    private readonly PiecePool _pool;
    private readonly MoveHandler _moveHandler;

    public void RemovePieceAt(Coordinate moveFrom)
    {
        _properties[moveFrom.File - 'a', moveFrom.Rank - 1] = Properties.None;
    }

    public void SetPiece(Coordinate moveTo, Properties to)
    {
        _properties[moveTo.File - 'a', moveTo.Rank - 1] = to;
    }

    public void Make(Move move)
    {
        _moveHandler.Make(move);
    }

    public Properties GetProperties(Coordinate coordinate) =>
        _properties[coordinate.File - 'a', coordinate.Rank - 1];
}

public class MoveHandler(Board board)
{
    public void Make(Move move)
    {
        if (!board.TryGetProperties(move.From, out var fromPiece))
            throw new InvalidMoveException(move, $"No piece exists at {move.From}");

        bool pawnIsPromoted = (fromPiece.Type == PieceType.Pawn && move.To.File == 'a') || move.To.File == 'h';
        var toPiece = pawnIsPromoted
            ? new Properties(PieceType.Queen, fromPiece.Colour)
            : fromPiece;

        board.RemovePieceAt(move.From);
        board.SetPiece(move.To, toPiece);
    }
}