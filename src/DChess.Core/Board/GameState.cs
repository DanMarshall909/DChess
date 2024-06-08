using System.Collections.ObjectModel;
using DChess.Core.Moves;
using DChess.Core.Pieces;

namespace DChess.Core.Board;

public sealed class GameState
{
    private readonly Game _game;
    private readonly IInvalidMoveHandler _invalidMoveHandler;
    private Properties[,] _properties;
    private readonly PiecePool _pool;

    public GameState(Game game, PiecePool pool, IInvalidMoveHandler invalidMoveHandler, Properties[,] properties)
    {
        _game = game;
        _pool = pool;
        _invalidMoveHandler = invalidMoveHandler;
        _properties = properties;
    }

    public ReadOnlyDictionary<Coordinate, Piece> Pieces
    {
        get
        {
            var pieces = new Dictionary<Coordinate, Piece>();
            for (var f = 0; f < 8; f++)
            {
                for (var r = 0; r < 8; r++)
                {
                    var props = _properties[f, r];
                    if (props == Properties.None) continue;
                    var coordinateFromZeroOffset = CoordinateFromZeroOffset(f, r);
                    pieces.Add(coordinateFromZeroOffset, _pool.GetPiece(coordinateFromZeroOffset, props));
                }
            }

            return new ReadOnlyDictionary<Coordinate, Piece>(pieces);
        }
    }

    private static Coordinate CoordinateFromZeroOffset(int fileArrayOffset, int rankArrayOffset)
        => new((byte)((fileArrayOffset & 0b111) | ((rankArrayOffset & 0b111) << 3)));


    public bool HasPieceAt(Coordinate coordinate)
    {
        var properties = _properties[coordinate.File - 'a', coordinate.Rank - 1];
        return properties != Properties.None;
    }

    public void Set(Coordinate coordinate, Properties properties)
        => _properties[coordinate.File - 'a', coordinate.Rank - 1] = properties;

    public IEnumerable<Piece> FriendlyPiecesByCoordinate(Colour colour)
    {
        for (var f = 0; f < 8; f++)
        {
            for (var r = 0; r < 8; r++)
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
            piece = new NullPiece(new(p, at, _game, _invalidMoveHandler));
            return false;
        }

        piece = _pool.GetPiece(at, properties);
        return true;
    }

    public void Clear()
    {
        _properties = new Properties[8, 8];
    }

    public Game Clone()
    {
        return new Game(_invalidMoveHandler, _properties);
    }

    public Coordinate GetKingCoordinate(Colour colour)
    {
        // get index of king
        for (var f = 0; f < 8; f++)
        {
            for (var r = 0; r < 8; r++)
            {
                var props = _properties[f, r];
                if (props.Type == PieceType.King && props.Colour == colour)
                    return new Coordinate(Game.Files[f], Game.Ranks[r]);
            }
        }

        return NullCoordinate;
    }

    public void RemovePieceAt(Coordinate moveFrom)
    {
        _properties[moveFrom.File - 'a', moveFrom.Rank - 1] = Properties.None;
    }

    public void SetPiece(Coordinate moveTo, Properties to)
    {
        _properties[moveTo.File - 'a', moveTo.Rank - 1] = to;
    }

    public Properties GetProperties(Coordinate coordinate) =>
        _properties[coordinate.File - 'a', coordinate.Rank - 1];
}