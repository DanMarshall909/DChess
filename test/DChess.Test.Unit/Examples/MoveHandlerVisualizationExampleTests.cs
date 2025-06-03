using DChess.Core.Game;
using DChess.Core.Moves;
using DChess.Test.Unit.TestHelpers;
using DChess.UI.WPF.TestHelpers;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit.Examples;

/// <summary>
/// Example tests that demonstrate how to use the MoveHandlerVisualizer.
/// </summary>
public class MoveHandlerVisualizationExampleTests : MoveHandlerTestBase
{
    [Fact(DisplayName = "Visualize a checkmate position")]
    public void Visualize_Checkmate_Position()
    {
        // Arrange - Set up a checkmate position (Black king in checkmate)
        Sut.Set("k7/PP6/1K6/8/8/8/8/8 w - - 0 1");
        
        // Act - Calculate the game state score
        int score = MoveHandler.GetGameStateScore(Sut, White);
        
        // Assert - Verify the score is correct
        score.Should().Be(Weights.Material.Pawn * 2 + Weights.GameState.Checkmate);
        
        // Visualize - Show the board with the score
        MoveHandlerVisualizer.VisualizeGameStateScore(Sut, White, $"Checkmate Position - Score: {score}");
    }
    
    [Fact(DisplayName = "Visualize a checkmate position with a pawn")]
    public void Visualize_Checkmate_With_Pawn()
    {
        // Arrange - Set up a checkmate position (Black king in checkmate) with a pawn
        Sut.Set("k7/1P6/8/8/8/8/8/K7 w - - 0 1");
        
        // Act - Calculate the game state score
        int score = MoveHandler.GetGameStateScore(Sut, White);
        
        // Assert - Verify the score is correct
        // The score includes the material value (1 pawn) and the game state value (checkmate)
        score.Should().Be(Weights.Material.Pawn + Weights.GameState.Checkmate);
        
        // Visualize - Show the board with the score
        MoveHandlerVisualizer.VisualizeGameStateScore(Sut, White, $"Checkmate With Pawn - Score: {score}");
    }
    
    [Fact(DisplayName = "Visualize the best move in a position")]
    public void Visualize_Best_Move()
    {
        // Arrange - Set up a position where there's a clear best move
        Sut.Set("3k3b/5p2/5P2/p7/8/8/3N4/4K3 w - - 0 1");
        
        // Act - Calculate the best move
        var bestMove = MoveHandler.GetBestMove(Sut, White);
        
        // Assert - Verify the best move is correct
        bestMove.ToString().Should().Contain("d2");
        
        // Visualize - Show the board with the best move
        MoveHandlerVisualizer.VisualizeBestMove(Sut, White, 3, $"Best Move: {bestMove.Format()}");
    }
    
    [Fact(DisplayName = "Visualize a move sequence")]
    public void Visualize_Move_Sequence()
    {
        // Arrange - Set up a starting position
        Sut.Set("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1");
        
        // Act & Visualize - Make a series of moves and visualize each one
        var moves = new[]
        {
            new Move("e2e4"), // King's Pawn Opening
            new Move("e7e5"), // King's Pawn Response
            new Move("g1f3"), // Knight to f3
            new Move("b8c6"), // Knight to c6
            new Move("f1c4"), // Bishop to c4 (Italian Game)
            new Move("f8c5")  // Bishop to c5
        };
        
        foreach (var move in moves)
        {
            // Visualize the move
            MoveHandlerVisualizer.VisualizeMove(Sut, move, $"Move: {move.Format()}");
            
            // Make the move
            Sut.Make(move);
        }
        
        // Visualize the final position
        MoveHandlerVisualizer.VisualizeBoard(Sut, "Final Position - Italian Game");
    }
}
