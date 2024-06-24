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
        { WhitePawn, '♟' },
        { WhiteRook, '♜' },
        { WhiteKnight, '♞' },
        { WhiteBishop, '♝' },
        { WhiteQueen, '♛' },
        { WhiteKing, '♚' },
        { BlackPawn, '♙' },
        { BlackRook, '♖' },
        { BlackKnight, '♘' },
        { BlackBishop, '♗' },
        { BlackQueen, '♕' },
        { BlackKing, '♔' }
    };

    public static PieceAttributes WhiteRook => new(Kind.Rook, White);
    public static PieceAttributes WhiteKnight => new(Kind.Knight, White);
    public static PieceAttributes WhiteBishop => new(Kind.Bishop, White);
    public static PieceAttributes WhiteQueen => new(Kind.Queen, White);
    public static PieceAttributes WhiteKing => new(Kind.King, White);
    public static PieceAttributes BlackPawn => new(Kind.Pawn, Black);
    public static PieceAttributes BlackRook => new(Kind.Rook, Black);
    public static PieceAttributes BlackKnight => new(Kind.Knight, Black);
    public static PieceAttributes BlackBishop => new(Kind.Bishop, Black);
    public static PieceAttributes BlackQueen => new(Kind.Queen, Black);
    public static PieceAttributes BlackKing => new(Kind.King, Black);

    public static readonly Dictionary<char, PieceAttributes> AttributesByChar =
        CharByAttributes.ToDictionary(x => x.Value, x => x.Key);

    public static char ToChar(PieceAttributes pieceAttributes) => CharByAttributes[pieceAttributes];

    public static PieceAttributes FromChar(char pieceChar) =>
        AttributesByChar.TryGetValue(pieceChar, out var pieceAttributes)
            ? pieceAttributes
            : None;
}