using DChess.Core.Game;

namespace DChess.Test.Unit;

public record GameOptions(Colour PlayerColour = Colour.White, string PlayerName = "Player1", string OpponentName = "Player2")
{
    public static GameOptions DefaultGameOptions => new(Colour.White, "Player1", "Player2");
}