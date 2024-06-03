using DChess.Core.Board;
using DChess.Core.Pieces;

namespace DChess.Test.Unit;

public class PieceTests
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();

    [Fact(DisplayName = "A piece can be moved")]
    public void a_piece_can_be_moved()
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);
        var chessPiece = new ChessPiece(PieceType.Pawn, Colour.White);

        board.ChessPieces[a2] = chessPiece;
        board.Pieces.Count.Should().Be(1);

        // Act
        var piece = board.Pieces[a2];
        piece.MoveTo(b3);

        // Assert
        var args = new Piece.Arguments(chessPiece, b3, board, _invalidMoveHandler);
        board.Pieces[b3].Should()
            .BeEquivalentTo(new Pawn(args), "the piece should be moved");

        board.Pieces.Count.Should().Be(1, "the piece should be moved, not duplicated");
    }

    [Fact(DisplayName = "Invalid move should not be allowed")]
    public void invalid_move_should_not_be_allowed()
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);
        board.ChessPieces[a1] = new ChessPiece(PieceType.Pawn, Colour.White);
        var piece = board.Pieces[a1];

        // Act
        piece.MoveTo(a6);

        // Assert
        _invalidMoveHandler.InvalidMoves.Should().HaveCount(1);
    }
}