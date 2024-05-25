using DChess.Core;
using static DChess.Core.Coordinate;
using static DChess.Core.PieceStruct;

namespace DChess.Test.Unit;

public class PieceTests
{
    [Fact(DisplayName = "A piece can be moved")]
    public void a_piece_can_be_moved()
    {
        // Arrange
        var board = new Board();
        var pieceStruct = new PieceStruct(PieceType.Pawn, PieceColour.White);

        board[a1] = pieceStruct;
        board.Pieces.Count.Should().Be(1);

        // Act
        var piece = board.Pieces[a1];
        piece.MoveTo(b1);

        // Assert
        board.Pieces[b1].Should()
            .BeEquivalentTo(new Pawn(pieceStruct, b1, board), "the piece should be moved");
        board.Pieces.Count.Should().Be(1, "the piece should be moved, not duplicated");
    }

    [Fact(DisplayName = "Invalid move should not be allowed")]
    public void invalid_move_should_not_be_allowed()
    {
        // Arrange
        var board = new Board();
        var pieceStruct = new PieceStruct(PieceType.Pawn, PieceColour.White);
        board[a1] = pieceStruct;

        // Act
        var piece = board.Pieces[a1];
        var act = () => piece.MoveTo(a2);

        // Assert
        act.Should().Throw<InvalidMoveException>();
        board.Pieces.Count.Should().Be(1, "pawns can only move forward");
    }

    [Fact(DisplayName = "A piece cannot take its own pieceStruct")]
    public void a_piece_cannot_take_its_own_piece()
    {
        // Arrange
        var board = new Board();
        var whitePawn = new PieceStruct(PieceType.Pawn, PieceColour.White);

        board[a1] = whitePawn;
        board[b1] = whitePawn;

        // Act
        var act = () => board.Pieces[a1].MoveTo(b1);
        act.Should().Throw<InvalidMoveException>("a piece cannot take its own pieceStruct");
    }
}