using DChess.Core;
using static DChess.Core.Coordinate;
using static DChess.Core.Piece;

namespace DChess.Test.Unit;

public class PieceFlyweightTests
{
// tests in this class
// # Allow a piece to be moved
// - [ ] Add a method on the piece base class to move by coordinate.
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
        pieceFlyweight.Move(a3);

        // Assert
        board.PieceFlyweights[a3].Should().BeEquivalentTo(new PawnFlyweight(piece, a3, board), "the piece should be moved");
        board.PieceFlyweights.Count.Should().Be(1, "the piece should be moved, not duplicated");
    }
}