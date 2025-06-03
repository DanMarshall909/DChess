using DChess.Test.Unit.TestHelpers;
using static DChess.Core.Game.Colour;
using static DChess.Core.Game.Piece.Kind;

namespace DChess.Test.Unit.Examples;

/// <summary>
///     Example tests that demonstrate how to use the visualization capabilities.
/// </summary>
public class VisualizationExampleTests : VisualizationTestBase
{
    [Fact(DisplayName = "Example of visualizing a board")]
    public void example_of_visualizing_a_board()
    {
        // Arrange - Set up a board position
        Sut.Set("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

        // Visualize the board (this will open a window but not block)
        // Comment out for normal test runs, uncomment for debugging
        // VisualizeBoard("Starting Position");

        // Act & Assert
        Sut.Board.Should().NotBeNull();
    }

    [Fact(DisplayName = "Example of visualizing a board and waiting for user to close")]
    public void example_of_visualizing_a_board_and_waiting_for_user_to_close()
    {
        // Arrange - Set up a board position
        Sut.Set("rnbqkbnr/pppppppp/8/8/4P3/8/PPPP1PPP/RNBQKBNR b KQkq - 0 1");

        // Visualize the board and wait for the user to close the window
        // Comment out for normal test runs, uncomment for debugging
        // VisualizeBoardAndWait("After 1. e4");

        // Act & Assert
        Sut.Board.Should().NotBeNull();
    }

    [Fact(DisplayName = "Example of visualizing a board on assertion failure")]
    public void example_of_visualizing_a_board_on_assertion_failure()
    {
        // Arrange - Set up a board position
        Sut.Set("r1bqkbnr/pppp1ppp/2n5/4p3/4P3/5N2/PPPP1PPP/RNBQKB1R w KQkq - 2 3");

        // Act & Assert with automatic visualization on failure
        // This will only show the board if the assertion fails
        AssertBoardState(board =>
        {
            // This assertion should pass
            board.Should().NotBeNull();

            // Uncomment to see the board visualization on failure
            // board.Should().BeNull("this will fail and show the board");
        }, "Assertion Failed - Ruy Lopez Position");
    }

    [Fact(DisplayName = "Example of visualizing a position")]
    public void example_of_visualizing_a_position()
    {
        // Arrange - Set up a position
        Sut.Set("rnbqk2r/pppp1ppp/5n2/2b1p3/2B1P3/5Q2/PPPP1PPP/RNB1K1NR b KQkq - 5 4");

        // Visualize the board (this will open a window but not block)
        // Comment out for normal test runs, uncomment for debugging
        // VisualizeBoard("Position Visualization");

        // Act & Assert with automatic visualization on failure
        AssertGameState(game =>
        {
            // Simple assertions that will pass
            game.Should().NotBeNull("the game should exist");
            game.Board.Should().NotBeNull("the board should exist");
            game.CurrentPlayer.Should().Be(Black, "it should be black's turn");
        }, "Position Assertion Failed");
    }

    [Fact(DisplayName = "Example of visualizing a checkmate position")]
    public void example_of_visualizing_a_checkmate_position()
    {
        // Arrange - Set up a checkmate position (Scholar's mate)
        Sut.Set("rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPP1PPP/RNBQKBNR w KQkq - 0 2");
        Sut.Move(f1, c4); // 2. Bc4
        Sut.Move(b8, c6); // 2... Nc6
        Sut.Move(d1, h5); // 3. Qh5
        Sut.Move(g8, f6); // 3... Nf6
        Sut.Move(h5, f7); // 4. Qxf7# (checkmate)

        // Visualize the board (this will open a window but not block)
        // Comment out for normal test runs, uncomment for debugging
        // VisualizeBoard("Checkmate Position (Scholar's Mate)");

        // Act & Assert with automatic visualization on failure
        AssertGameState(game =>
        {
            game.IsInCheck(Black).Should().BeTrue("the black king should be in check");
            game.Status(Black).Should().Be(Checkmate, "the game status should be Checkmate");
        }, "Checkmate Position Assertion Failed");
    }

    [Fact(DisplayName = "Example of using FluentAssertions with visualization")]
    public void example_of_using_fluentassertions_with_visualization()
    {
        // Arrange - Set up a position
        Sut.Set("rnbqkbnr/pppp1ppp/8/4p3/4P3/5N2/PPPP1PPP/RNBQKB1R b KQkq - 1 2");

        // Act & Assert using the FluentAssertions extensions
        // These assertions will automatically visualize the board if they fail

        // Boolean assertion with visualization
        Sut.CurrentPlayer.Should().Be(Black, "it should be black's turn", Sut, "Current Player Visualization");

        // Check that a piece is at the expected position
        var knight = Sut.Board[f3];
        knight.Should().NotBeNull("there should be a piece at f3", Sut, "Knight Visualization");
        knight.Kind.Should().Be(Knight, "the piece at f3 should be a knight", Sut, "Knight Type Visualization");

        // Intentionally commented out failing assertion to demonstrate visualization
        // Uncomment to see the visualization when the assertion fails
        // Sut.IsInCheck(Black).Should().BeTrue("black should be in check", Sut, "Check Visualization");
    }
}