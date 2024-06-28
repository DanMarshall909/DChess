namespace DChess.Core.Game;

public record Fen
{
    public Colour CurrentPlayer { get; }
    public string CastlingRights { get; }
    public string PossibleEnPassantTargets { get; }
    public string HalfmoveClock { get; }
    public string FullmoveNumber { get; }

    public Fen(string fenString)
    {
        var props = fenString.Split(' ');
        if (props.Length != 6)
            throw new ArgumentException("FEN string must have 6 parts");
        FenString = fenString;
        Board = GetBoard(props[0]);
        CurrentPlayer = props[1] == "w" ? Colour.White : Colour.Black;
        CastlingRights = props[2];
        PossibleEnPassantTargets = props[3];
        HalfmoveClock = props[4];
        FullmoveNumber = props[5];
    }

    public string FenString { get; init; }

    public Fen(Game game)
    {
        FenString = GetFenString(game);
        CurrentPlayer = game.CurrentPlayer;
        Board = game.Board;
        CastlingRights = "";
        PossibleEnPassantTargets = "";
        HalfmoveClock = "0";
        FullmoveNumber = "1";
    }

    private string GetFenString(Game game)
    {
        var sb = new StringBuilder();
        for (int rank = 8; rank >= 1; rank--)
        {
            Append(rank);
        }

        return sb.ToString();

        void Append(int rank)
        {
            int emptySquares = 0;
            for (char file = 'a'; file <= 'h'; file++)
            {
                var square = new Square(file, (byte)rank);
                var piece = game.Board[square];
                if (piece == PieceAttributes.None)
                {
                    emptySquares++;
                }
                else
                {
                    if (emptySquares > 0)
                    {
                        sb.Append(emptySquares);
                        emptySquares = 0;
                    }

                    sb.Append(piece.AsChar);
                }
            }

            if (emptySquares > 0)
            {
                sb.Append(emptySquares);
            }

            if (rank > 1)
            {
                sb.Append('/');
            }
        }
    }


    public Board Board { get; init; }

    private Board GetBoard(string prop)
    {
        var board = new Board();
        string[] ranks = prop.Split('/');

        for (int rank = 8; rank >= 1; rank--)
        {
            char file = 'a';
            string line = ranks[8 - rank].Trim();

            foreach (char pieceChar in line)
            {
                if (char.IsDigit(pieceChar))
                {
                    file += (char)(pieceChar - '0');
                }
                else
                {
                    var square = new Square(file, (byte)rank);
                    var piece = PieceAttributes.FromChar(pieceChar);
                    if (piece != PieceAttributes.None)
                    {
                        board[square] = piece;
                    }

                    file++;
                }
            }
        }

        return board;
    }
}