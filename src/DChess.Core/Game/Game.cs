using DChess.Core.Flyweights;

namespace DChess.Core.Game;

/// <summary>
///     All the pieceAttributes of a game at a specific point in play
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

    public ReadOnlyDictionary<Coordinate, PieceFlyweight> Pieces
    {
        get
        {
            var pieces = new Dictionary<Coordinate, PieceFlyweight>();
            for (var f = 0; f < 8; f++)
            for (var r = 0; r < 8; r++)
            {
                var props = _board[f, r];
                if (props == PieceAttributes.None) continue;
                var coordinateFromZeroOffset = Coordinate.FromZeroOffset(f, r);
                pieces.Add(coordinateFromZeroOffset
                    , PieceFlyweightPool.PieceWithProperties(new(coordinateFromZeroOffset, props)));
            }

            return new ReadOnlyDictionary<Coordinate, PieceFlyweight>(pieces);
        }
    }

    public Move LastMove { get; set; }

    public Game AsClone() =>
        new(_board, _errorHandler)
        {
            CurrentPlayer = CurrentPlayer
        };

    public override string ToString() => Pieces.Where(x => x.Key != Coordinate.None)?.ToString() ?? "Not initialised";

    public IEnumerable<PieceFlyweight> FriendlyPieces(Colour colour)
    {
        // todo: optimise
        for (var f = 0; f < 8; f++)
        for (var r = 0; r < 8; r++)
        {
            var props = _board[f, r];
            if (props.Colour == colour)
                yield return PieceFlyweightPool.PieceWithProperties(new(Coordinate.FromZeroOffset(f, r), props));
        }
    }

    public IEnumerable<PieceFlyweight> OpposingPieces(Colour colour)
        => FriendlyPieces(colour.Invert());

    public bool TryGetPiece(Coordinate at, out PieceFlyweight pieceFlyweight)
    {
        var p = _board.TryGetProperties(at, out var properties) ? properties : PieceAttributes.None;
        if (p == PieceAttributes.None)
        {
            pieceFlyweight = PieceFlyweightPool.PieceWithProperties(new(at, properties));
            return false;
        }

        pieceFlyweight = PieceFlyweightPool.PieceWithProperties(new(at, properties));
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
        if (!MoveHandler.HasLegalMoves(colour, this)) return isInCheck ? Checkmate : Stalemate;

        return isInCheck ? Check : InPlay;
    }

    public void Move(Move move)
    {
        _moveHandler.Make(move, this);
    }

    public void Move(Coordinate from, Coordinate to)
    {
        _moveHandler.Make(new Move(from, to), this);
    }

    public Task MakeBestMove(Colour colour, CancellationToken token = default)
    {
        var move = _moveHandler.GetBestMove(this, colour);
        Move(move);
        LastMove = move;

        return Task.CompletedTask;
    }
}