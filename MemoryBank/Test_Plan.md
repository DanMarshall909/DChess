# DChess Test Plan

This document outlines the test plan for the DChess project, focusing on unit tests for existing features and identifying areas that need additional test coverage.

## Current Test Coverage

The DChess project currently has tests covering:

- Basic piece movement rules for all piece types
- Simple check and checkmate detection
- Basic game state tracking
- Move validation
- Pawn promotion

## Planned Unit Tests for Existing Features

### 1. Check and Checkmate Detection

```csharp
// Test cases for check detection
[Theory(DisplayName = "Check is correctly detected in various positions")]
[InlineData("k7/8/1R6/8/8/8/8/K7 w - - 0 1", true)]  // Rook puts king in check
[InlineData("k7/8/2B5/8/8/8/8/K7 w - - 0 1", true)]  // Bishop puts king in check
[InlineData("k7/8/2N5/8/8/8/8/K7 w - - 0 1", true)]  // Knight puts king in check
[InlineData("k7/8/1P6/8/8/8/8/K7 w - - 0 1", false)] // Pawn doesn't put king in check (not diagonal)
[InlineData("k7/1P6/8/8/8/8/8/K7 w - - 0 1", true)]  // Pawn puts king in check diagonally
public void check_is_correctly_detected(string fenString, bool expectedInCheck)

// Test cases for checkmate detection
[Theory(DisplayName = "Checkmate is correctly detected in various positions")]
[InlineData("k7/1R6/1R6/8/8/8/8/K7 w - - 0 1", true)]  // Double rook checkmate
[InlineData("k7/1Q6/8/8/8/8/8/K7 w - - 0 1", true)]    // Queen checkmate
[InlineData("k7/1R6/8/8/8/8/8/K7 w - - 0 1", false)]   // Check but not checkmate
public void checkmate_is_correctly_detected(string fenString, bool expectedCheckmate)
```

### 2. Stalemate Detection

```csharp
[Theory(DisplayName = "Stalemate is correctly detected")]
[InlineData("k7/8/1Q6/8/8/8/8/K7 b - - 0 1", true)]  // King has no legal moves but is not in check
[InlineData("k7/8/1R6/8/8/8/8/K7 b - - 0 1", false)] // King is in check, not stalemate
[InlineData("k7/p7/8/8/8/8/8/K7 b - - 0 1", false)]  // King has legal moves, not stalemate
public void stalemate_is_correctly_detected(string fenString, bool expectedStalemate)
```

### 3. Pawn Promotion

```csharp
[Fact(DisplayName = "Pawn is automatically promoted to queen when reaching the opposite end")]
public void pawn_is_promoted_to_queen_when_reaching_opposite_end()

[Fact(DisplayName = "Pawn promotion is correctly handled in check situations")]
public void pawn_promotion_is_correctly_handled_in_check_situations()

[Fact(DisplayName = "Pawn promotion is correctly handled when it results in check")]
public void pawn_promotion_results_in_check()

[Fact(DisplayName = "Pawn promotion is correctly handled when it results in checkmate")]
public void pawn_promotion_results_in_checkmate()
```

### 4. Preventing Illegal Moves

```csharp
[Fact(DisplayName = "Move that would put own king in check is invalid")]
public void move_that_would_put_own_king_in_check_is_invalid()

[Fact(DisplayName = "Pinned piece cannot move if it would expose king to check")]
public void pinned_piece_cannot_move_if_it_would_expose_king_to_check()

[Fact(DisplayName = "When in check, only moves that remove check are valid")]
public void when_in_check_only_moves_that_remove_check_are_valid()
```

### 5. Edge Case Tests for Piece Movement

```csharp
[Fact(DisplayName = "Knight can jump over pieces")]
public void knight_can_jump_over_pieces()

[Fact(DisplayName = "Rook cannot move through pieces")]
public void rook_cannot_move_through_pieces()

[Fact(DisplayName = "Bishop cannot move through pieces")]
public void bishop_cannot_move_through_pieces()

[Fact(DisplayName = "Queen cannot move through pieces")]
public void queen_cannot_move_through_pieces()

[Fact(DisplayName = "King cannot move to a square that is under attack")]
public void king_cannot_move_to_a_square_that_is_under_attack()
```

### 6. Game State Transition

