using DChess.Core.Game;
using DChess.Test.Unit.TestHelpers;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit.Examples;

/// <summary>
///     Example tests that demonstrate how to use the visualization capabilities with best move calculations.
/// </summary>
public class BestMoveVisualizationExampleTests : MoveHandlerTestBase
{
    [Fact(DisplayName = "Example of visualizing best move calculation")]
    public void example_of_visualizing_best_move_calculation()
    {
        // Arrange - Set up a board position
        Sut.Set("r1bqkbnr/pppp1ppp/2n5/4p3/4P3/5N2/PPPP1PPP/RNBQKB1R w KQkq - 2 3");

        // Visualize the board (this will open a window but not block)
        // Comment out for normal test runs, uncomment for debugging
        // VisualizeBoard("Ruy Lopez Position");

        // Act - Calculate the best move
        var bestMove = MoveHandler.GetBestMove(Sut, White, 3);

        // Assert with automatic visualization on failure
        bestMove.Format().Should().Be("b1c3", "the best move should be Nc3", Sut, "Best Move Visualization");
    }

    [Fact(DisplayName = "Example of visualizing multiple best move options")]
    public void example_of_visualizing_multiple_best_move_options()
    {
        // Arrange - Set up a board position with multiple good options
        Sut.Set("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");

        // Act - Calculate the best move
        var bestMove = MoveHandler.GetBestMove(Sut, White, 2);

        // Assert with automatic visualization on failure
        // This will show the board if the assertion fails
        // We're checking if the best move is one of the common opening moves
        var commonOpeningMoves = new[] { "e2e4", "d2d4", "g1f3", "c2c4" };
        commonOpeningMoves.Should().Contain(bestMove.Format(),
            "the best move should be a common opening move", Sut, "Opening Move Visualization");
    }

    [Fact(DisplayName = "Example of visualizing a tactical best move")]
    public void example_of_visualizing_a_tactical_best_move()
    {
        // Arrange - Set up a position with a tactical opportunity
        // This position has a knight fork available
        Sut.Set("r1bqkb1r/pppp1ppp/2n2n2/4p3/4P3/2N2N2/PPPP1PPP/R1BQKB1R w KQkq - 4 4");

        // Act - Calculate the best move
        var bestMove = MoveHandler.GetBestMove(Sut, White, 3);

        // Assert with automatic visualization on failure
        // Use the AssertBestMove helper method which will visualize on failure
        AssertBestMove(White, new Move("f3e5"), 3, "the best move should be the knight fork Nxe5");
    }

    [Fact(DisplayName = "Example of visualizing a checkmate in one")]
    public void example_of_visualizing_a_checkmate_in_one()
    {
        // Arrange - Set up a position with checkmate in one
        Sut.Set("r1bqkb1r/pppp1Qpp/2n2n2/4p3/4P3/5N2/PPPP1PPP/RNB1KB1R b KQkq - 0 4");

        // Visualize the board (this will open a window but not block)
        // Comment out for normal test runs, uncomment for debugging
        // VisualizeBoard("Checkmate in One Position");

        // Act - Check if black is in checkmate
        bool isInCheckmate = Sut.Status(Black) == Checkmate;

        // Assert with automatic visualization on failure
        isInCheckmate.Should().BeTrue("black should be in checkmate", Sut, "Checkmate Visualization");
    }

    [Fact(DisplayName = "Example of visualizing a capture sequence")]
    public void example_of_visualizing_a_capture_sequence()
    {
        // Arrange - Set up a position with a capture sequence
        Sut.Set("rnbqkbnr/ppp2ppp/8/3pp3/4P3/5N2/PPPP1PPP/RNBQKB1R w KQkq - 0 3");

        // Act - Calculate the best move (should be exd5)
        var bestMove = MoveHandler.GetBestMove(Sut, White);

        // Assert with automatic visualization on failure
        bestMove.Format().Should().Be("e4d5", "the best move should be exd5", Sut, "Capture Sequence Visualization");

        // Check if the destination square has a piece (which will be captured)
        bool destinationHasPiece = Sut.Board.HasPieceAt(new Square("d5"));
        destinationHasPiece.Should()
            .BeTrue("there should be a piece to capture at d5", Sut, "Pre-Capture Visualization");

        // Perform the move
        Sut.Make(bestMove);

        // Verify the move was made correctly
        Sut.Board.HasPieceAt(new Square("e4")).Should()
            .BeFalse("the e4 pawn should have moved", Sut, "Post-Capture Visualization");
        Sut.Board.HasPieceAt(new Square("d5")).Should()
            .BeTrue("the pawn should now be at d5", Sut, "Capture Result Visualization");
    }

    [Fact(DisplayName = "Example of visualizing a game state score")]
    public void example_of_visualizing_a_game_state_score()
    {
        // Arrange - Set up a position with material advantage
        Sut.Set("rnbqkbnr/ppp2ppp/8/3P4/8/8/PPPP1PPP/RNBQKBNR b KQkq - 0 3");

        // Act - Calculate the game state score
        int score = MoveHandler.GetGameStateScore(Sut, White);

        // Assert with automatic visualization on failure
        score.Should().BeGreaterThan(0, "white should have a material advantage", Sut,
            "Material Advantage Visualization");

        // Use the AssertGameStateScore helper method which will visualize on failure
        AssertGameStateScore(White, 1, "white should have a pawn advantage");
    }
}