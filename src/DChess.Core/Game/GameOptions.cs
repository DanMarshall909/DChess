namespace DChess.Core.Game;

public record GameOptions(Colour PlayerColour = White, string PlayerName = "Player1", string OpponentName = "Player2")
{
    public static GameOptions DefaultGameOptions => new(White, "Player1", "Player2");
}