```csharp
[Fact(DisplayName = "Game state transitions from InPlay to Check when king is threatened")]
public void game_state_transitions_from_in_play_to_check()

[Fact(DisplayName = "Game state transitions from Check to InPlay when check is resolved")]
public void game_state_transitions_from_check_to_in_play()

[Fact(DisplayName = "Game state transitions from Check to Checkmate when no legal moves exist")]
public void game_state_transitions_from_check_to_checkmate()

[Fact(DisplayName = "Game state transitions to Stalemate when no legal moves exist but king is not in check")]
public void game_state_transitions_to_stalemate()
```

## Test Visualization Framework

The DChess project now includes a robust visualization framework for tests, which helps with debugging and understanding test failures.

### 1. FluentAssertions Extensions

```csharp
// Example of using FluentAssertions extensions with visualization
[Fact(DisplayName = "Game state reflects king in check")]
public void game_state_reflects_king_in_check()
{
    Sut.Set("k7/8/1P6/8/8/8/8/K7 w - - 0 1");
    Sut.Status(White).Should().Be(InPlay, "white should be in play", Sut, "Initial Position Visualization");

    Sut.Move(b6, b7);
    Sut.Status(Black).Should().Be(Check, "black king should be in check", Sut, "Check Position Visualization");
}
```

### 2. Best Move Visualization

```csharp
[Fact(DisplayName = "Example of visualizing best move calculation")]
public void example_of_visualizing_best_move_calculation()
{
    // Arrange - Set up a board position
    Sut.Set("r1bqkbnr/pppp1ppp/2n5/4p3/4P3/5N2/PPPP1PPP/RNBQKB1R w KQkq - 2 3");
    
    // Act - Calculate the best move
    var bestMove = MoveHandler.GetBestMove(Sut, White, maxDepth: 3);
    
    // Assert with automatic visualization on failure
    bestMove.Format().Should().Be("b1c3", "the best move should be Nc3", Sut, "Best Move Visualization");
}
```

### 3. Capture Sequence Visualization

```csharp
[Fact(DisplayName = "Example of visualizing a capture sequence")]
public void example_of_visualizing_a_capture_sequence()
{
    // Arrange - Set up a position with a capture sequence
    Sut.Set("rnbqkbnr/ppp2ppp/8/3pp3/4P3/5N2/PPPP1PPP/RNBQKB1R w KQkq - 0 3");
    
    // Check if the destination square has a piece (which will be captured)
    var destinationHasPiece = Sut.Board.HasPieceAt(new Square("d5"));
    destinationHasPiece.Should().BeTrue("there should be a piece to capture at d5", Sut, "Pre-Capture Visualization");
    
    // Perform the move
    Sut.Make(bestMove);
    
    // Verify the move was made correctly
    Sut.Board.HasPieceAt(new Square("e4")).Should().BeFalse("the e4 pawn should have moved", Sut, "Post-Capture Visualization");
    Sut.Board.HasPieceAt(new Square("d5")).Should().BeTrue("the pawn should now be at d5", Sut, "Capture Result Visualization");
}
```

## Implementation Strategy

For each test category:

1. **Create or Extend Test Classes**:
   - Add to existing test classes where appropriate
   - Create new test classes for specific rule categories if needed

2. **Use FEN Notation**:
   - Set up specific board positions using FEN notation
   - Ensure positions accurately represent the scenario being tested

3. **Leverage Existing Test Infrastructure**:
   - Use the `GameTestBase` class for common setup
   - Use `MoveHandlerTestBase` for move calculation tests
   - Use `VisualizationTestBase` for tests that need visual debugging
   - Utilize FluentAssertions extensions for better test readability and visualization

4. **Focus on Edge Cases**:
   - Test boundary conditions
   - Test interactions between different rules
   - Test unusual board configurations

5. **Ensure Clear Test Names and Documentation**:
   - Use descriptive test names
   - Add comments explaining the purpose of complex tests
   - Document expected behavior clearly
   - Include visualization titles that describe what's being visualized

## Test Implementation Priority

1. **Check and Checkmate Detection** - Fundamental to game rules
2. **Preventing Illegal Moves** - Critical for game integrity
3. **Stalemate Detection** - Important game end condition
4. **Game State Transitions** - Ensures correct game flow
5. **Pawn Promotion** - Special rule with multiple implications
6. **Edge Case Tests for Piece Movement** - Ensures movement rules are robust
7. **Best Move Calculations** - Verifies AI decision making
8. **Visualization Tests** - Ensures visual debugging tools work correctly

## Future Test Areas (Not Yet Implemented Features)

- Castling
- En passant
- Fifty-move rule
- Threefold repetition
- Algebraic notation parsing
