using DChess.Core.Moves;
using DChess.UI.WPF.Extensions;

namespace DChess.UI.WPF.TestHelpers;

/// <summary>
///     Provides visualization capabilities for MoveHandler tests.
/// </summary>
public static class MoveHandlerVisualizer
{
    /// <summary>
    ///     Visualizes the board for a given game state.
    /// </summary>
    /// <param name="game">The game to visualize.</param>
    /// <param name="title">The title of the visualization window.</param>
    /// <param name="waitForClose">Whether to wait for the window to be closed before continuing.</param>
    public static void VisualizeBoard(Game game, string title = "Board Visualization", bool waitForClose = false)
    {
        game.VisualizeBoard(title, waitForClose);
    }

    /// <summary>
    ///     Visualizes the board for a given game state and waits for the window to be closed.
    /// </summary>
    /// <param name="game">The game to visualize.</param>
    /// <param name="title">The title of the visualization window.</param>
    public static void VisualizeBoardAndWait(Game game, string title = "Board Visualization")
    {
        game.VisualizeBoardAndWait(title);
    }

    /// <summary>
    ///     Visualizes the board for a given move.
    /// </summary>
    /// <param name="game">The game to visualize.</param>
    /// <param name="move">The move to visualize.</param>
    /// <param name="title">The title of the visualization window.</param>
    /// <param name="waitForClose">Whether to wait for the window to be closed before continuing.</param>
    public static void VisualizeMove(Game game, Move move, string title = "Move Visualization",
        bool waitForClose = false)
    {
        // Clone the game to avoid modifying the original
        var clone = game.AsClone();

        // Make the move
        clone.Make(move);

        // Visualize the board
        clone.VisualizeBoard($"{title} - {move.Format()}", waitForClose);
    }

    /// <summary>
    ///     Visualizes the best move for a given game state.
    /// </summary>
    /// <param name="game">The game to visualize.</param>
    /// <param name="colour">The colour to find the best move for.</param>
    /// <param name="maxDepth">The maximum depth to search for the best move.</param>
    /// <param name="title">The title of the visualization window.</param>
    /// <param name="waitForClose">Whether to wait for the window to be closed before continuing.</param>
    public static void VisualizeBestMove(Game game, Colour colour, int maxDepth = 3,
        string title = "Best Move Visualization", bool waitForClose = false)
    {
        // Find the best move
        var bestMove = MoveHandler.GetBestMove(game, colour, maxDepth);

        // Visualize the move
        VisualizeMove(game, bestMove, title, waitForClose);
    }

    /// <summary>
    ///     Visualizes the game state score for a given game state.
    /// </summary>
    /// <param name="game">The game to visualize.</param>
    /// <param name="colour">The colour to calculate the score for.</param>
    /// <param name="title">The title of the visualization window.</param>
    /// <param name="waitForClose">Whether to wait for the window to be closed before continuing.</param>
    public static void VisualizeGameStateScore(Game game, Colour colour,
        string title = "Game State Score Visualization", bool waitForClose = false)
    {
        // Calculate the score
        int score = MoveHandler.GetGameStateScore(game, colour);

        // Visualize the board
        game.VisualizeBoard($"{title} - Score: {score}", waitForClose);
    }
}