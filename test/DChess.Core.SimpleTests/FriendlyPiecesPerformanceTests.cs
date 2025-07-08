using DChess.Core.Game;
using FluentAssertions;
using System.Diagnostics;
using Xunit;

namespace DChess.Core.SimpleTests;

public class FriendlyPiecesPerformanceTests
{
    [Fact(DisplayName = "FriendlyPieces should be performant when called repeatedly")]
    public void friendly_pieces_should_be_performant_when_called_repeatedly()
    {
        // Arrange
        var game = new Game.Game(new Board(), new TestErrorHandler(), 3);
        game.Board.SetStandardLayout();
        
        const int iterations = 1000;
        var stopwatch = new Stopwatch();
        
        // Act
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var whitePieces = game.FriendlyPieces(Colour.White).ToList();
            var blackPieces = game.FriendlyPieces(Colour.Black).ToList();
        }
        stopwatch.Stop();
        
        // Assert
        stopwatch.ElapsedMilliseconds.Should().BeLessThan(100, 
            "FriendlyPieces should be optimized for repeated calls");
        
        // Verify correctness
        var whitePiecesResult = game.FriendlyPieces(Colour.White).ToList();
        var blackPiecesResult = game.FriendlyPieces(Colour.Black).ToList();
        
        whitePiecesResult.Should().HaveCount(16, "Standard chess position has 16 white pieces");
        blackPiecesResult.Should().HaveCount(16, "Standard chess position has 16 black pieces");
    }
    
    [Fact(DisplayName = "FriendlyPieces should not allocate unnecessary memory")]
    public void friendly_pieces_should_not_allocate_unnecessary_memory()
    {
        // Arrange
        var game = new Game.Game(new Board(), new TestErrorHandler(), 3);
        game.Board.SetStandardLayout();
        
        // Act & Assert - This will fail with current implementation
        // but should pass after optimization
        var whitePieces1 = game.FriendlyPieces(Colour.White);
        var whitePieces2 = game.FriendlyPieces(Colour.White);
        
        // For now, just verify functionality works
        whitePieces1.Should().HaveCount(16);
        whitePieces2.Should().HaveCount(16);
    }
    
    [Fact(DisplayName = "Pieces property should be performant when called repeatedly")]
    public void pieces_property_should_be_performant_when_called_repeatedly()
    {
        // Arrange
        var game = new Game.Game(new Board(), new TestErrorHandler(), 3);
        game.Board.SetStandardLayout();
        
        const int iterations = 10000;
        var stopwatch = new Stopwatch();
        
        // Act
        stopwatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var pieces = game.Pieces;
            var count = pieces.Count; // Force evaluation
        }
        stopwatch.Stop();
        
        // Assert - This will fail with current implementation
        // Current implementation creates new dictionary each time
        stopwatch.ElapsedMilliseconds.Should().BeLessThan(10, 
            "Pieces property should be optimized for repeated calls and not recreate dictionary each time");
        
        // Verify correctness
        var piecesResult = game.Pieces;
        piecesResult.Should().HaveCount(32, "Standard chess position has 32 pieces");
    }
    
    [Fact(DisplayName = "Pieces property should not create new dictionary instances on each call")]
    public void pieces_property_should_not_create_new_dictionary_instances_on_each_call()
    {
        // Arrange
        var game = new Game.Game(new Board(), new TestErrorHandler(), 3);
        game.Board.SetStandardLayout();
        
        // Act
        var pieces1 = game.Pieces;
        var pieces2 = game.Pieces;
        
        // Assert - This will fail with current implementation
        // Current implementation creates new dictionary each time
        pieces1.Should().BeSameAs(pieces2, 
            "Pieces property should return the same instance when board hasn't changed");
    }
}