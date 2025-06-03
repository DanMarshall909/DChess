# DChess Rules Reference

This document provides a comprehensive reference for chess rules as implemented in the DChess project.

## Basic Chess Rules

### Board Setup
- The chess board is an 8×8 grid with alternating light and dark squares
- Each player starts with 16 pieces: 8 pawns, 2 knights, 2 bishops, 2 rooks, 1 queen, and 1 king
- The pieces are arranged in a standard formation with pawns in the second rank and other pieces in the first rank

### Turn Order
- White moves first, then players alternate turns
- A player must move on their turn
- Only one piece can be moved per turn (except for castling, which involves two pieces)

### Movement Rules

#### Pawn
- **Basic Movement**: Forward one square
- **First Move**: Option to move forward two squares on first move
- **Capture**: Diagonally forward one square
- **Promotion**: When a pawn reaches the opposite end of the board, it must be promoted to a queen, rook, bishop, or knight
- **En Passant**: A pawn can capture an opponent's pawn that has just moved two squares forward as if it had only moved one square (not yet implemented in DChess)

#### Knight
- Moves in an L-shape: two squares in one direction, then one square perpendicular
- Only piece that can jump over other pieces

#### Bishop
- Moves diagonally any number of squares
- Cannot jump over other pieces
- Each bishop is confined to squares of one color

#### Rook
- Moves horizontally or vertically any number of squares
- Cannot jump over other pieces

#### Queen
- Combines the power of the rook and bishop
- Moves horizontally, vertically, or diagonally any number of squares
- Cannot jump over other pieces

#### King
- Moves one square in any direction
- Cannot move into check
- **Castling**: King moves two squares towards a rook, and the rook moves to the square the king crossed (not yet implemented in DChess)

### Special Rules

#### Check
- A king is in check when it is under attack by an opponent's piece
- When in check, a player must:
  1. Move the king out of check
  2. Block the check with another piece
  3. Capture the piece causing the check

#### Checkmate
- Occurs when a king is in check and there is no legal move to escape
- The player whose king is checkmated loses the game

#### Stalemate
- Occurs when a player has no legal moves but their king is not in check
- Results in a draw

#### Castling (Not Yet Implemented in DChess)
Requirements:
- Neither the king nor the rook has moved previously
- There are no pieces between the king and the rook
- The king is not in check
- The king does not pass through or land on a square that is attacked by an enemy piece

#### En Passant (Not Yet Implemented in DChess)
- When a pawn moves two squares forward from its starting position and lands beside an opponent's pawn, the opponent's pawn can capture it as if it had only moved one square

#### Pawn Promotion
- When a pawn reaches the opposite end of the board, it must be promoted to a queen, rook, bishop, or knight
- In DChess, pawns are automatically promoted to queens

### Game End Conditions

#### Win Conditions
- **Checkmate**: A player wins by checkmating the opponent's king

#### Draw Conditions
- **Stalemate**: When a player has no legal moves but is not in check
- **Insufficient Material**: When neither player has enough pieces to checkmate (not yet implemented in DChess)
- **50-Move Rule**: After 50 consecutive moves without a pawn move or capture (not yet implemented in DChess)
- **Threefold Repetition**: When the same position occurs three times (not yet implemented in DChess)
- **Agreement**: Players can agree to a draw (not yet implemented in DChess)

## DChess Implementation Details

### Move Validation Process
1. **General Validation**:
   - Checks if the move is within the board boundaries
   - Ensures the player is moving their own piece
   - Verifies the destination square is not occupied by a friendly piece
   - Confirms the move doesn't put or leave the player's king in check

2. **Piece-Specific Validation**:
   - Each piece type has its own validation logic
   - Checks if the move follows the piece's movement pattern
   - For most pieces, verifies there are no pieces blocking the path

3. **Special Move Validation**:
   - Pawn promotion is handled automatically
   - Castling and en passant are not yet implemented

### Check Detection
- After each move, the system checks if either king is in check
- This is done by examining if any opponent's piece can legally move to the king's square

### Checkmate Detection
- When a king is in check, the system generates all possible moves for the player
- If no legal moves exist that would remove the check, checkmate is declared

### Move Generation and Evaluation
- The AI uses a minimax algorithm with alpha-beta pruning
- Position evaluation considers material value and game state
- Future improvements will include more sophisticated evaluation criteria

## Chess Notation

### Algebraic Notation
- Files (columns) are labeled a-h from left to right
- Ranks (rows) are labeled 1-8 from bottom to top
- Each square has a unique identifier, e.g., e4, a1, h8
- Moves are written as the piece letter (except for pawns) followed by the destination square
  - Example: Nf3 means "knight to f3"
  - For pawn moves, only the destination square is given, e.g., e4
  - Captures are indicated with "x", e.g., Nxe5
  - Castling is written as O-O (kingside) or O-O-O (queenside)

### DChess Notation
- DChess currently uses coordinate notation for moves
- A move is represented as the starting square followed by the ending square
- Example: "e2e4" means "move the piece at e2 to e4"
