using DChess.Core;
using static DChess.Core.Coordinate;
using static DChess.Core.Piece;

namespace DChess.Test.Unit;

public class PieceFlyweightTests
{
// tests in this class
// # Allow a piece to be moved
// - [X] Add a method on the piece base class to move by coordinate.
// - [ ] Add a method to check if the move is generally valid.
// - [ ] Disallow taking your own piece.
// - [ ] Disallow taking your own piece.

    [Fact(DisplayName = "A piece can be moved")]
    public void a_piece_can_be_moved()
    {
        // Arrange
        var board = new Board();
        var piece = new Piece(PieceType.Pawn, PieceColour.White);

        board[a1] = piece;
        board.PieceFlyweights.Count.Should().Be(1);

        // Act
        var pieceFlyweight = board.PieceFlyweights[a1];
        pieceFlyweight.Move(b1);

        // Assert
        board.PieceFlyweights[b1].Should()
            .BeEquivalentTo(new PawnFlyweight(piece, b1, board), "the piece should be moved");
        board.PieceFlyweights.Count.Should().Be(1, "the piece should be moved, not duplicated");
    }

    [Fact(DisplayName = "Invalid move should not be allowed")]
    public void invalid_move_should_not_be_allowed()
    {
        // Arrange
        var board = new Board();
        var piece = new Piece(PieceType.Pawn, PieceColour.White);
        board[a1] = piece;

        // Act
        var pieceFlyweight = board.PieceFlyweights[a1];
        var act = () => pieceFlyweight.Move(a2);

        // Assert
        act.Should().Throw<InvalidMoveException>();
        board.PieceFlyweights.Count.Should().Be(1, "pawns can only move forward");
    }
}