﻿using DChess.Core;

namespace DChess.Test.Unit;

public class RenderingTests
{
    [Fact(DisplayName = "The board should be displayed correctly")]
    public void the_board_should_be_displayed_correctly()
    {
        // Arrange
        var board = new Board();
        var renderer = new TextRenderer();

        // Assert
        renderer.Render(board);
        renderer.LastRender.Should().BeEquivalentTo(
            """
            █░█░█░█░
            ░█░█░█░█
            █░█░█░█░
            ░█░█░█░█
            █░█░█░█░
            ░█░█░█░█
            █░█░█░█░
            ░█░█░█░█
            """);
    }

    [Fact(DisplayName = "A standard  board should be displayed correctly with pieces")]
    public void a_standard_board_should_be_displayed_correctly_with_pieces()
    {
        // Arrange
        var board = new Board();
        Board.SetStandardLayout(board);
        var renderer = new TextRenderer();

        renderer.Render(board);
        renderer.LastRender.Should().BeEquivalentTo(
            """
            ♖♘♗♕♔♗♘♖
            ♙♙♙♙♙♙♙♙
            █░█░█░█░
            ░█░█░█░█
            █░█░█░█░
            ░█░█░█░█
            ♟♟♟♟♟♟♟♟
            ♜♞♝♛♚♝♞♜
            """);
    }
}