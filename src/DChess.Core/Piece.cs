using static DChess.Core.PieceType;

namespace DChess.Core;

public record struct Piece(PieceType Type, PieceColor Colour)
{
    public override string ToString()
    {
        return Colour switch
        {
            PieceColor.Black => Type switch
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

public enum PieceColor
{
    White, Black
}