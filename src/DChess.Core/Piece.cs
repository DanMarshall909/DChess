using static DChess.Core.PieceType;

namespace DChess.Core;

public record struct Piece(PieceType Type, PieceColour Colour)
{
    public override string ToString()
    {
        return Colour switch
        {
            PieceColour.Black => Type switch
            {
                Pawn => "♙",
                Rook => "♖",
                Knight => "♘",
                Bishop => "♗",
                Queen => "♕",
                King => "♔",
                _ => ""
            },
            _ => Type switch
            {
                Pawn => "♟",
                Rook => "♜",
                Knight => "♞",
                Bishop => "♝",
                Queen => "♛",
                King => "♚",
                _ => ""
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