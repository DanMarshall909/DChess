using DChess.Core.Game;
using DChess.UI.WPF.Extensions;

namespace DChess.Test.Unit.TestHelpers;

/// <summary>
///     Base class for tests that need to visualize the chess board.
/// </summary>
public abstract class VisualizationTestBase : GameTestBase
{
    /// <summary>
    ///     Visualizes the current board state in a window.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    /// <param name="waitForClose">Whether to wait for the window to be closed before continuing.</param>
    protected void VisualizeBoard(string title = "Chess Board Visualization", bool waitForClose = false)
    {
        Sut.VisualizeBoard(title, waitForClose);
    }

    /// <summary>
    ///     Visualizes the current board state in a window and waits for the user to close the window.
    /// </summary>
    /// <param name="title">The title of the window.</param>
    protected void VisualizeBoardAndWait(string title = "Chess Board Visualization")
    {
        Sut.VisualizeBoardAndWait(title);
    }

    /// <summary>
    ///     Executes the specified assertion on the board, and visualizes the board if the assertion fails.
    /// </summary>
    /// <param name="assertion">The assertion to execute.</param>
    /// <param name="title">The title of the window.</param>
    protected void AssertBoardState(Action<Board> assertion, string title = "Failed Assertion - Board State")
    {
        try
        {
            assertion(Sut.Board);
        }
        catch (Exception)
        {
            // Visualize board on assertion failure
            Sut.VisualizeBoardAndWait(title);
            throw;
        }
    }

    /// <summary>
    ///     Executes the specified assertion on the game, and visualizes the board if the assertion fails.
    /// </summary>
    /// <param name="assertion">The assertion to execute.</param>
    /// <param name="title">The title of the window.</param>
    protected void AssertGameState(Action<Game> assertion, string title = "Failed Assertion - Game State")
    {
        try
        {
            assertion(Sut);
        }
        catch (Exception)
        {
            // Visualize board on assertion failure
            Sut.VisualizeBoardAndWait(title);
            throw;
        }
    }
}