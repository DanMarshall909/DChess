namespace DChess.Core.Game;

public enum Colour
{
    None,
    White,
    Black
}

public static class ColourExtensions
{
    public static Colour Invert(this Colour colour) => colour switch
    {
        White => Black,
        Black => White,
        _ => None
    };
}