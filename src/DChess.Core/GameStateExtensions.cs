using DChess.Core.Renderers;

namespace DChess.Core;

public static class GameStateExtensions
{
    public static string RenderToText(this Game.Game game)
    {
        var renderer = new TextRenderer();
        renderer.Render(game);
        return renderer.LastRender;
    }
}