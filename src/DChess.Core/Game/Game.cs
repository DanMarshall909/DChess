using System.Collections.ObjectModel;
using DChess.Core.Errors;
using DChess.Core.Moves;
using DChess.Core.Pieces;

namespace DChess.Core.Game;

/// <summary>
///     All the properties of a game at a specific point in play
/// </summary>
public sealed class Game
{
    public enum GameStatus
    {
        InPlay,
        Check,
        Checkmate,
        Stalemate
    }

    private readonly Board _board;
    private readonly IErrorHandler _errorHandler;
    private readonly MoveHandler _moveHandler;

    public Game(Board board, IErrorHandler errorHandler)
    {
        _moveHandler = new MoveHandler(errorHandler);
        _errorHandler = errorHandler;
        _board = Board.CloneOrEmptyIfNull(board);
    }

    public Board Board => _board;
    public Colour CurrentPlayer { get; set; } = White;

    public ReadOnlyDictionary<Coordinate, Piece> Pieces
    {
        get
        {
            var pieces = new Dictionary<Coordinate, Piece>();
            for (var f = 0; f < 8; f++)
            for (var r = 0; r < 8; r++)
            {
                var props = _board[f, r];
                if (props == Properties.None) continue;
                var coordinateFromZeroOffset = Coordinate.FromZeroOffset(f, r);
                pieces.Add(coordinateFromZeroOffset
                    , PiecePool.PieceWithProperties(coordinateFromZeroOffset, props));
            }

            return new ReadOnlyDictionary<Coordinate, Piece>(pieces);
        }
    }

    public Move LastMove { get; set; }

    public bool HasLegalMoves(Colour colour) => GetLegalMoves(colour).Any();

    public IEnumerable<Move> GetLegalMoves(Colour colour)
    {
        foreach (var piece in FriendlyPieces(colour))
        foreach (var moveValidity in piece.MoveValidities(this))
            if (moveValidity.result.IsValid)
                yield return new Move(piece.Coordinate, moveValidity.to);
    }

    public override string ToString() => Pieces.Where(x => x.Key != Coordinate.None)?.ToString() ?? "Not initialised";

    public IEnumerable<Piece> FriendlyPieces(Colour colour)
    {
        // todo: optimise
        for (var f = 0; f < 8; f++)
        for (var r = 0; r < 8; r++)
        {
            var props = _board[f, r];
            if (props.Colour == colour)
                yield return PiecePool.PieceWithProperties(Coordinate.FromZeroOffset(f, r), props);
        }
    }

    public IEnumerable<Piece> OpposingPieces(Colour colour)
        => FriendlyPieces(colour.Invert());

    public bool TryGetPiece(Coordinate at, out Piece piece)
    {
        var p = _board.TryGetProperties(at, out var properties) ? properties : Properties.None;
        if (p == Properties.None)
        {
            piece = PiecePool.PieceWithProperties(at, properties);
            return false;
        }

        piece = PiecePool.PieceWithProperties(at, properties);
        return true;
    }

    public bool IsInCheck(Colour colour)
    {
        var king = Board.KingCoordinate(colour);
        if (king != Coordinate.None)
            return OpposingPieces(colour).Any(piece => piece.CanMoveTo(king, this));

        _errorHandler.HandleNoKingFound(colour);
        return false;
    }

    public GameStatus Status(Colour colour)
    {
        bool isInCheck = IsInCheck(colour);
        if (!HasLegalMoves(colour)) return isInCheck ? Checkmate : Stalemate;

        return isInCheck ? Check : InPlay;
    }

    public Game AsClone() =>
        new(_board, _errorHandler)
        {
            CurrentPlayer = CurrentPlayer
        };

    public void Move(Move move, bool force = false)
    {
        _moveHandler.Make(move, this, force);
    }

    public void Move(Coordinate from, Coordinate to, bool force = false)
    {
        _moveHandler.Make(new Move(from, to), this, force);
    }

    public Task MakeBestMove(Colour colour, CancellationToken token = default)
    {
        var move = _moveHandler.GetBestMove(colour, this);
        Move(move);
        LastMove = move;

        return Task.CompletedTask;
    }
}