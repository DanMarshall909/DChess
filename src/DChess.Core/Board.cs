using static DChess.Core.Coordinate;
using static DChess.Core.PieceProperties;
using static DChess.Core.PieceProperties.PieceColour;

namespace DChess.Core;

public class Board(PieceCollection? pieces = null)
{
    public const int MaxPieces = 32;
    public readonly PieceCollection Pieces = pieces ?? new PieceCollection();

    public PieceProperties this[Coordinate coordinate]
    {
        get => Pieces[coordinate];
        set => Pieces[coordinate] = value;
    }

    public bool HasPieceAt(Coordinate coordinate) => Pieces.TryGetPiece(coordinate, out _);

    public PieceProperties this[string coordinateString] => Pieces[new Coordinate(coordinateString)];

    public PieceProperties this[char file, byte rank]
    {
        get => Pieces[new(file, rank)];
        set => Pieces[new(file, rank)] = value;
    }

    public static void SetStandardLayout(Board board)
    {
        Place(a8, PieceType.Rook, Black);
        Place(b8, PieceType.Knight, Black);
        Place(c8, PieceType.Bishop, Black);
        Place(d8, PieceType.Queen, Black);
        Place(e8, PieceType.King, Black);
        Place(f8, PieceType.Bishop, Black);
        Place(g8, PieceType.Knight, Black);
        Place(h8, PieceType.Rook, Black);

        Place(a7, PieceType.Pawn, Black);
        Place(b7, PieceType.Pawn, Black);
        Place(c7, PieceType.Pawn, Black);
        Place(d7, PieceType.Pawn, Black);
        Place(e7, PieceType.Pawn, Black);
        Place(f7, PieceType.Pawn, Black);
        Place(g7, PieceType.Pawn, Black);
        Place(h7, PieceType.Pawn, Black);

        Place(a2, PieceType.Pawn, White);
        Place(b2, PieceType.Pawn, White);
        Place(c2, PieceType.Pawn, White);
        Place(d2, PieceType.Pawn, White);
        Place(e2, PieceType.Pawn, White);
        Place(f2, PieceType.Pawn, White);
        Place(g2, PieceType.Pawn, White);
        Place(h2, PieceType.Pawn, White);

        Place(a1, PieceType.Rook, White);
        Place(b1, PieceType.Knight, White);
        Place(c1, PieceType.Bishop, White);
        Place(d1, PieceType.Queen, White);
        Place(e1, PieceType.King, White);
        Place(f1, PieceType.Bishop, White);
        Place(g1, PieceType.Knight, White);
        Place(h1, PieceType.Rook, White);
        return;

        void Place(Coordinate coordinate, PieceType type, PieceColour colour)
        {
            board.Pieces.Add(new PieceProperties(type, colour, coordinate));
        }
    }
}