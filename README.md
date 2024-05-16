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
## What it is
DCHESS is a coding challenge that I'm setting for myself.
My initial goals are for it to be:
- a playable chess game
- console based played by invoking `dc [move]` with `move` in algebraic notation
- a demonstration of TDD
- the game should retain state allowing the game to be played over multiple sessions
- modular and extensible
- fun

## Development Checklist for DCHESS

- [x] **Display a Chess Board with Pieces**
    - [x] Create a `Board` class that holds all pieces and their positions.
    - [x] Display the board in the console or graphical UI.
    - [x] Populate the board with initial positions for all pieces.

- [ ] **Allow a piece to be moved**
    - [ ] Create a function to update the board by coordinate.

- [ ] **Implement the Rules of Chess, Only Allowing Legal Moves**
    - [ ] **General Movement Rules**
        - [ ] Disallow taking your own pieces.
        - [ ] Disallow moves that would put the player in check.
        - [ ] Disallow moves that leave the player in checkmate.
        - [ ] Enforce that only moves to a square occupied by an opposing piece are captures.
    - [ ] **Disallow Invalid Moves**
        - [ ] **Pawn**
            - [ ] Ensure pawns cannot move backwards.
            - [ ] Ensure pawns can only move forward two squares on their first move and one square otherwise.
            - [ ] Ensure pawns can move diagonally only when capturing an opponent’s piece.
            - [ ] Enforce that pawns can be promoted upon reaching the opposite end of the board.
        - [ ] **Knight**
            - [ ] Ensure knights move in an L-shape.
            - [ ] Ensure knights can jump over other pieces.
        - [ ] **Bishop**
            - [ ] Ensure bishops move diagonally any number of squares.
            - [ ] Ensure bishops cannot jump over other pieces.
        - [ ] **Rook**
            - [ ] Ensure rooks move horizontally or vertically any number of squares.
            - [ ] Ensure rooks cannot jump over other pieces.
        - [ ] **Queen**
            - [ ] Ensure queens move horizontally, vertically, or diagonally any number of squares.
            - [ ] Ensure queens cannot jump over other pieces.
        - [ ] **King**
            - [ ] Ensure kings move one square in any direction.
            - [ ] Ensure kings cannot move into check.
            - [ ] Implement castling rules, ensuring the king and rook haven't moved and the squares between them are unoccupied.
            - [ ] Implement rules for check (king is under threat).
            - [ ] Implement rules for checkmate (king is in check and no legal moves can remove the threat).
    - [ ] **Special Moves**
        - [ ] Implement en passant capturing rules.
        - [ ] Ensure special moves adhere to the context-specific conditions (like the positions of pieces for castling and en passant).
- [ ] **Game Status**
    - [ ] Implement a system to keep track of the game status (e.g., ongoing, check, checkmate, stalemate). Display the current game status each time the board is shown.

- [ ] **Allow the User to Move Pieces Using Algebraic Notation**
    - [ ] Parse algebraic notation into board coordinates.
    - [ ] Create a function to update the board based on the parsed move.
    - [ ] Develop input validation to ensure notation is syntactically correct.
 
- [ ] **Turn System**
    - [ ] Implement a turn system to ensure that players alternate turns correctly. Include turn validation in the move processing logic to prevent a player from making consecutive moves.

- [ ] **Game History**
    - [ ] Implement a way to record the history of moves made during the game. Store each move in a list or file.
    - [ ] Provide a command to display the move history (`dc --history`).
    - [ ] Consider implementing functionality for undoing and redoing moves based on the game history.

- [ ] **Error Handling**
    - [ ] Plan for robust error handling, especially for user input. This includes providing useful error messages when illegal moves are attempted.
    - [ ] Ensure that the error messages are clear and helpful, guiding the user on how to correct their input or explaining why a move cannot be made.

