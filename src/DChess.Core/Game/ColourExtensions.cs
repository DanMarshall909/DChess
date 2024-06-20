namespace DChess.Core.Game;

public static class ColourExtensions
{
    public static Colour AsColour(this string colour) =>
        colour.ToLowerInvariant() switch
        {
            "white" => White,
            "black" => Black,
            _ => White
        };

    public static Colour Invert(this Colour colour) => colour switch
    {
        White => Black,
        Black => White,
        _ => None
    };
}