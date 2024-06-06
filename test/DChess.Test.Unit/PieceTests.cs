using DChess.Core.Board;
using DChess.Core.Pieces;

namespace DChess.Test.Unit;

public class PieceTests
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();

    [Fact(DisplayName = "A properties can be moved")]
    public void a_piece_can_be_moved()
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);
        var chessPiece = new Properties(PieceType.Pawn, Colour.White);

        board.Set(a2, chessPiece);
        board.Pieces.Count.Should().Be(1);

        // Act
        var piece = board.Pieces[a2];
        piece.MoveTo(b3);

        // Assert
        var args = new Piece.Arguments(chessPiece, b3, board, _invalidMoveHandler);
        board.Pieces[b3].Should()
            .BeEquivalentTo(new Pawn(args), "the properties should be moved");

        board.Pieces.Count.Should().Be(1, "the properties should be moved, not duplicated");
    }

    [Fact(DisplayName = "Invalid move should not be allowed")]
    public void invalid_move_should_not_be_allowed()
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);
        board.Set(a1, new Properties(PieceType.Pawn, Colour.White));
        var piece = board.Pieces[a1];

        // Act
        piece.MoveTo(a6);

        // Assert
        _invalidMoveHandler.InvalidMoves.Should().HaveCount(1);
    }
}