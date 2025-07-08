using DChess.Core.Errors;
using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.UI.Console;

class Program
{
    private static Game game = null!;
    private static readonly ExceptionErrorHandler errorHandler = new();

    static void Main(string[] args)
    {
        System.Console.WriteLine("=== DChess Console Chess Game ===");
        System.Console.WriteLine("Commands: move (e.g., 'e2e4'), help, quit");
        System.Console.WriteLine();

        InitializeGame();
        GameLoop();
    }

    private static void InitializeGame()
    {
        var board = new Board();
        board.SetStandardLayout();
        game = new Game(board, errorHandler, 3);
        
        System.Console.WriteLine("New game started!");
        DisplayBoard();
        System.Console.WriteLine($"Current player: {game.CurrentPlayer}");
        System.Console.WriteLine();
    }

    private static void GameLoop()
    {
        while (true)
        {
            try
            {
                System.Console.Write("> ");
                System.Console.Out.Flush();
                var input = System.Console.ReadLine();
                
                // Handle EOF or null input (Ctrl+C, Ctrl+D, etc.)
                if (input == null)
                {
                    System.Console.WriteLine("\nGoodbye!");
                    return;
                }
                
                input = input.Trim().ToLower();
                
                if (string.IsNullOrEmpty(input))
                    continue;

            switch (input)
            {
                case "quit":
                case "exit":
                    System.Console.WriteLine("Thanks for playing!");
                    return;

                case "help":
                    DisplayHelp();
                    break;

                case "board":
                    DisplayBoard();
                    break;

                default:
                    if (TryParseMove(input, out var move))
                    {
                        if (TryMakeMove(move))
                        {
                            DisplayBoard();
                            CheckGameStatus();
                            
                            if (game.Status(game.CurrentPlayer) == Game.GameStatus.InPlay)
                            {
                                System.Console.WriteLine("AI is thinking...");
                                MakeAiMove();
                                DisplayBoard();
                                CheckGameStatus();
                            }
                        }
                    }
                    else
                    {
                        System.Console.WriteLine("Invalid command or move format. Try 'help' for instructions.");
                    }
                    break;
            }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"An error occurred: {ex.Message}");
                System.Console.WriteLine("Type 'quit' to exit or continue playing.");
            }
        }
    }

    private static void DisplayBoard()
    {
        System.Console.WriteLine(game.Board.RenderToText());
    }

    private static void DisplayHelp()
    {
        System.Console.WriteLine("Commands:");
        System.Console.WriteLine("  move format: [from][to] (e.g., 'e2e4', 'g1f3')");
        System.Console.WriteLine("  board - display the current board");
        System.Console.WriteLine("  help - show this help");
        System.Console.WriteLine("  quit - exit the game");
        System.Console.WriteLine();
        System.Console.WriteLine("Square notation: files a-h, ranks 1-8");
        System.Console.WriteLine("Example moves: e2e4, g1f3, b1c3, f1c4");
        System.Console.WriteLine();
    }

    private static bool TryParseMove(string input, out Move move)
    {
        move = default;
        
        if (input.Length != 4)
            return false;

        try
        {
            var fromSquare = new Square(input.Substring(0, 2));
            var toSquare = new Square(input.Substring(2, 2));
            move = new Move(fromSquare, toSquare);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private static bool TryMakeMove(Move move)
    {
        try
        {
            // Check if it's a valid move for the current player
            if (!game.TryGetPiece(move.From, out var piece))
            {
                System.Console.WriteLine($"No piece at {move.From}");
                return false;
            }

            if (piece.Colour != game.CurrentPlayer)
            {
                System.Console.WriteLine($"That's not your piece! Current player: {game.CurrentPlayer}");
                return false;
            }

            var moveResult = piece.CheckMove(move.To, game);
            if (!moveResult.IsValid)
            {
                System.Console.WriteLine($"Invalid move: {moveResult.Validity}");
                return false;
            }

            game.Make(move);
            System.Console.WriteLine($"Move: {move.From} -> {move.To}");
            return true;
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error making move: {ex.Message}");
            return false;
        }
    }

    private static void MakeAiMove()
    {
        try
        {
            var aiTask = game.MakeBestMove(game.CurrentPlayer);
            aiTask.Wait();
            
            System.Console.WriteLine($"AI move: {game.LastMove.From} -> {game.LastMove.To}");
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"AI error: {ex.Message}");
        }
    }

    private static void CheckGameStatus()
    {
        var status = game.Status(game.CurrentPlayer);
        
        switch (status)
        {
            case Game.GameStatus.Check:
                System.Console.WriteLine($"{game.CurrentPlayer} is in check!");
                break;
            case Game.GameStatus.Checkmate:
                System.Console.WriteLine($"Checkmate! {game.CurrentPlayer.Opponent()} wins!");
                break;
            case Game.GameStatus.Stalemate:
                System.Console.WriteLine("Stalemate! The game is a draw.");
                break;
            case Game.GameStatus.InPlay:
                System.Console.WriteLine($"Current player: {game.CurrentPlayer}");
                break;
        }
        
        System.Console.WriteLine();
    }
}