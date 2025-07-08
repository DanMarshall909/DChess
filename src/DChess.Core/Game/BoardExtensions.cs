namespace DChess.Core.Game;

public static class BoardExtensions
{
    public static void SetStandardLayout(this Board board)
    {
        board.Clear();
        board.Place(BlackRook, a8);
        board.Place(BlackKnight, b8);
        board.Place(BlackBishop, c8);
        board.Place(BlackQueen, d8);
        board.Place(BlackKing, e8);
        board.Place(BlackBishop, f8);
        board.Place(BlackKnight, g8);
        board.Place(BlackRook, h8);

        board.Place(BlackPawn, a7);
        board.Place(BlackPawn, b7);
        board.Place(BlackPawn, c7);
        board.Place(BlackPawn, d7);
        board.Place(BlackPawn, e7);
        board.Place(BlackPawn, f7);
        board.Place(BlackPawn, g7);
        board.Place(BlackPawn, h7);

        board.Place(WhitePawn, a2);
        board.Place(WhitePawn, b2);
        board.Place(WhitePawn, c2);
        board.Place(WhitePawn, d2);
        board.Place(WhitePawn, e2);
        board.Place(WhitePawn, f2);
        board.Place(WhitePawn, g2);
        board.Place(WhitePawn, h2);

        board.Place(WhiteRook, a1);
        board.Place(WhiteKnight, b1);
        board.Place(WhiteBishop, c1);
        board.Place(WhiteQueen, d1);
        board.Place(WhiteKing, e1);
        board.Place(WhiteBishop, f1);
        board.Place(WhiteKnight, g1);
        board.Place(WhiteRook, h1);
    }

    public static Board Set(this Board board, string text)
    {
        board.Clear();
        string[] lines = text.Split(Environment.NewLine);
        for (byte rank = 8; rank >= 1; rank--)
        {
            string line = lines[8 - rank].Trim();
            for (var file = 'a'; file <= 'h'; file++)
            {
                var square = new Square(file, rank);
                char pieceChar = line[file - 'a'];
                var piece = PieceAttributes.FromChar(pieceChar);
                if (piece == PieceAttributes.None) continue;

                board[square] = piece;
            }
        }

        return board;
    }

    public static string RenderToText(this Board board)
    {
        var renderer = new TextRenderer();
        renderer.Render(board);
        return renderer.LastRender;
    }

    public static Board FromText(string text)
    {
        var board = new Board();
        string[] lines = text.Split(Environment.NewLine);
        for (byte rank = 8; rank >= 1; rank--)
        {
            string line = lines[8 - rank].Trim();
            for (var file = 'a'; file <= 'h'; file++)
            {
                var square = new Square(file, rank);
                char pieceChar = line[file - 'a'];
                var piece = PieceAttributes.FromChar(pieceChar);
                if (piece == PieceAttributes.None) continue;

                board[square] = piece;
            }
        }

        return board;
    }
}