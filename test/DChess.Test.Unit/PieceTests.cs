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
        var pieceStruct = new ChessPiece(PieceType.Pawn, Colour.White);

        board[a1] = pieceStruct;
        board.Pieces.Count.Should().Be(1);

        // Act
        var piece = board.Pieces[a1];
        piece.MoveTo(b1);

        // Assert
        var args = new Piece.Arguments(pieceStruct, b1, board, _invalidMoveHandler);
        board.Pieces[b1].Should()
            .BeEquivalentTo(new Pawn(args), "the piece should be moved");
        board.Pieces.Count.Should().Be(1, "the piece should be moved, not duplicated");
    }

    [Fact(DisplayName = "Invalid move should not be allowed")]
    public void invalid_move_should_not_be_allowed()
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);
        var pieceStruct = new ChessPiece(PieceType.Pawn, Colour.White);
        board[a1] = pieceStruct;
        var piece = board.Pieces[a1];

        // Act
        piece.MoveTo(a2);

        // Assert
        _invalidMoveHandler.InvalidMoves.Should().HaveCount(1);
    }
}