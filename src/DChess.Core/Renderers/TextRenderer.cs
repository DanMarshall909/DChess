namespace DChess.Core.Renderers;

public class TextRenderer : IBoardRenderer
{
    private const char WhiteSquare = '\u2588';
    private const char BlackSquare = '\u2591';

    public string LastRender { get; private set; } = string.Empty;

    public void Render(Board board)
    {
        var result = new StringBuilder(" abcdefgh" + Environment.NewLine);
        for (byte rank = 8; rank >= 1; rank--)
        {
            result.Append(rank);

            for (var file = 'a'; file <= 'h'; file++)
            {
                char cell = RenderCell(board, file, rank);

                result.Append(cell);
            }

            if (rank > 1) // Don't append newline on the last line
                result.AppendLine();
        }

        LastRender = result.ToString();
    }

    private static char RenderCell(Board board, char file, byte rank) => PieceChar(file, rank, board);

    private static char PieceChar(char file, byte rank, Board board)
    {
        if (board.TryGetAtrributes(new Square(file, rank), out var chessPiece))
            return DisplayChar(chessPiece);

        bool isOddSquare = (rank + file) % 2 == 0;
        return isOddSquare ? BlackSquare : WhiteSquare;
    }

    private static char DisplayChar(PieceAttributes pieceAttributes)
    {
        return pieceAttributes.Colour switch
        {
            Black => pieceAttributes.Kind switch
            {
                Kind.Pawn => '♙',
                Kind.Rook => '♖',
                Kind.Knight => '♘',
                Kind.Bishop => '♗',
                Kind.Queen => '♕',
                Kind.King => '♔',
                _ => throw new ArgumentOutOfRangeException(nameof(Kind), pieceAttributes.Kind, "Unknown pieceAttributes kind")
            },
            _ => pieceAttributes.Kind switch
            {
                Kind.Pawn => '♟',
                Kind.Rook => '♜',
                Kind.Knight => '♞',
                Kind.Bishop => '♝',
                Kind.Queen => '♛',
                Kind.King => '♚',
                _ => throw new ArgumentOutOfRangeException(nameof(Kind), pieceAttributes.Kind, "Unknown pieceAttributes kind")
            }
        };
    }
}