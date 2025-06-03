using DChess.Core.Game;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit.Rules;

/// <summary>
/// Tests for check detection functionality in the chess engine.
/// This is a sample implementation to demonstrate the test structure.
/// </summary>
public class CheckDetectionTests : GameTestBase
{
    [Theory(DisplayName = "Check is correctly detected in various positions")]
    [InlineData("k7/8/1R6/8/8/8/8/K7 w - - 0 1", true)]  // Rook puts king in check
    [InlineData("k7/8/2B5/8/8/8/8/K7 w - - 0 1", true)]  // Bishop puts king in check
    [InlineData("k7/8/2N5/8/8/8/8/K7 w - - 0 1", true)]  // Knight puts king in check
    [InlineData("k7/1P6/8/8/8/8/8/K7 w - - 0 1", true)]  // Pawn puts king in check diagonally
    [InlineData("k7/8/1P6/8/8/8/8/K7 w - - 0 1", false)] // Pawn doesn't put king in check (not diagonal)
    [InlineData("k7/8/8/8/8/8/8/K7 w - - 0 1", false)]   // No check
    [InlineData("k7/8/8/8/8/8/p7/K7 w - - 0 1", false)]  // Pawn not in position to give check
    public void check_is_correctly_detected(string fenString, bool expectedInCheck)
    {
        // Arrange
        Sut.Set(fenString);
        
        // Act
        var isInCheck = Sut.IsInCheck(Black);
        
        // Assert
        isInCheck.Should().Be(expectedInCheck, 
            $"the black king {(expectedInCheck ? "should" : "should not")} be in check in position: {fenString}");
    }

    [Theory(DisplayName = "Checkmate is correctly detected in various positions")]
    [InlineData("k7/1R6/1R6/8/8/8/8/K7 w - - 0 1", true)]  // Double rook checkmate
    [InlineData("k7/1Q6/8/8/8/8/8/K7 w - - 0 1", true)]    // Queen checkmate
    [InlineData("k7/1R6/8/8/8/8/8/K7 w - - 0 1", false)]   // Check but not checkmate (king can move)
    [InlineData("k7/8/8/8/8/8/8/K7 w - - 0 1", false)]     // No check, no checkmate
    [InlineData("k7/1R6/8/8/8/p7/8/K7 w - - 0 1", false)]  // Check but pawn can block
    public void checkmate_is_correctly_detected(string fenString, bool expectedCheckmate)
    {
        // Arrange
        Sut.Set(fenString);
        
        // Act
        var status = Sut.Status(Black);
        
        // Assert
        if (expectedCheckmate)
        {
            status.Should().Be(Checkmate, 
                $"the black king should be in checkmate in position: {fenString}");
        }
        else
        {
            status.Should().NotBe(Checkmate, 
                $"the black king should not be in checkmate in position: {fenString}");
        }
    }

    [Fact(DisplayName = "Check detection works for both white and black kings")]
    public void check_detection_works_for_both_white_and_black_kings()
    {
        // Arrange - White king in check
        Sut.Set("K7/8/1r6/8/8/8/8/k7 b - - 0 1");
        
        // Act & Assert
        Sut.IsInCheck(White).Should().BeTrue("the white king should be in check");
        Sut.IsInCheck(Black).Should().BeFalse("the black king should not be in check");
        
        // Arrange - Black king in check
        Sut.Set("k7/8/1R6/8/8/8/8/K7 w - - 0 1");
        
        // Act & Assert
        Sut.IsInCheck(Black).Should().BeTrue("the black king should be in check");
        Sut.IsInCheck(White).Should().BeFalse("the white king should not be in check");
    }

    [Fact(DisplayName = "Check from multiple pieces is correctly detected")]
    public void check_from_multiple_pieces_is_correctly_detected()
    {
        // Arrange - Black king in check from two pieces
        Sut.Set("k7/2B5/1R6/8/8/8/8/K7 w - - 0 1");
        
        // Act
        var isInCheck = Sut.IsInCheck(Black);
        
        // Assert
        isInCheck.Should().BeTrue("the black king should be in check from both rook and bishop");
    }

    [Fact(DisplayName = "Check is detected after a move that puts the king in check")]
    public void check_is_detected_after_a_move_that_puts_the_king_in_check()
    {
        // Arrange
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(BlackKing, e8);
        Sut.Board.Place(WhiteRook, a1);
        
        // Act - Move rook to give check
        Sut.Move(a1, a8);
        
        // Assert
        Sut.IsInCheck(Black).Should().BeTrue("the black king should be in check after rook moves to a8");
        Sut.Status(Black).Should().Be(Check, "the game status should be Check for black");
    }

    [Fact(DisplayName = "Check is resolved after a move that blocks the check")]
    public void check_is_resolved_after_a_move_that_blocks_the_check()
    {
        // Arrange - Black king in check from rook, with a pawn that can block
        Sut.Set("k7/8/1R6/8/8/8/p7/K7 b - - 0 1");
        
        // Verify initial check
        Sut.IsInCheck(Black).Should().BeTrue("the black king should be in check initially");
        
        // Act - Move pawn to block check
        Sut.Move(a2, a6);
        
        // Assert
        Sut.IsInCheck(Black).Should().BeFalse("the black king should not be in check after pawn blocks");
        Sut.Status(White).Should().Be(InPlay, "the game status should be InPlay after check is blocked");
    }

    [Fact(DisplayName = "Check is resolved after capturing the checking piece")]
    public void check_is_resolved_after_capturing_the_checking_piece()
    {
        // Arrange - Black king in check from rook, with a bishop that can capture
        Sut.Set("k7/8/1R6/8/8/b7/8/K7 b - - 0 1");
        
        // Verify initial check
        Sut.IsInCheck(Black).Should().BeTrue("the black king should be in check initially");
        
        // Act - Capture the checking rook
        Sut.Move(a3, b6);
        
        // Assert
        Sut.IsInCheck(Black).Should().BeFalse("the black king should not be in check after capturing the rook");
        Sut.Status(White).Should().Be(InPlay, "the game status should be InPlay after check is resolved");
    }

    [Fact(DisplayName = "Check is resolved after moving the king out of check")]
    public void check_is_resolved_after_moving_the_king_out_of_check()
    {
        // Arrange - Black king in check from rook, with space to move
        Sut.Set("k7/8/1R6/8/8/8/8/K7 b - - 0 1");
        
        // Verify initial check
        Sut.IsInCheck(Black).Should().BeTrue("the black king should be in check initially");
        
        // Act - Move king out of check
        Sut.Move(a8, b8);
        
        // Assert
        Sut.IsInCheck(Black).Should().BeFalse("the black king should not be in check after moving away");
        Sut.Status(White).Should().Be(InPlay, "the game status should be InPlay after check is resolved");
    }
}
