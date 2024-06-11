namespace DChess.Core.Game;

public readonly struct Properties(PieceType type, Colour colour) : IEquatable<Properties>
{
    public static readonly Properties None = default;
    public PieceType Type { get; } = type;
    public Colour Colour { get; } = colour;

    public override string ToString() => $"{Colour} {Type}";

    public bool Equals(Properties other) => Type == other.Type && Colour == other.Colour;

    public override bool Equals(object? obj) => obj is Properties other && Equals(other);

    public override int GetHashCode() => HashCode.Combine((int)Type, (int)Colour);

    public static bool operator ==(Properties left, Properties right) => left.Equals(right);

    public static bool operator !=(Properties left, Properties right) => !left.Equals(right);
}