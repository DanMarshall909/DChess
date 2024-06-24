namespace DChess.Core;

public static class GameStateExtensions
{
    public static string RenderToText(this Board board)
    {
        var renderer = new TextRenderer();
        renderer.Render(board);
        return renderer.LastRender;
    }
}