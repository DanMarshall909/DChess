namespace DChess.Core.Game;

public readonly struct PieceAttributes(Kind kind, Colour colour) : IEquatable<PieceAttributes>
{
    public static readonly PieceAttributes None = default;
    public Kind Kind { get; } = kind;
    public Colour Colour { get; } = colour;

    public override string ToString() => $"{Colour} {Kind}";

    public bool Equals(PieceAttributes other) => Kind == other.Kind && Colour == other.Colour;

    public override bool Equals(object? obj) => obj is PieceAttributes other && Equals(other);

    public override int GetHashCode() => HashCode.Combine((int)Kind, (int)Colour);

    public static bool operator ==(PieceAttributes left, PieceAttributes right) => left.Equals(right);

    public static bool operator !=(PieceAttributes left, PieceAttributes right) => !left.Equals(right);
}