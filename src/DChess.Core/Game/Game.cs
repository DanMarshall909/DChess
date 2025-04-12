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
        Checkmate,
        Stalemate
    }

    private Board _board;
    private readonly IErrorHandler _errorHandler;
    private readonly MoveHandler _moveHandler;
    private readonly int _maxAllowableDepth;
    private Game _lastMoveGameState;
    private List<Move> _moveHistory = new List<Move>();

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
                var squareFromZeroOffset = Square.FromZeroOffset(f, r);
                pieces.Add(squareFromZeroOffset
                    , PieceFlyweightPool.PieceWithContext(new PieceContext(squareFromZeroOffset, props)));
            }

            return new ReadOnlyDictionary<Square, ChessPiece>(pieces);
        }
    }
    
    public void OpenLichess()
    {
        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
        {
            FileName = AsLichessUrl,
            UseShellExecute = true // This is important for URLs
        });
    }

    public Move LastMove { get; private set; }

    public Game AsClone() =>
        new(_board, _errorHandler, _maxAllowableDepth)
        {
            CurrentPlayer = CurrentPlayer
        };

    public override string ToString() => new Fen(this).FenString;
    
    public string AsLichessUrl => "https://lichess.org/editor/" + this.ToString();

    public IEnumerable<ChessPiece> FriendlyPieces(Colour colour)
    {
        // todo: optimise
        List<ChessPiece> pieces = new List<ChessPiece>();
        for (var f = 0; f < 8; f++)
        for (var r = 0; r < 8; r++)
        {
            var props = _board[f, r];
            if (props.Colour == colour)
                pieces.Add(PieceFlyweightPool.PieceWithContext(new PieceContext(Square.FromZeroOffset(f, r), props)));
        }

        return pieces;
    }

    public IEnumerable<ChessPiece> OpposingPieces(Colour colour)
        => FriendlyPieces(colour.Invert());

    public bool TryGetPiece(Square at, out ChessPiece chessPiece)
    {
        var a = _board.TryGetAtributes(at, out var properties) ? properties : PieceAttributes.None;
        if (a == PieceAttributes.None)
        {
            chessPiece = PieceFlyweightPool.PieceWithContext(new(at, properties));
            return false;
        }

        chessPiece = PieceFlyweightPool.PieceWithContext(new(at, properties));
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
        if (hasLegalMoves) return isInCheck ? Check : InPlay;
        return isInCheck ? Checkmate : Stalemate;
    }

    public void Make(Move move)
    {
        _lastMoveGameState = this.AsClone();
        _moveHandler.Make(move, this);
        _moveHistory.Add(move);
    }

    public void Move(Square from, Square to)
    {
        _moveHandler.Make(new Move(from, to), this);
    }

    public Task MakeBestMove(Colour colour)
    {
        var move = _moveHandler.GetBestMove(this, colour);
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
        _board = _lastMoveGameState.Board;
        CurrentPlayer = _lastMoveGameState.CurrentPlayer;
    }

    public void Set(string fenString)
    {
        Set(new Fen(fenString));
    }
}