using DChess.Core.Game;
using DChess.Core.Renderers;

namespace DChess.Core;

public static class GameStateExtensions
{
    public static string RenderToText(this GameState gameState)
    {
        var renderer = new TextRenderer();
        renderer.Render(gameState);
        return renderer.LastRender;
    }
}