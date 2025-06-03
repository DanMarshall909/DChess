using DChess.Core.Game;
using DChess.UI.WPF.Extensions;
using FluentAssertions;
using FluentAssertions.Primitives;

namespace DChess.Test.Unit.Extensions;

/// <summary>
/// Extensions for FluentAssertions to integrate with the chess visualization system.
/// </summary>
public static class FluentAssertionsExtensions
{
    /// <summary>
    /// Extends BooleanAssertions to visualize the game board if the assertion fails.
    /// </summary>
    /// <param name="assertion">The boolean assertion.</param>
    /// <param name="game">The game to visualize.</param>
    /// <param name="title">The title for the visualization window.</param>
    /// <param name="because">A formatted phrase explaining why the assertion should be satisfied.</param>
    /// <param name="becauseArgs">Zero or more values to use for filling in any {n} placeholders in the because text.</param>
    /// <returns>The same assertion instance for chaining.</returns>
    public static AndConstraint<BooleanAssertions> BeTrue(
        this BooleanAssertions assertion,
        Game game,
        string title = "Assertion Failed",
        string because = "",
        params object[] becauseArgs)
    {
        try
        {
            return assertion.BeTrue(because, becauseArgs);
        }
        catch (Exception)
        {
            // Visualize the board on failure
            game.VisualizeBoardAndWait($"{title} - {because}");
            throw;
        }
    }

    /// <summary>
    /// Extends BooleanAssertions to visualize the game board if the assertion fails.
    /// </summary>
    /// <param name="assertion">The boolean assertion.</param>
    /// <param name="game">The game to visualize.</param>
    /// <param name="title">The title for the visualization window.</param>
    /// <param name="because">A formatted phrase explaining why the assertion should be satisfied.</param>
    /// <param name="becauseArgs">Zero or more values to use for filling in any {n} placeholders in the because text.</param>
    /// <returns>The same assertion instance for chaining.</returns>
    public static AndConstraint<BooleanAssertions> BeFalse(
        this BooleanAssertions assertion,
        Game game,
        string title = "Assertion Failed",
        string because = "",
        params object[] becauseArgs)
    {
        try
        {
            return assertion.BeFalse(because, becauseArgs);
        }
        catch (Exception)
        {
            // Visualize the board on failure
            game.VisualizeBoardAndWait($"{title} - {because}");
            throw;
        }
    }

    /// <summary>
    /// Extends ObjectAssertions to visualize the game board if the assertion fails.
    /// </summary>
    /// <typeparam name="T">The type of the object.</typeparam>
    /// <param name="assertion">The object assertion.</param>
    /// <param name="game">The game to visualize.</param>
    /// <param name="expected">The expected value.</param>
    /// <param name="title">The title for the visualization window.</param>
    /// <param name="because">A formatted phrase explaining why the assertion should be satisfied.</param>
    /// <param name="becauseArgs">Zero or more values to use for filling in any {n} placeholders in the because text.</param>
    /// <returns>The same assertion instance for chaining.</returns>
    public static AndConstraint<ObjectAssertions> Be<T>(
        this ObjectAssertions assertion,
        T expected,
        Game game,
        string title = "Assertion Failed",
        string because = "",
        params object[] becauseArgs)
    {
        try
        {
            return assertion.Be(expected, because, becauseArgs);
        }
        catch (Exception)
        {
            // Visualize the board on failure
            game.VisualizeBoardAndWait($"{title} - {because}");
            throw;
        }
    }

    /// <summary>
    /// Extends ObjectAssertions to visualize the game board if the assertion fails.
    /// </summary>
    /// <param name="assertion">The object assertion.</param>
    /// <param name="game">The game to visualize.</param>
    /// <param name="title">The title for the visualization window.</param>
    /// <param name="because">A formatted phrase explaining why the assertion should be satisfied.</param>
    /// <param name="becauseArgs">Zero or more values to use for filling in any {n} placeholders in the because text.</param>
    /// <returns>The same assertion instance for chaining.</returns>
    public static AndConstraint<ObjectAssertions> NotBeNull(
        this ObjectAssertions assertion,
        Game game,
        string title = "Assertion Failed",
        string because = "",
        params object[] becauseArgs)
    {
        try
        {
            return assertion.NotBeNull(because, becauseArgs);
        }
        catch (Exception)
        {
            // Visualize the board on failure
            game.VisualizeBoardAndWait($"{title} - {because}");
            throw;
        }
    }
}
