namespace DChess.UI.WPF.Extensions;

using DChess.UI.WPF.Renderers;

/// <summary>
/// Extension methods for visualizing chess boards.
/// </summary>
public static class BoardVisualizationExtensions
{
    /// <summary>
    /// Visualizes the specified board in a WPF window.
    /// </summary>
    /// <param name="board">The board to visualize.</param>
    /// <param name="title">The title of the window.</param>
    /// <param name="waitForClose">Whether to wait for the window to be closed before continuing.</param>
    public static void Visualize(this Board board, string title = "Chess Board Visualization", bool waitForClose = false)
    {
        var renderer = new WpfBoardRenderer(title, waitForClose);
        renderer.Render(board);
    }

    /// <summary>
    /// Visualizes the board of the specified game in a WPF window.
    /// </summary>
    /// <param name="game">The game whose board to visualize.</param>
    /// <param name="title">The title of the window.</param>
    /// <param name="waitForClose">Whether to wait for the window to be closed before continuing.</param>
    public static void VisualizeBoard(this Game game, string title = "Chess Board Visualization", bool waitForClose = false)
    {
        game.Board.Visualize(title, waitForClose);
    }

    /// <summary>
    /// Visualizes the board of the specified game in a WPF window and waits for the user to close the window.
    /// </summary>
    /// <param name="game">The game whose board to visualize.</param>
    /// <param name="title">The title of the window.</param>
    public static void VisualizeBoardAndWait(this Game game, string title = "Chess Board Visualization")
    {
        game.VisualizeBoard(title, true);
    }
}
