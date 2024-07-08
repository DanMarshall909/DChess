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

    private Board _board;
    private readonly IErrorHandler _errorHandler;
    private readonly MoveHandler _moveHandler;
    private readonly int _maxAllowableDepth;

    public Game(Board board, IErrorHandler errorHandler, int maxAllowableDepth)
    {
        _maxAllowableDepth = maxAllowableDepth;
        _moveHandler = new MoveHandler(errorHandler, maxAllowableDepth);
        _errorHandler = errorHandler;
        _board = Board.CloneOrEmptyIfNull(board);
    }

    public Board Board => _board;
    public Colour CurrentPlayer { get; set; } = White;
    public Colour Opponent => CurrentPlayer.Invert();

    public ReadOnlyDictionary<Square, PieceFlyweight> Pieces
    {
        get
        {
            var pieces = new Dictionary<Square, PieceFlyweight>();
            for (var f = 0; f < 8; f++)
            for (var r = 0; r < 8; r++)
            {
                var props = _board[f, r];
                if (props == PieceAttributes.None) continue;
                var squareFromZeroOffset = Square.FromZeroOffset(f, r);
                pieces.Add(squareFromZeroOffset
                    , PieceFlyweightPool.PieceWithContext(new PieceContext(squareFromZeroOffset, props)));
            }

            return new ReadOnlyDictionary<Square, PieceFlyweight>(pieces);
        }
    }

    public Move LastMove { get; private set; }

    public Game AsClone() =>
        new(_board, _errorHandler, _maxAllowableDepth)
        {
            CurrentPlayer = CurrentPlayer
        };

    public override string ToString() => new Fen(this).FenString;

    public IEnumerable<PieceFlyweight> FriendlyPieces(Colour colour)
    {
        // todo: optimise
        for (var f = 0; f < 8; f++)
        for (var r = 0; r < 8; r++)
        {
            var props = _board[f, r];
            if (props.Colour == colour)
                yield return PieceFlyweightPool.PieceWithContext(new PieceContext(Square.FromZeroOffset(f, r), props));
        }
    }

    public IEnumerable<PieceFlyweight> OpposingPieces(Colour colour)
        => FriendlyPieces(colour.Invert());

    public bool TryGetPiece(Square at, out PieceFlyweight pieceFlyweight)
    {
        var a = _board.TryGetAtributes(at, out var properties) ? properties : PieceAttributes.None;
        if (a == PieceAttributes.None)
        {
            pieceFlyweight = PieceFlyweightPool.PieceWithContext(new(at, properties));
            return false;
        }

        pieceFlyweight = PieceFlyweightPool.PieceWithContext(new(at, properties));
        return true;
    }

    public bool IsInCheck(Colour colour)
    {
        var king = Board.KingSquare(colour);
        if (king != Square.None)
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

    public void Move(Square from, Square to)
    {
        _moveHandler.Make(new Move(from, to), this);
    }

    public Task MakeBestMove(Colour colour)
    {
        var move = _moveHandler.GetBestMove(this, colour);
        Move(move);
        LastMove = move;

        return Task.CompletedTask;
    }

    public void Set(Fen fen)
    {
        _board = fen.Board;
        CurrentPlayer = fen.CurrentPlayer;
    }
}