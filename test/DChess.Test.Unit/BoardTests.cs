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
        board['A', 1] = piece;

        // Assert
        board['A', 1].Should().Be(piece);
    }

    [Fact(DisplayName = "If there is no piece on the board, a null piece should be returned")]
    public void if_there_is_no_piece_on_the_board_a_null_piece_should_be_returned()
    {
        // Arrange
        var board = new Board();

        // Act
        var piece = board['A', 1];
        var pieceType = piece.GetType();

        // Assert
        pieceType.Should().Be(typeof(NullPiece));
    }

    [Theory(DisplayName = "A piece cannot be placed outside the board")]
    [InlineData('A', -1)]
    [InlineData('A', 0)]
    [InlineData('A', 9)]
    [InlineData('H', 1)]
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