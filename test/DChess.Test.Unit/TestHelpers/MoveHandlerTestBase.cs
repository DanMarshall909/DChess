using DChess.Core.Game;
using DChess.Core.Moves;
using DChess.Test.Unit.TestHelpers;
using DChess.UI.WPF.TestHelpers;

namespace DChess.Test.Unit.TestHelpers;

/// <summary>
/// Base class for MoveHandler tests that includes visualization capabilities.
/// </summary>
public abstract class MoveHandlerTestBase : VisualizationTestBase
{
    /// <summary>
    /// Visualizes the best move for the current game state.
    /// </summary>
    /// <param name="colour">The colour to find the best move for.</param>
    /// <param name="maxDepth">The maximum depth to search for the best move.</param>
    /// <param name="title">The title of the visualization window.</param>
    /// <param name="waitForClose">Whether to wait for the window to be closed before continuing.</param>
    protected void VisualizeBestMove(Colour colour, int maxDepth = 3, string title = "Best Move Visualization", bool waitForClose = false)
    {
        MoveHandlerVisualizer.VisualizeBestMove(Sut, colour, maxDepth, title, waitForClose);
    }

    /// <summary>
    /// Visualizes the game state score for the current game state.
    /// </summary>
    /// <param name="colour">The colour to calculate the score for.</param>
    /// <param name="title">The title of the visualization window.</param>
    /// <param name="waitForClose">Whether to wait for the window to be closed before continuing.</param>
    protected void VisualizeGameStateScore(Colour colour, string title = "Game State Score Visualization", bool waitForClose = false)
    {
        MoveHandlerVisualizer.VisualizeGameStateScore(Sut, colour, title, waitForClose);
    }

    /// <summary>
    /// Visualizes a move on the current game state.
    /// </summary>
    /// <param name="move">The move to visualize.</param>
    /// <param name="title">The title of the visualization window.</param>
    /// <param name="waitForClose">Whether to wait for the window to be closed before continuing.</param>
    protected void VisualizeMove(Move move, string title = "Move Visualization", bool waitForClose = false)
    {
        MoveHandlerVisualizer.VisualizeMove(Sut, move, title, waitForClose);
    }

    /// <summary>
    /// Asserts that the game state score for the specified colour matches the expected score.
    /// Visualizes the board if the assertion fails.
    /// </summary>
    /// <param name="colour">The colour to calculate the score for.</param>
    /// <param name="expectedScore">The expected score.</param>
    /// <param name="because">A message explaining why the score should match the expected value.</param>
    protected void AssertGameStateScore(Colour colour, int expectedScore, string because = "")
    {
        try
        {
            int actualScore = MoveHandler.GetGameStateScore(Sut, colour);
            actualScore.Should().Be(expectedScore, because);
        }
        catch (Exception)
        {
            // Visualize the board and score on assertion failure
            VisualizeGameStateScore(colour, "Failed Score Assertion", true);
            throw;
        }
    }

    /// <summary>
    /// Asserts that the best move for the specified colour matches the expected move.
    /// Visualizes the board and the best move if the assertion fails.
    /// </summary>
    /// <param name="colour">The colour to find the best move for.</param>
    /// <param name="expectedMove">The expected best move.</param>
    /// <param name="maxDepth">The maximum depth to search for the best move.</param>
    /// <param name="because">A message explaining why the move should match the expected value.</param>
    protected void AssertBestMove(Colour colour, Move expectedMove, int maxDepth = 3, string because = "")
    {
        try
        {
            var bestMove = MoveHandler.GetBestMove(Sut, colour, maxDepth);
            bestMove.Format().Should().Be(expectedMove.Format(), because);
        }
        catch (Exception)
        {
            // Visualize the board and best move on assertion failure
            VisualizeBestMove(colour, maxDepth, "Failed Best Move Assertion", true);
            throw;
        }
    }
}
