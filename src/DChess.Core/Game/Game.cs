using System.Diagnostics;
using DChess.Core.Flyweights;

namespace DChess.Core.Game;

/// <summary>
///     All the pieceAttributes of a game at a specific point in play
/// </summary>
[DebuggerDisplay("{AsLichessUrl()}")]
public sealed class Game
{
    public enum GameStatus
    {
        InPlay,
        Check,
        OpponentInCheck,
        Checkmate,
        OpponentCheckmate,
        Stalemate,
        Invalid
    }

    private readonly IErrorHandler _errorHandler;
    private readonly int _maxAllowableDepth;
    private readonly MoveHandler _moveHandler;
    private readonly List<Move> _moveHistory = new();

    private Board _board;
    private Game? _lastMoveGameState;

    public Game(Board board, IErrorHandler errorHandler, int maxAllowableDepth)
    {
        _maxAllowableDepth = maxAllowableDepth;
        _moveHandler = new MoveHandler(errorHandler, maxAllowableDepth);
        _errorHandler = errorHandler;
        _board = Board.CloneOrEmptyIfNull(board);
    }

    public Board Board => _board;
    public Colour CurrentPlayer { get; set; } = White;
    public Colour Opponent => CurrentPlayer.Opponent();
    public IReadOnlyList<Move> MoveHistory => _moveHistory.AsReadOnly();

    public ReadOnlyDictionary<Square, ChessPiece> Pieces
    {
        get
        {
            var pieces = new Dictionary<Square, ChessPiece>();
            for (var f = 0; f < 8; f++)
            for (var r = 0; r < 8; r++)
            {
                var props = _board[f, r];
                if (props == PieceAttributes.None) continue;
                var square = Square.FromZeroOffset(f, r);
                pieces.Add(square, ChessPieceFactory.PieceWithContext(new PieceContext(square, props)));
            }

            return new ReadOnlyDictionary<Square, ChessPiece>(pieces);
        }
    }

    public Move LastMove { get; private set; }

    public string AsLichessUrl => "https://lichess.org/editor/" + ToString();

    public void OpenLichess()
    {
        Process.Start(new ProcessStartInfo
        {
            FileName = AsLichessUrl,
            UseShellExecute = true // This is important for URLs
        });
    }

    public Game AsClone() =>
        new(_board, _errorHandler, _maxAllowableDepth)
        {
            CurrentPlayer = CurrentPlayer
        };

    public override string ToString() => new Fen(this).FenString;

    public IEnumerable<ChessPiece> FriendlyPieces(Colour colour)
    {
        for (var f = 0; f < 8; f++)
        for (var r = 0; r < 8; r++)
        {
            var props = _board[f, r];
            if (props.Colour == colour)
                yield return ChessPieceFactory.PieceWithContext(new PieceContext(Square.FromZeroOffset(f, r), props));
        }
    }

    public IEnumerable<ChessPiece> OpposingPieces(Colour colour)
        => FriendlyPieces(colour.Opponent());

    public bool TryGetPiece(Square at, out ChessPiece chessPiece)
    {
        var a = _board.TryGetAtributes(at, out var properties) ? properties : PieceAttributes.None;
        if (a == PieceAttributes.None)
        {
            chessPiece = ChessPieceFactory.PieceWithContext(new PieceContext(at, properties));
            return false;
        }

        chessPiece = ChessPieceFactory.PieceWithContext(new PieceContext(at, properties));
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
        bool hasLegalMoves = MoveHandler.HasLegalMoves(colour, this);

        if (isInCheck)
            return !hasLegalMoves ? Checkmate : Check;

        return IsInCheck(colour.Opponent()) ? OpponentInCheck : InPlay;
    }

    public void Make(Move move)
    {
        _lastMoveGameState = AsClone();
        _moveHandler.Make(move, this);
        _moveHistory.Add(move);
    }

    public void Move(Square from, Square to)
    {
        _moveHandler.Make(new Move(from, to), this);
    }

    public Task MakeBestMove(Colour colour)
    {
        var move = MoveHandler.GetBestMove(this, colour, _maxAllowableDepth);
        Make(move);
        LastMove = move;

        return Task.CompletedTask;
    }

    public void Set(Fen fen)
    {
        _board = fen.Board;
        CurrentPlayer = fen.CurrentPlayer;
    }

    public void UndoLastMove()
    {
        if (_lastMoveGameState == null)
            return;
            
        _board = _lastMoveGameState.Board;
        CurrentPlayer = _lastMoveGameState.CurrentPlayer;
    }

    public void Set(string fenString)
    {
        Set(new Fen(fenString));
    }
}