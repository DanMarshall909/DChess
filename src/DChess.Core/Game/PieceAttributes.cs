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

    public static readonly Dictionary<PieceAttributes, char> CharByAttributes = new()
    {
        { new PieceAttributes(Kind.Pawn, White), '♟' },
        { new PieceAttributes(Kind.Rook, White), '♜' },
        { new PieceAttributes(Kind.Knight, White), '♞' },
        { new PieceAttributes(Kind.Bishop, White), '♝' },
        { new PieceAttributes(Kind.Queen, White), '♛' },
        { new PieceAttributes(Kind.King, White), '♚' },
        { new PieceAttributes(Kind.Pawn, Black), '♙' },
        { new PieceAttributes(Kind.Rook, Black), '♖' },
        { new PieceAttributes(Kind.Knight, Black), '♘' },
        { new PieceAttributes(Kind.Bishop, Black), '♗' },
        { new PieceAttributes(Kind.Queen, Black), '♕' },
        { new PieceAttributes(Kind.King, Black), '♔' }
    };

    public static readonly Dictionary<char, PieceAttributes> AttributesByChar =
        CharByAttributes.ToDictionary(x => x.Value, x => x.Key);

    public static char ToChar(PieceAttributes pieceAttributes) => CharByAttributes[pieceAttributes];

    public static PieceAttributes FromChar(char pieceChar) =>
        AttributesByChar.TryGetValue(pieceChar, out var pieceAttributes)
            ? pieceAttributes
            : None;
}