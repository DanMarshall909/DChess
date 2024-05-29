namespace DChess.Test.Unit;

public class RenderingTests
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();


    [Fact(DisplayName = "The board should be displayed correctly")]
    public void the_board_should_be_displayed_correctly()
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);
        var renderer = new TextRenderer();

        // Assert
        renderer.Render(board);
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

    [Fact(DisplayName = "A standard  board should be displayed correctly with pieceStruct")]
    public void a_standard_board_should_be_displayed_correctly_with_pieces()
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);
        board.SetStandardLayout();
        var renderer = new TextRenderer();

        renderer.Render(board);
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