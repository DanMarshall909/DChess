namespace DChess.Core.Game;

public static class BoardExtensions
{
    public static void SetStandardLayout(this Board board)
    {
        board.Set(
            """
            rnbqkbnr
            pppppppp
            █░█░█░█░
            ░█░█░█░█
            █░█░█░█░
            ░█░█░█░█
            PPPPPPPP
            RNBQKBNR
            """);
    }

    public static Board Set(this Board board, string text)
    {
        board.Clear();
        var lines = text.Split(Environment.NewLine);
        for (byte rank = 8; rank >= 1; rank--)
        {
            var line = lines[8 - rank].Trim();
            for (var file = 'a'; file <= 'h'; file++)
            {
                var square = new Square(file, rank);
                var pieceChar = line[file - 'a'];
                var piece = PieceAttributes.FromChar(pieceChar);
                if (piece == PieceAttributes.None) continue;

                board[square] = piece;
            }
        }

        return board;
    }
}