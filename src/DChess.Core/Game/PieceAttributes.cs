namespace DChess.Core.Game;

public readonly struct PieceAttributes(ChessPiece.Type type, Colour colour) : IEquatable<PieceAttributes>
{
    public static readonly PieceAttributes None = default;
    public ChessPiece.Type Type { get; } = type;
    public Colour Colour { get; } = colour;

    public override string ToString() => $"{Colour} {Type}";

    public bool Equals(PieceAttributes other) => Type == other.Type && Colour == other.Colour;

    public override bool Equals(object? obj) => obj is PieceAttributes other && Equals(other);

    public override int GetHashCode() => HashCode.Combine((int)Type, (int)Colour);

    public static bool operator ==(PieceAttributes left, PieceAttributes right) => left.Equals(right);

    public static bool operator !=(PieceAttributes left, PieceAttributes right) => !left.Equals(right);
}