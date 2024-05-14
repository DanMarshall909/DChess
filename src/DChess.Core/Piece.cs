using static DChess.Core.PieceType;

namespace DChess.Core;

public record struct Piece(PieceType Type, PieceColour Colour)
{
    public override string ToString() => DisplayChar().ToString();

    public char DisplayChar()
    {
        return Colour switch
        {
            PieceColour.Black => Type switch
            {
                Pawn => '♙',
                Rook => '♖',
                Knight => '♘',
                Bishop => '♗',
                Queen => '♕',
                King => '♔',
                _ => throw new ArgumentOutOfRangeException(nameof(Type), Type, "Unknown piece type")
            },
            _ => Type switch
            {
                Pawn => '♟',
                Rook => '♜',
                Knight => '♞',
                Bishop => '♝',
                Queen => '♛',
                King => '♚',
                _ => throw new ArgumentOutOfRangeException(nameof(Type), Type, "Unknown piece type")
            }
        };
    }
}

public enum PieceType
{
    Pawn,
    Rook,
    Knight,
    Bishop,
    Queen,
    King
}

public enum PieceColour
{
    White, Black
}