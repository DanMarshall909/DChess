using System.Collections.ObjectModel;
using DChess.Core.Moves;
using DChess.Core.Pieces;

namespace DChess.Core.Game;

/// <summary>
/// All the properties of a game at a specific point in play
/// </summary>
public sealed class GameState
{
    private readonly MoveHandler _moveHandler;
    private readonly IErrorHandler _errorHandler;
    private readonly BoardState _boardState;

    public BoardState BoardState => _boardState;
    public Colour CurrentPlayer { get; set; } = White;
    public GameState(BoardState boardState, IErrorHandler errorHandler)
    {
        _moveHandler = new MoveHandler(errorHandler) ;
        _errorHandler = errorHandler;
        _boardState = BoardState.CloneOrEmptyIfNull(boardState);
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
                var coordinateFromZeroOffset = Coordinate.FromZeroOffset(f, r);
                pieces.Add(coordinateFromZeroOffset
                    , PiecePool.PieceWithProperties(coordinateFromZeroOffset, props));
            }

            return new ReadOnlyDictionary<Coordinate, Piece>(pieces);
        }
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
                yield return PiecePool.PieceWithProperties(Coordinate.FromZeroOffset(f, r), props);
        }
    }

    public IEnumerable<Piece> OpposingPieces(Colour colour)
        => FriendlyPieces(colour.Invert());

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
            piece = PiecePool.PieceWithProperties(at, properties);
            return false;
        }

        piece = PiecePool.PieceWithProperties(at, properties);
        return true;
    }

    public void Clear()
    {
        _boardState.Clear();
    }

    public Coordinate KingCoordinate(Colour colour)
    {
        return _boardState.Find(props => props.Type == PieceType.King && props.Colour == colour);
    }

    public Properties GetProperties(Coordinate coordinate) =>
        _boardState[coordinate];

    public bool IsInCheck(Colour colour)
    {
        var king = KingCoordinate(colour);
        if (king != Coordinate.None)
            return OpposingPieces(colour).Any(piece => piece.CanMoveTo(king, this));

        _errorHandler.HandleNoKingFound();
        return false;
    }

    public GameStatus Status(Colour colour)
    {
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

    public GameState AsClone() =>
        new(_boardState, _errorHandler)
        {
            CurrentPlayer = CurrentPlayer
        };

    public void Move(Move move, bool force = false)
    {
        _moveHandler.Make(move, this, force);
    }
    public void Move(Coordinate from, Coordinate to, bool force = false)
    {
        _moveHandler.Make(new(from, to), this, force);
    }

    /// <summary>
    ///     The vertical ranks (rows) of the board, from 1 to 8
    /// </summary>
    public static byte[] Ranks = { 1, 2, 3, 4, 5, 6, 7, 8 };

    /// <summary>
    ///     The horizontal files (columns) of the board, from a to h
    /// </summary>
    public static char[] Files = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };
    
    /// <summary>
    /// All the coordinates on the board
    /// </summary>
    public static IEnumerable<Coordinate> AllCoordinates { get; } = new[]
    {
        a1, b1, c1, d1, e1, f1, g1, h1,
        a2, b2, c2, d2, e2, f2, g2, h2,
        a3, b3, c3, d3, e3, f3, g3, h3,
        a4, b4, c4, d4, e4, f4, g4, h4,
        a5, b5, c5, d5, e5, f5, g5, h5,
        a6, b6, c6, d6, e6, f6, g6, h6,
        a7, b7, c7, d7, e7, f7, g7, h7,
        a8, b8, c8, d8, e8, f8, g8, h8
    };
}