using DChess.Core.Game;
using DChess.Core.Renderers;
using FluentAssertions.Execution;

namespace DChess.Test.Unit.Extensions;

public static class BoardAssertions
{
    public static void ShouldHavePieceAt(this Board board, Square square, PieceAttributes expectedPiece, string because = "", params object[] becauseArgs)
    {
        var renderer = new TextRenderer();
        renderer.Render(board);

        Execute.Assertion
            .ForCondition(board.TryGetAtributes(square, out var actualPiece) && actualPiece.Equals(expectedPiece))
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected board to have piece {0} at {1}, but found {2}.\n\nBoard state:\n{3}",
                expectedPiece,
                square,
                board.TryGetAtributes(square, out var piece) ? piece.ToString() : "no piece",
                renderer.LastRender);
    }

    public static void ShouldNotHavePieceAt(this Board board, Square square, string because = "", params object[] becauseArgs)
    {
        var renderer = new TextRenderer();
        renderer.Render(board);

        Execute.Assertion
            .ForCondition(!board.HasPieceAt(square))
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected board to not have a piece at {0}, but found {1}.\n\nBoard state:\n{2}",
                square,
                board.TryGetAtributes(square, out var piece) ? piece.ToString() : "no piece",
                renderer.LastRender);
    }

    public static void ShouldBeValidMove(this Board board, Square from, Square to, string because = "", params object[] becauseArgs)
    {
        var (renderer, pieceAttributes, game, chessPiece, moveResult) = ValidateMove(board, from, to);
        
        if (pieceAttributes == null) return;

        Execute.Assertion
            .ForCondition(moveResult.IsValid)
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected piece {0} at {1} to have a valid move to {2}, but the move is invalid: {3}.\n\nBoard state:\n{4}",
                pieceAttributes,
                from,
                to,
                moveResult.Validity.Message(),
                renderer.LastRender);
    }

    public static void ShouldNotBeValidMove(this Board board, Square from, Square to, string because = "", params object[] becauseArgs)
    {
        var (renderer, pieceAttributes, game, chessPiece, moveResult) = ValidateMove(board, from, to);
        
        if (pieceAttributes == null) return;

        Execute.Assertion
            .ForCondition(!moveResult.IsValid)
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected piece {0} at {1} to not have a valid move to {2}, but the move is valid.\n\nBoard state:\n{3}",
                pieceAttributes,
                from,
                to,
                renderer.LastRender);
    }

    public static void ShouldBeMove(this Move move, Move expected, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(move.Equals(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected move {0}, but got {1}.",
                expected.Format(),
                move.Format());
    }

    public static void ShouldNotBeMove(this Move move, Move expectedNotToBe, string because = "", params object[] becauseArgs)
    {
        Execute.Assertion
            .ForCondition(!move.Equals(expectedNotToBe))
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected move to not be {0}, but it was. {1}",
                expectedNotToBe.Format(),
                because);
    }

    public static string GetBoardState(this Board board)
    {
        var renderer = new TextRenderer();
        renderer.Render(board);
        return renderer.LastRender;
    }

    public static void ShouldBeMoveWithBoard(this Move move, Move expected, Board board, string because = "", params object[] becauseArgs)
    {
        var boardState = board.GetBoardState();
        
        Execute.Assertion
            .ForCondition(move.Equals(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected move {0}, but got {1}.\n\nBoard state:\n{2}",
                expected.Format(),
                move.Format(),
                boardState);
    }

    public static void ShouldNotBeMoveWithBoard(this Move move, Move expectedNotToBe, Board board, string because = "", params object[] becauseArgs)
    {
        var boardState = board.GetBoardState();
        
        Execute.Assertion
            .ForCondition(!move.Equals(expectedNotToBe))
            .BecauseOf(because, becauseArgs)
            .FailWith(
                "Expected move to not be {0}, but it was. {1}\n\nBoard state:\n{2}",
                expectedNotToBe.Format(),
                because,
                boardState);
    }

    private static (TextRenderer renderer, PieceAttributes? pieceAttributes, Game game, dynamic chessPiece, dynamic moveResult) ValidateMove(Board board, Square from, Square to)
    {
        var renderer = new TextRenderer();
        renderer.Render(board);

        if (!board.TryGetAtributes(from, out var pieceAttributes))
        {
            Execute.Assertion
                .FailWith(
                    "Expected a piece at {0} to have a valid move to {1}, but there is no piece at that square.\n\nBoard state:\n{2}",
                    from,
                    to,
                    renderer.LastRender);
            return (renderer, null, null, null, null);
        }

        var game = new Game(board, new TestErrorHandler(), maxAllowableDepth: 3);
        var piece = game.Pieces[from];
        var moveResult = piece.CheckMove(to, game);
        
        return (renderer, pieceAttributes, game, piece, moveResult);
    }
} 