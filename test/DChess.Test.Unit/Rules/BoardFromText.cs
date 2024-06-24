using DChess.Core.Game;

namespace DChess.Test.Unit.Rules;

internal class BoardExtensions
{
    public static Board FromText(string text)
    {
        var board = new Board();
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