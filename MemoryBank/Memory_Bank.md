# DChess Memory Bank

This document serves as a quick reference guide for the DChess project, summarizing key information and rules.

## Project Overview

DChess is a chess engine designed to be:
- A playable chess game
- Modular and extensible
- A demonstration of TDD (Test-Driven Development)
- Lighthearted and fun

## Chess Rules Implemented

### Piece Movement Rules

#### Pawn
- ✅ Moves forward only (never backward)
- ✅ Can move two squares forward on first move, one square otherwise
- ✅ Can capture diagonally
- ✅ Can be promoted when reaching the opposite end of the board

#### Knight
- ✅ Moves in an L-shape (2 squares in one direction, then 1 square perpendicular)
- ✅ Can jump over other pieces

#### Rook
- ✅ Moves horizontally or vertically any number of squares
- ✅ Cannot jump over other pieces

#### Bishop
- ✅ Moves diagonally any number of squares
- ✅ Cannot jump over other pieces

#### Queen
- ✅ Moves horizontally, vertically, or diagonally any number of squares
- ✅ Cannot jump over other pieces

#### King
- ✅ Moves one square in any direction

### Game Rules

#### Check and Checkmate
- ✅ Check: King is under threat
- ✅ Checkmate: King is in check and no legal moves can remove the threat
- ✅ Cannot make moves that would put your own king in check

#### Special Rules (Not Yet Implemented)
- ❌ Castling: The king and rook haven't moved
- ❌ Castling: The squares between king and rook must be unoccupied
- ❌ En-passant capturing

#### Game Status Tracking
- ✅ System to track game status (ongoing, check, checkmate, stalemate)
- ❌ State tree structure for move history/exploration
- ❌ Turn system implementation

## Technical Implementation

### Core Architecture
- Board representation using a collection of pieces
- Flyweight pattern for chess pieces to reduce memory usage
- Move validation system with general and piece-specific rules

### Testing Framework
- ✅ FluentAssertions extensions for chess-specific assertions
- ✅ Visualization capabilities for test failures
- ✅ MoveHandlerTestBase for testing move calculations
- ✅ VisualizationTestBase for visual debugging
- ✅ Example tests for best move calculations with visualization

### Design Patterns Used
- ✅ Flyweight Pattern: Used for chess pieces to reduce memory allocation
- ✅ Memoization: Used for caching expensive calculations
- ✅ Factory Pattern: Used in ChessPieceFactory
- ✅ Extension Methods: Used for FluentAssertions to enhance testing

### API Design
- REST API with endpoints for game creation and moves
- Azure Function implementation
- Stateless design (game state passed in requests)

## Pending Improvements

### High Priority
1. **Modern C# Features**
   - Nullable reference types consistency
   - Pattern matching in switch expressions
   - File-scoped namespaces

2. **Code Quality**
   - Fix uninitialized fields
   - Complete TODOs
   - Fix static/instance method inconsistencies

3. **Algorithm Improvements**
   - Iterative deepening for better time management
   - Move ordering for alpha-beta pruning
   - Transposition tables

### Medium Priority
1. **Performance Optimizations**
   - Bitboard representation
   - Targeted move generation
   - Enhanced position evaluation

2. **Error Handling**
   - Input validation
   - Error recovery mechanisms
   - Structured logging

### Lower Priority
1. **Documentation**
2. **UI and User Experience**
3. **Additional Chess Features**

## Chess Rules Quick Reference

### Basic Movement
- **Pawns**: Forward 1 square (or 2 on first move), capture diagonally
- **Knights**: L-shape (2+1), can jump over pieces
- **Bishops**: Diagonal only, any distance
- **Rooks**: Horizontal/vertical only, any distance
- **Queens**: Any direction, any distance
- **Kings**: Any direction, 1 square only

### Special Rules
- **Check**: King is threatened
- **Checkmate**: King is in check with no legal moves
- **Castling**: King moves 2 squares toward rook, rook jumps over
- **En Passant**: Pawn can capture an opponent's pawn that has just moved two squares
- **Promotion**: Pawn reaching the opposite end can be upgraded

### Game End Conditions
- **Checkmate**: Win for the player who delivered checkmate
- **Stalemate**: Draw when a player has no legal moves but is not in check
- **Insufficient Material**: Draw when neither player has enough pieces to checkmate
- **50-Move Rule**: Draw after 50 moves without a pawn move or capture
- **Threefold Repetition**: Draw when the same position occurs three times
