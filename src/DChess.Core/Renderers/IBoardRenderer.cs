using DChess.Core.Game;

namespace DChess.Core.Renderers;

public interface IBoardRenderer
{
    void Render(GameState gameState);
}