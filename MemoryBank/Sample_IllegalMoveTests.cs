using DChess.Core.Game;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit.Rules;

/// <summary>
/// Tests for illegal move detection in the chess engine.
/// This is a sample implementation to demonstrate the test structure.
/// </summary>
public class IllegalMoveTests : GameTestBase
{
    [Fact(DisplayName = "Move that would put own king in check is invalid")]
    public void move_that_would_put_own_king_in_check_is_invalid()
    {
        // Arrange - Position where moving a piece would expose king to check
        Sut.Set("k7/8/8/8/8/3b4/4P3/4K3 w - - 0 1");
        
        // Act
        var result = Sut.Pieces[e2].CheckMove(e3, Sut);
        
        // Assert
        result.IsValid.Should().BeFalse("moving the pawn would expose the king to check from the bishop");
        result.Validity.Should().Be(MovingThisPieceWouldPutYourKingInCheck);
    }

    [Fact(DisplayName = "Pinned piece cannot move if it would expose king to check")]
    public void pinned_piece_cannot_move_if_it_would_expose_king_to_check()
    {
        // Arrange - Position with a pinned piece
        Sut.Set("k7/8/8/8/8/8/3P4/3K1r2 w - - 0 1");
        
        // Act - Try to move the pinned pawn
        var result = Sut.Pieces[d2].CheckMove(d3, Sut);
        
        // Assert
        result.IsValid.Should().BeFalse("the pawn is pinned to the king by the rook");
        result.Validity.Should().Be(MovingThisPieceWouldPutYourKingInCheck);
    }

    [Fact(DisplayName = "Pinned piece can move along the pin line")]
    public void pinned_piece_can_move_along_the_pin_line()
    {
        // Arrange - Position with a pinned piece
        Sut.Set("k7/8/8/8/8/8/3P4/3K1r2 w - - 0 1");
        
        // Act - Try to move the pinned piece along the pin line
        var result = Sut.Pieces[d2].CheckMove(e3, Sut);
        
        // Assert
        result.IsValid.Should().BeFalse("even though the move is along the pin line, pawns can't move diagonally unless capturing");
        
        // Set up a different position with a pinned bishop
        Sut.Set("k7/8/8/8/8/8/3B4/3K1r2 w - - 0 1");
        
        // Act - Try to move the pinned bishop along the pin line
        result = Sut.Pieces[d2].CheckMove(e3, Sut);
        
        // Assert
        result.IsValid.Should().BeTrue("the bishop can move along the pin line");
    }

    [Fact(DisplayName = "When in check, only moves that remove check are valid")]
    public void when_in_check_only_moves_that_remove_check_are_valid()
    {
        // Arrange - Position with king in check and a piece that can block
        Sut.Set("k7/8/8/8/8/8/3P4/3K1r2 w - - 0 1");
        
        // Verify king is in check
        Sut.IsInCheck(White).Should().BeTrue("the white king should be in check from the rook");
        
        // Act & Assert - Try to move a piece that doesn't block check
        var result = Sut.Pieces[d2].CheckMove(d3, Sut);
        result.IsValid.Should().BeFalse("moving the pawn doesn't remove the check");
        
        // Act & Assert - Try to move the king out of check
        result = Sut.Pieces[d1].CheckMove(c1, Sut);
        result.IsValid.Should().BeTrue("the king can move out of check");
        
        // Set up a different position where a piece can block check
        Sut.Set("k7/8/8/8/8/8/5P2/3K1r2 w - - 0 1");
        
        // Act & Assert - Try to block the check
        result = Sut.Pieces[f2].CheckMove(f1, Sut);
        result.IsValid.Should().BeTrue("the pawn can block the check");
    }

    [Fact(DisplayName = "When in check, capturing the checking piece is valid")]
    public void when_in_check_capturing_the_checking_piece_is_valid()
    {
        // Arrange - Position with king in check and a piece that can capture the checker
        Sut.Set("k7/8/8/8/8/5P2/8/3K1r2 w - - 0 1");
        
        // Verify king is in check
        Sut.IsInCheck(White).Should().BeTrue("the white king should be in check from the rook");
        
        // Act - Try to capture the checking piece
        var result = Sut.Pieces[f3].CheckMove(f1, Sut);
        
        // Assert
        result.IsValid.Should().BeTrue("the pawn can capture the checking rook");
    }

    [Fact(DisplayName = "King cannot move to a square that is under attack")]
    public void king_cannot_move_to_a_square_that_is_under_attack()
    {
        // Arrange - Position where king has moves but some are to attacked squares
        Sut.Set("k7/8/8/8/8/8/8/3K1r2 w - - 0 1");
        
        // Act & Assert - Try to move to an attacked square
        var result = Sut.Pieces[d1].CheckMove(e1, Sut);
        result.IsValid.Should().BeFalse("the king cannot move to e1 as it's attacked by the rook");
        
        // Act & Assert - Try to move to a safe square
        result = Sut.Pieces[d1].CheckMove(c1, Sut);
        result.IsValid.Should().BeTrue("the king can move to c1 as it's not attacked");
    }

    [Fact(DisplayName = "King cannot move to a square adjacent to the opponent's king")]
    public void king_cannot_move_to_a_square_adjacent_to_the_opponents_king()
    {
        // Arrange - Position where kings are close
        Sut.Set("8/8/8/8/8/3k4/8/3K4 w - - 0 1");
        
        // Act & Assert - Try to move adjacent to opponent's king
        var result = Sut.Pieces[d1].CheckMove(d2, Sut);
        result.IsValid.Should().BeFalse("the king cannot move adjacent to the opponent's king");
        
        // Act & Assert - Try to move to a valid square
        result = Sut.Pieces[d1].CheckMove(e1, Sut);
        result.IsValid.Should().BeTrue("the king can move to e1 as it's not adjacent to the opponent's king");
    }

    [Fact(DisplayName = "Cannot move a piece from an empty square")]
    public void cannot_move_a_piece_from_an_empty_square()
    {
        // Arrange
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(BlackKing, e8);
        
        // Act
        var result = Sut.Pieces[a1].CheckMove(a2, Sut);
        
        // Assert
        result.IsValid.Should().BeFalse("cannot move from an empty square");
        result.Validity.Should().Be(NoPieceAtSource);
    }

    [Fact(DisplayName = "Cannot move opponent's pieces")]
    public void cannot_move_opponents_pieces()
    {
        // Arrange
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(BlackKing, e8);
        Sut.Board.Place(BlackPawn, d7);
        
        // Act - White tries to move black's pawn
        var result = Sut.Pieces[d7].CheckMove(d6, Sut);
        
        // Assert
        result.IsValid.Should().BeFalse("white cannot move black's pieces");
        result.Validity.Should().Be(CannotMoveOpponentsPiece);
    }

    [Fact(DisplayName = "Cannot move to a square occupied by own piece")]
    public void cannot_move_to_a_square_occupied_by_own_piece()
    {
        // Arrange
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(WhitePawn, d2);
        Sut.Board.Place(WhiteRook, d1);
        
        // Act - Try to move pawn to square occupied by own rook
        var result = Sut.Pieces[d2].CheckMove(d1, Sut);
        
        // Assert
        result.IsValid.Should().BeFalse("cannot move to a square occupied by own piece");
        result.Validity.Should().Be(CannotTakeOwnPiece);
    }
}
