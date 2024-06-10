using DChess.Core.Game;
using DChess.Core.Renderers;

namespace DChess.Test.Unit;

public class RenderingTests
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();


    [Fact(DisplayName = "The board should be displayed correctly")]
    public void the_board_should_be_displayed_correctly()
    {
        // Arrange
        var game = new Game(_invalidMoveHandler);
        var renderer = new TextRenderer();

        // Assert
        renderer.Render(game);
        renderer.LastRender.Should().BeEquivalentTo(
            """
             abcdefgh
            8█░█░█░█░
            7░█░█░█░█
            6█░█░█░█░
            5░█░█░█░█
            4█░█░█░█░
            3░█░█░█░█
            2█░█░█░█░
            1░█░█░█░█
            """);
    }

    [Fact(DisplayName = "A standard  board should be displayed correctly with chess pieces")]
    public void a_standard_board_should_be_displayed_correctly_with_pieces()
    {
        // Arrange
        var game = new Game(_invalidMoveHandler);
        game.SetStandardLayout();
        var renderer = new TextRenderer();

        renderer.Render(game);
        (Environment.NewLine + renderer.LastRender).Should().BeEquivalentTo(
            """
            
             abcdefgh
            8♖♘♗♕♔♗♘♖
            7♙♙♙♙♙♙♙♙
            6█░█░█░█░
            5░█░█░█░█
            4█░█░█░█░
            3░█░█░█░█
            2♟♟♟♟♟♟♟♟
            1♜♞♝♛♚♝♞♜
            """);
    }
}