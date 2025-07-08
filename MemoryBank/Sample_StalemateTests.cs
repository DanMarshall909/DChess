using DChess.Core.Game;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit.Rules;

/// <summary>
/// Tests for stalemate detection functionality in the chess engine.
/// This is a sample implementation to demonstrate the test structure.
/// </summary>
public class StalemateTests : GameTestBase
{
    [Theory(DisplayName = "Stalemate is correctly detected in various positions")]
    [InlineData("k7/8/1Q6/8/8/8/8/K7 b - - 0 1", true)]  // Classic queen stalemate
    [InlineData("k7/8/1R6/8/8/8/8/K7 b - - 0 1", false)] // King is in check, not stalemate
    [InlineData("k7/p7/8/8/8/8/8/K7 b - - 0 1", false)]  // King has legal moves, not stalemate
    [InlineData("k7/8/8/8/8/8/P7/K7 b - - 0 1", false)]  // King can move, not stalemate
    [InlineData("k1K5/8/8/8/8/8/8/8 b - - 0 1", false)]  // Kings adjacent, not stalemate (king can move)
    [InlineData("8/8/8/8/8/k1K5/q7/8 w - - 0 1", true)]  // White king stalemated by black queen
    public void stalemate_is_correctly_detected(string fenString, bool expectedStalemate)
    {
        // Arrange
        Sut.Set(fenString);
        
        // Act
        var status = Sut.Status(fenString.Contains("w") ? White : Black);
        
        // Assert
        if (expectedStalemate)
        {
            status.Should().Be(Stalemate, 
                $"the position should be a stalemate in: {fenString}");
        }
        else
        {
            status.Should().NotBe(Stalemate, 
                $"the position should not be a stalemate in: {fenString}");
        }
    }

    [Fact(DisplayName = "Stalemate is detected when king has no legal moves but is not in check")]
    public void stalemate_is_detected_when_king_has_no_legal_moves_but_is_not_in_check()
    {
        // Arrange - Classic stalemate position
        Sut.Set("k7/8/1Q6/8/8/8/8/K7 b - - 0 1");
        
        // Act & Assert
        Sut.IsInCheck(Black).Should().BeFalse("the black king should not be in check");
        MoveHandler.HasLegalMoves(Black, Sut).Should().BeFalse("black should have no legal moves");
        Sut.Status(Black).Should().Be(Stalemate, "the game status should be Stalemate");
    }

    [Fact(DisplayName = "Stalemate is not confused with checkmate")]
    public void stalemate_is_not_confused_with_checkmate()
    {
        // Arrange - Stalemate position
        Sut.Set("k7/8/1Q6/8/8/8/8/K7 b - - 0 1");
        
        // Act & Assert
        Sut.Status(Black).Should().Be(Stalemate, "the position should be stalemate");
        Sut.Status(Black).Should().NotBe(Checkmate, "the position should not be checkmate");
        
        // Arrange - Checkmate position
        Sut.Set("k7/1Q6/8/8/8/8/8/K7 b - - 0 1");
        
        // Act & Assert
        Sut.Status(Black).Should().Be(Checkmate, "the position should be checkmate");
        Sut.Status(Black).Should().NotBe(Stalemate, "the position should not be stalemate");
    }

    [Fact(DisplayName = "Stalemate detection works for both white and black")]
    public void stalemate_detection_works_for_both_white_and_black()
    {
        // Arrange - Black king stalemated
        Sut.Set("k7/8/1Q6/8/8/8/8/K7 b - - 0 1");
        
        // Act & Assert
        Sut.Status(Black).Should().Be(Stalemate, "black should be in stalemate");
        Sut.Status(White).Should().NotBe(Stalemate, "white should not be in stalemate");
        
        // Arrange - White king stalemated
        Sut.Set("8/8/8/8/8/k1K5/q7/8 w - - 0 1");
        
        // Act & Assert
        Sut.Status(White).Should().Be(Stalemate, "white should be in stalemate");
        Sut.Status(Black).Should().NotBe(Stalemate, "black should not be in stalemate");
    }

    [Fact(DisplayName = "Game transitions to stalemate after a move that causes stalemate")]
    public void game_transitions_to_stalemate_after_a_move_that_causes_stalemate()
    {
        // Arrange - Position one move away from stalemate
        Sut.Set("k7/8/8/8/8/8/8/KQ6 w - - 0 1");
        
        // Verify not stalemate initially
        Sut.Status(Black).Should().NotBe(Stalemate, "the position should not be stalemate initially");
        
        // Act - Move queen to create stalemate
        Sut.Move(b1, b6);
        
        // Assert
        Sut.Status(Black).Should().Be(Stalemate, "the position should be stalemate after queen moves to b6");
    }

    [Fact(DisplayName = "Stalemate is correctly detected in complex positions")]
    public void stalemate_is_correctly_detected_in_complex_positions()
    {
        // Arrange - Complex stalemate position
        Sut.Set("5k2/5P2/5K2/8/8/8/8/8 b - - 0 1");
        
        // Act & Assert
        Sut.IsInCheck(Black).Should().BeFalse("the black king should not be in check");
        MoveHandler.HasLegalMoves(Black, Sut).Should().BeFalse("black should have no legal moves");
        Sut.Status(Black).Should().Be(Stalemate, "the game status should be Stalemate");
    }

    [Fact(DisplayName = "Stalemate is correctly detected when only king can move but has no legal moves")]
    public void stalemate_is_correctly_detected_when_only_king_can_move_but_has_no_legal_moves()
    {
        // Arrange - Position where black has pieces but only king can move, and it has no legal moves
        Sut.Set("k7/p7/1Q6/8/8/8/8/K7 b - - 0 1");
        
        // Act & Assert
        Sut.IsInCheck(Black).Should().BeFalse("the black king should not be in check");
        MoveHandler.HasLegalMoves(Black, Sut).Should().BeFalse("black should have no legal moves");
        Sut.Status(Black).Should().Be(Stalemate, "the game status should be Stalemate");
    }

    [Fact(DisplayName = "Stalemate is not detected when pieces other than king can move")]
    public void stalemate_is_not_detected_when_pieces_other_than_king_can_move()
    {
        // Arrange - Position where king has no moves but pawn can move
        Sut.Set("k7/p7/1Q5Q/8/8/8/8/K7 b - - 0 1");
        
        // Act & Assert
        MoveHandler.HasLegalMoves(Black, Sut).Should().BeTrue("black should have legal moves (pawn can move)");
        Sut.Status(Black).Should().NotBe(Stalemate, "the game status should not be Stalemate");
    }
}
