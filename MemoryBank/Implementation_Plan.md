# DChess Test Implementation Plan

This document outlines the step-by-step implementation plan for adding unit tests to the DChess project, focusing on existing features that need additional test coverage.

## Implementation Phases

### Phase 1: Check and Checkmate Detection Tests

1. **Create CheckDetectionTests.cs**
   - Extend GameTestBase
   - Implement theory tests for check detection with various piece types
   - Implement theory tests for checkmate detection in different scenarios

2. **Test Implementation Details**
   - Use FEN notation to set up specific board positions
   - Test check detection from different piece types (rook, bishop, knight, queen, pawn)
   - Test checkmate detection in various configurations
   - Test edge cases where check looks like checkmate but isn't

### Phase 2: Preventing Illegal Moves Tests

1. **Create IllegalMoveTests.cs**
   - Extend GameTestBase
   - Implement tests for moves that would put own king in check
   - Implement tests for pinned pieces
   - Implement tests for legal moves when in check

2. **Test Implementation Details**
   - Set up scenarios where a piece is pinned to the king
   - Create positions where only specific moves can get out of check
   - Test scenarios where a seemingly valid move would expose the king to check

### Phase 3: Stalemate Detection Tests

1. **Create StalemateTests.cs**
   - Extend GameTestBase
   - Implement theory tests for stalemate detection
   - Test various stalemate positions

2. **Test Implementation Details**
   - Set up classic stalemate positions
   - Test edge cases where stalemate might be confused with checkmate
   - Verify stalemate detection works for both white and black

### Phase 4: Game State Transition Tests

1. **Extend GameTests.cs**
   - Add tests for game state transitions
   - Test transitions between InPlay, Check, Checkmate, and Stalemate

2. **Test Implementation Details**
   - Create sequences of moves that transition between states
   - Verify correct state after each move
   - Test edge cases where state might be ambiguous

### Phase 5: Pawn Promotion Tests

1. **Extend PawnTests.cs**
   - Add more comprehensive tests for pawn promotion
   - Test promotion in various scenarios (check, causing check, etc.)

2. **Test Implementation Details**
   - Set up positions where pawns are about to promote
   - Test promotion resulting in check or checkmate
   - Test promotion when already in check

### Phase 6: Edge Case Tests for Piece Movement

1. **Extend Piece-Specific Test Classes**
   - Add edge case tests to each piece's test class
   - Focus on movement restrictions and special cases

2. **Test Implementation Details**
   - Test piece movement when blocked by other pieces
   - Test knight's ability to jump over pieces
   - Test king's inability to move to attacked squares

## Test Class Structure

Each test class should follow this structure:

```csharp
namespace DChess.Test.Unit.Rules
{
    public class [FeatureName]Tests : GameTestBase
    {
        // Setup code if needed beyond what GameTestBase provides
        
        [Fact(DisplayName = "Descriptive test name")]
        public void test_method_name()
        {
            // Arrange
            // Set up the board using FEN notation or direct piece placement
            
            // Act
            // Perform the action being tested
            
            // Assert
            // Verify the expected outcome
        }
        
        [Theory(DisplayName = "Descriptive theory test name")]
        [InlineData("fen_string_1", expected_result_1)]
        [InlineData("fen_string_2", expected_result_2)]
        public void theory_test_method_name(string fenString, expected_type expectedResult)
        {
            // Arrange
            Sut.Set(fenString);
            
            // Act
            var result = // Action being tested
            
            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
```

## Common Test Patterns

1. **FEN-Based Position Testing**:
   ```csharp
   Sut.Set("fen_string_here");
   var result = // Test action
   result.Should().Be(expectedResult);
   ```

2. **Direct Piece Placement**:
   ```csharp
   Sut.Board.Place(WhiteKing, e1);
   Sut.Board.Place(BlackKing, e8);
   Sut.Board.Place(WhiteRook, a1);
   // Test action and assertions
   ```

3. **Move Sequence Testing**:
   ```csharp
   Sut.Board.Place(WhiteKing, e1);
   Sut.Board.Place(BlackKing, e8);
   Sut.Board.Place(WhiteRook, a1);
   
   Sut.Move(a1, a8); // Make a move
   Sut.Status(Black).Should().Be(Check); // Assert state after move
   ```

## Test Data

### Common FEN Strings for Testing

```
// Starting position
"rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1"

// Check positions
"k7/8/1R6/8/8/8/8/K7 w - - 0 1"  // Rook gives check
"k7/8/2B5/8/8/8/8/K7 w - - 0 1"  // Bishop gives check
"k7/8/2N5/8/8/8/8/K7 w - - 0 1"  // Knight gives check
"k7/1P6/8/8/8/8/8/K7 w - - 0 1"  // Pawn gives check

// Checkmate positions
"k7/1R6/1R6/8/8/8/8/K7 w - - 0 1"  // Double rook checkmate
"k7/1Q6/8/8/8/8/8/K7 w - - 0 1"    // Queen checkmate

// Stalemate positions
"k7/8/1Q6/8/8/8/8/K7 b - - 0 1"    // Classic queen stalemate
```

## Implementation Timeline

1. **Week 1**: Implement Phase 1 and 2 (Check/Checkmate Detection and Illegal Moves)
2. **Week 2**: Implement Phase 3 and 4 (Stalemate Detection and Game State Transitions)
3. **Week 3**: Implement Phase 5 and 6 (Pawn Promotion and Edge Cases)

## Success Criteria

- All tests pass consistently
- Test coverage increases for existing features
- Edge cases are properly tested
- Tests are well-documented and maintainable
- Tests follow the project's coding standards
