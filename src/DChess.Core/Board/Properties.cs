namespace DChess.Core.Board;

public readonly struct Properties(PieceType type, Colour colour) : IEquatable<Properties>
{
    public static readonly Properties None = default;
    public PieceType Type { get; private init; } = type;
    public Colour Colour { get; private init; } = colour;

    public bool Equals(Properties other) => Type == other.Type && Colour == other.Colour;

    public override bool Equals(object? obj) => obj is Properties other && Equals(other);

    public override int GetHashCode() => HashCode.Combine((int)Type, (int)Colour);

    public static bool operator ==(Properties left, Properties right) => left.Equals(right);

    public static bool operator !=(Properties left, Properties right) => !left.Equals(right);
}