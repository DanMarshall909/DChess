using System.Collections.ObjectModel;
using DChess.Core.Moves;
using DChess.Core.Pieces;

namespace DChess.Core.Game;

public sealed class GameState
{
    private readonly Game _game;
    private readonly IInvalidMoveHandler _invalidMoveHandler;
    private readonly ChessBoardState _chessBoardState;
    private readonly PiecePool _pool;

    public GameState(Game game, PiecePool pool, IInvalidMoveHandler invalidMoveHandler, ChessBoardState chessBoardState)
    {
        _game = game;
        _pool = pool;
        _invalidMoveHandler = invalidMoveHandler;
        _chessBoardState = chessBoardState;
    }

    public override string ToString() => Pieces.Where(x => x.Key != Coordinate.None)?.ToString() ?? "Not initialised";

    public ReadOnlyDictionary<Coordinate, Piece> Pieces
    {
        get
        {
            var pieces = new Dictionary<Coordinate, Piece>();
            for (var f = 0; f < 8; f++)
            {
                for (var r = 0; r < 8; r++)
                {
                    var props = _chessBoardState[f, r];
                    if (props == Properties.None) continue;
                    var coordinateFromZeroOffset = CoordinateFromZeroOffset(f, r);
                    pieces.Add(coordinateFromZeroOffset, _pool.PieceWithProperties(coordinateFromZeroOffset, props));
                }
            }

            return new ReadOnlyDictionary<Coordinate, Piece>(pieces);
        }
    }

    private static Coordinate CoordinateFromZeroOffset(int fileArrayOffset, int rankArrayOffset)
        => new((byte)((fileArrayOffset & 0b111) | ((rankArrayOffset & 0b111) << 3)));


    public bool HasPieceAt(Coordinate coordinate)
    {
        var properties = _chessBoardState[coordinate.File, coordinate.Rank];
        return properties != Properties.None;
    }

    public void Place(Properties pieceProperties, Coordinate at)
        => _chessBoardState[at] = pieceProperties;

    public IEnumerable<Piece> FriendlyPieces(Colour colour)
    {
        for (var f = 0; f < 8; f++)
        for (var r = 0; r < 8; r++)
        {
            var props = _chessBoardState[f, r];
            if (props.Colour == colour)
                yield return _pool.PieceWithProperties(CoordinateFromZeroOffset(f, r), props);
        }
    }

    public IEnumerable<Piece> OpposingPieces(Colour colour)
        => FriendlyPieces(colour == White ? Black : White);

    public bool TryGetProperties(Coordinate coordinate, out Properties properties)
    {
        properties = _chessBoardState[coordinate];
        return properties != Properties.None;
    }

    public bool TryGetPiece(Coordinate at, out Piece piece)
    {
        var p = TryGetProperties(at, out var properties) ? properties : Properties.None;
        if (p == Properties.None)
        {
            piece = _pool.PieceWithProperties(at, properties);
            return false;
        }

        piece = _pool.PieceWithProperties(at, properties);
        return true;
    }

    public void ClearProperties()
    {
        _chessBoardState.Clear();
    }

    public Game Clone() => new(_invalidMoveHandler, _chessBoardState);

    public Coordinate KingCoordinate(Colour colour)
    {
        return _chessBoardState.Find(props => props.Type == PieceType.King && props.Colour == colour);
    }

    public void RemovePieceAt(Coordinate moveFrom)
    {
        _chessBoardState[moveFrom] = Properties.None;
    }

    public void SetPiece(Coordinate moveTo, Properties to)
    {
        _chessBoardState[moveTo] = to;
    }

    public Properties GetProperties(Coordinate coordinate) =>
        _chessBoardState[coordinate];

    public bool IsInCheck(Colour colour)
    {
        var king = KingCoordinate(colour);
        foreach (var p in OpposingPieces(colour))
        {
            if (p.CanMoveTo(king))
            {
                return true;
            }
        }

        return false;
    }
}