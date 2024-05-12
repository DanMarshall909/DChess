using DChess.Core;

namespace DChess.Test.Unit;

public class BoardTests
{
    [Fact(DisplayName = "The board should be displayed correctly")]
    public void the_board_should_be_displayed_correctly()
    {
        // Arrange
        var board = new Board();

        // Assert
        board.PrettyText.Should().BeEquivalentTo(
            """
            █ ░ █ ░ █ ░ █ ░
            ░ █ ░ █ ░ █ ░ █
            █ ░ █ ░ █ ░ █ ░
            ░ █ ░ █ ░ █ ░ █
            █ ░ █ ░ █ ░ █ ░
            ░ █ ░ █ ░ █ ░ █
            █ ░ █ ░ █ ░ █ ░
            ░ █ ░ █ ░ █ ░ █
            """);
    }

    [Fact(DisplayName = "A piece can be placed on the board")]
    public void a_piece_can_be_placed_on_the_board()
    {
        // Arrange
        var board = new Board();
        var piece = new Pawn(PieceType.Pawn, PieceColor.White);

        // Act
        board['a', 1] = piece;

        // Assert
        board['a', 1].Should().Be(piece);
    }

    [Fact(DisplayName = "If there are no pieces on the board, a cell's piece is null")]
    public void if_there_are_no_pieces_on_the_board_a_cells_piece_is_null()
    {
        // Arrange
        var board = new Board();

        // Act
        var piece = board.Cells['a', 1].Piece;

        // Assert
        piece.Should().BeNull();
    }

    [Theory(DisplayName = "A piece cannot be placed outside the board")]
    [InlineData('a', -1)]
    [InlineData('a', 0)]
    [InlineData('a', 9)]
    [InlineData('h', 1)]
    [InlineData('1', 1)]
    public void a_piece_cannot_be_placed_outside_the_board(char column, int row)
    {
        // Arrange
        var board = new Board();
        var piece = new Pawn(PieceType.Pawn, PieceColor.White);

        // Act
        Action act = () => board[column, row] = piece;

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}