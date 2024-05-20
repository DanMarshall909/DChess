using static DChess.Core.Coordinate;
using static DChess.Core.Piece.PieceColour;
using static DChess.Core.Piece.PieceType;

namespace DChess.Core;

public class Board(PieceCollection? pieces = null)
{
    public const int MaxPieces = 32;
    public readonly PieceCollection Pieces = pieces ?? new PieceCollection();

    public Piece this[Coordinate coordinate]
    {
        get => Pieces[coordinate];
        set => Pieces[coordinate] = value;
    }

    public bool HasPieceAt(Coordinate coordinate) => Pieces.TryGetPiece(coordinate, out _);

    public Piece this[string coordinateString] => Pieces[new Coordinate(coordinateString)];

    public Piece this[char file, byte rank]
    {
        get => Pieces[new(file, rank)];
        set => Pieces[new(file, rank)] = value;
    }

    public static void SetStandardLayout(Board board)
    {
        Place(a8, Rook, Black);
        Place(b8, Knight, Black);
        Place(c8, Bishop, Black);
        Place(d8, Queen, Black);
        Place(e8, King, Black);
        Place(f8, Bishop, Black);
        Place(g8, Knight, Black);
        Place(h8, Rook, Black);

        Place(a7, Pawn, Black);
        Place(b7, Pawn, Black);
        Place(c7, Pawn, Black);
        Place(d7, Pawn, Black);
        Place(e7, Pawn, Black);
        Place(f7, Pawn, Black);
        Place(g7, Pawn, Black);
        Place(h7, Pawn, Black);

        Place(a2, Pawn, White);
        Place(b2, Pawn, White);
        Place(c2, Pawn, White);
        Place(d2, Pawn, White);
        Place(e2, Pawn, White);
        Place(f2, Pawn, White);
        Place(g2, Pawn, White);
        Place(h2, Pawn, White);

        Place(a1, Rook, White);
        Place(b1, Knight, White);
        Place(c1, Bishop, White);
        Place(d1, Queen, White);
        Place(e1, King, White);
        Place(f1, Bishop, White);
        Place(g1, Knight, White);
        Place(h1, Rook, White);
        return;

        void Place(Coordinate coordinate, Piece.PieceType type, Piece.PieceColour colour)
        {
            board.Pieces.Add(new Piece(type, colour, coordinate));
        }
    }
}