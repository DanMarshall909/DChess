namespace DChess.Core.Game;

public readonly struct PieceAttributes(Colour colour, Kind kind) : IEquatable<PieceAttributes>
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
        { WhitePawn, 'P' },
        { WhiteRook, 'R' },
        { WhiteKnight, 'N' },
        { WhiteBishop, 'B' },
        { WhiteQueen, 'Q' },
        { WhiteKing, 'K' },
        { BlackPawn, 'p' },
        { BlackRook, 'r' },
        { BlackKnight, 'n' },
        { BlackBishop, 'b' },
        { BlackQueen, 'q' },
        { BlackKing, 'k' }
    };
    
    public static readonly Dictionary<char, PieceAttributes> AttributesByChar =
        CharByAttributes.ToDictionary(x => x.Value, x => x.Key);

    public static char ToChar(PieceAttributes pieceAttributes) => CharByAttributes[pieceAttributes];

    public static PieceAttributes FromChar(char pieceChar) =>
        AttributesByChar.TryGetValue(pieceChar, out var pieceAttributes)
            ? pieceAttributes
            : None;
}