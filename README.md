# DCHESS

```
D♘C♗H♕E♗S♘S♖
♜D♞C♝H♛E♝S♞S 
S♖D♘C♗H♕E♗S♘ 
♞S♜D♞C♝H♛E♝S
S♘S♖D♘C♗H♕E♗ 
♝S♞S♜D♞C♝H♛E 
E♗S♘S♖D♘C♗H♕ 
♛E♝S♞S♜D♞C♝H
```

Making chess nerdier

# What it is

DCHESS is a coding challenge that I'm setting for myself.
My initial goals are for it to be:

- a playable chess game
- console based played by invoking `dc [move]` with `move` in algebraic notation
- a demonstration of TDD
- the game should retain state allowing the game to be played over multiple sessions
- modular and extensible
- fun

# Development Checklist for DCHESS

## Display a Chess Board with Pieces

- [x] Create a `Board` class that holds all pieces and their positions.
- [x] Display the board in the console or graphical UI.
- [x] Populate the board with initial positions for all pieces.

## Move text rendering into a separate class
- [X] Provide interface for rendering the board
  -  [X] Takes in a board and returns a string representation of the board
- [X] Adjust the display logic to use the new class

## Refactor the board to be a collection of pieces rather than a 2D array of cells

- [ ] Create a `Piece` base abstract class that holds the piece type and color (eventually it will contain the rules and other things specific to that piece).
- [ ] Refactor the `Board` class to store pieces and their positions.
- [ ] Update the display logic to show pieces on the board.

## Allow a piece to be moved

- [ ] Add a method on the piece base class to move by coordinate.
- [ ] Add a method to check if the move is generally valid.
- [ ] Disallow taking your own pieces.
- [X] ~~Disallow moving off the board. Already handled by Coordinate validation~~
## Add a virtual method to check if a move for the particular piece type is valid.  

### Pawn

- [ ] Pawns cannot move backwards.
- [ ] Pawns can only move forward two squares on their first move and one square otherwise.

### Knight

- [ ] Knights move in an L-shape.
- [ ] Knights can jump over other pieces.

### Bishop

- [ ] Bishops move diagonally any number of squares.
- [ ] Bishops cannot jump over other pieces.

### Rook

- [ ] Rooks move horizontally or vertically any number of squares.
- [ ] Rooks cannot jump over other pieces.

### Queen

- [ ] Queens move horizontally, vertically, or diagonally any number of squares.
- [ ] Queens cannot jump over other pieces.

### King

- [ ] Kings move one square in any direction.

## Movement Rules phase 2 (requiring game rules to be implemented)

### Capturing
- [ ] Ensure pawns can move diagonally only when capturing an opponent’s piece.
- [ ] Enforce that pawns can be promoted upon reaching the opposite end of the board.

### Check
- [ ] Implement rules for check (king is under threat).
- [ ] Implement rules for checkmate (king is in check and no legal moves can remove the threat).
- [ ] Ensure kings cannot move into check.
- [ ] Disallow moves that would put the player in check.
 
- [ ] Enforce that only moves to a square occupied by an opposing piece are captures.
- [ ] Implement castling rules
  - The king and rook haven't moved
  - The squares between them are unoccupied.
- [ ] Implement en passant capturing rules.
- [ ] Ensure special moves adhere to the context-specific conditions (like the positions of pieces for castling and en
  passant).

## Game Status

- [ ] Implement a system to keep track of the game status (e.g., ongoing, check, checkmate, stalemate). Display the
  current game status each time the board is shown.

## Allow the User to Move Pieces Using Algebraic Notation

- [ ] Parse algebraic notation into board coordinates.
- [ ] Create a function to update the board based on the parsed move.
- [ ] Develop input validation to ensure notation is syntactically correct.

## Turn System

- [ ] Implement a turn system to ensure that players alternate turns correctly. Include turn validation in the move
  processing logic to prevent a player from making consecutive moves.

## Game History

- [ ] Implement a way to record the history of moves made during the game. Store each move in a list or file.
- [ ] Provide a command to display the move history (`dc --history`).
- [ ] Consider implementing functionality for undoing and redoing moves based on the game history.

## Error Handling

- [ ] Plan for robust error handling, especially for user input. This includes providing useful error messages when
  illegal moves are attempted.
- [ ] Ensure that the error messages are clear and helpful, guiding the user on how to correct their input or explaining
  why a move cannot be made.
