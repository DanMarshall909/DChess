using DChess.Core.Renderers;

namespace DChess.Core;

public static class GameStateExtensions
{
    public static string RenderToText(this Game.Board board)
    {
        var renderer = new TextRenderer();
        renderer.Render(board);
        return renderer.LastRender;
    }
}