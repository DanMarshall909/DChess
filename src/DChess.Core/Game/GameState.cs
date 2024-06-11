using System.Collections.ObjectModel;
using DChess.Core.Moves;
using DChess.Core.Pieces;

namespace DChess.Core.Game;

public sealed class GameState
{
    private readonly IInvalidMoveHandler _invalidMoveHandler;
    private readonly BoardState _boardState;
    private readonly PiecePool _pool;

    public BoardState BoardState => _boardState;
    public Colour CurrentPlayer { get; set; } = White;

    public GameState(PiecePool pool, IInvalidMoveHandler invalidMoveHandler, BoardState boardState)
    {
        _pool = pool;
        _invalidMoveHandler = invalidMoveHandler;
        _boardState = boardState;
    }

    public string AsText => this.RenderToText();

    public bool HasLegalMoves(Colour colour)
    {
        foreach (var piece in FriendlyPieces(colour))
        {
            foreach (var move in piece.LegalMoves(this))
            {
                var result = piece.CheckMove(move.To, this);
                if (result.IsValid)
                    return true;
            }
        }

        return false;
    }

    public override string ToString() => Pieces.Where(x => x.Key != Coordinate.None)?.ToString() ?? "Not initialised";

    public ReadOnlyDictionary<Coordinate, Piece> Pieces
    {
        get
        {
            var pieces = new Dictionary<Coordinate, Piece>();
            for (var f = 0; f < 8; f++)
            for (var r = 0; r < 8; r++)
            {
                var props = _boardState[f, r];
                if (props == Properties.None) continue;
                var coordinateFromZeroOffset = CoordinateFromZeroOffset(f, r);
                pieces.Add(coordinateFromZeroOffset, _pool.PieceWithProperties(coordinateFromZeroOffset, props));
            }

            return new ReadOnlyDictionary<Coordinate, Piece>(pieces);
        }
    }

    private static Coordinate CoordinateFromZeroOffset(int fileArrayOffset, int rankArrayOffset)
        => new((byte)((fileArrayOffset & 0b111) | ((rankArrayOffset & 0b111) << 3)));

    public bool HasPieceAt(Coordinate coordinate)
    {
        var properties = _boardState[coordinate.File, coordinate.Rank];
        return properties != Properties.None;
    }

    public void Place(Properties pieceProperties, Coordinate at)
        => _boardState[at] = pieceProperties;

    public IEnumerable<Piece> FriendlyPieces(Colour colour)
    {
        for (var f = 0; f < 8; f++)
        for (var r = 0; r < 8; r++)
        {
            var props = _boardState[f, r];
            if (props.Colour == colour)
                yield return _pool.PieceWithProperties(CoordinateFromZeroOffset(f, r), props);
        }
    }

    public IEnumerable<Piece> OpposingPieces(Colour colour)
        => FriendlyPieces(colour == White ? Black : White);

    public bool TryGetProperties(Coordinate coordinate, out Properties properties)
    {
        properties = _boardState[coordinate];
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
        _boardState.Clear();
    }

    public Coordinate KingCoordinate(Colour colour)
    {
        return _boardState.Find(props => props.Type == PieceType.King && props.Colour == colour);
    }

    public void RemovePieceAt(Coordinate moveFrom)
    {
        _boardState[moveFrom] = Properties.None;
    }

    public void SetPiece(Coordinate moveTo, Properties to)
    {
        _boardState[moveTo] = to;
    }

    public Properties GetProperties(Coordinate coordinate) =>
        _boardState[coordinate];

    public bool IsInCheck(Colour colour)
    {
        var king = KingCoordinate(colour);
        if (king != Coordinate.None)
            return OpposingPieces(colour).Any(piece => piece.CanMoveTo(king, this));

        _invalidMoveHandler.HandleNoKingFound();
        return false;
    }

    public GameStatus Status(Colour colour)
    {
        GameStatus status;

        bool isInCheck = IsInCheck(colour);
        if (!HasLegalMoves(colour)) return isInCheck ? Checkmate : Stalemate;

        return isInCheck ? Check : InPlay;
    }

    public enum GameStatus
    {
        InPlay,
        Check,
        Checkmate,
        Stalemate
    }

    public GameState AsClone()
    {
        var clone = new GameState(_pool, _invalidMoveHandler, _boardState);
        clone.CurrentPlayer = CurrentPlayer;
        return clone;
    }

    public void Move(Move move, bool force = false)
    {
        var piece = Pieces[move.From];
        if (piece is NullPiece)
            _invalidMoveHandler.HandleNoPieceAt(move.From);

        if (!force)
        {
            var result = piece.CheckMove(move.To, this);
            if (!result.IsValid)
                _invalidMoveHandler.HandleInvalidMove(result);
        }

        bool pawnIsPromoted = (piece.Type == PieceType.Pawn && move.To.File == 'a') || move.To.File == 'h';
        var toPiece = pawnIsPromoted
            ? new Properties(PieceType.Queen, piece.Colour)
            : piece.Properties;

        RemovePieceAt(move.From);
        SetPiece(move.To, toPiece);

        CurrentPlayer = CurrentPlayer == White ? Black : White;
    }
}