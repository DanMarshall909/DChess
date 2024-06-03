# ♜🄳♞🄲♝🄷♛🄴♝🅂♞🅂♜

Making chess nerdier

# What it is

DChess is a chess engine designed to be:
- a playable chess game
- modular and extensible
- a demonstration of TDD
- lighthearted and fun

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

- [X] Create a `Piece` base abstract class that holds the piece type and colour (eventually it will contain the rules
  and other things specific to that piece).
- [X] Refactor the `Board` class to store pieces and their positions.
- [X] Update the display logic to show pieces on the board.

## Refactor to make use flyweight pattern for pieces

- [X] Create an object pool for pieces that returns objects rather than structs, but still uses structs for the board.
- [X] Refactor the board to use the object pool.

## Allow a piece to be moved

- [X] Add a method on the piece base class to move by coordinate.
- [X] Add a method to check if the move is generally valid.
- [X] Add a virtual method to check if a move for the particular piece type is valid.
- [X] Disallow taking your own pieces.
- [X] ~~Disallow moving off the board. Already handled by Coordinate validation~~

## Specific Piece Movement Rules

### Pawn

- [X] Pawns can only move forward.
- [X] Pawns cannot move backwards.
- [X] Pawns can only move forward two squares on their first move and one square otherwise.
- [X] Pawns can take diagonally.

### Knight

- [X] Knights move in an L-shape.
- [X] Knights can ONLY move in an L-shape.
- [X] Knights can jump over other pieces.

### Rook

- [X] Rooks move horizontally or vertically any number of squares.
- [X] Rooks cannot jump over other pieces.

### Bishop

- [X] Bishops move diagonally any number of squares.
- [X] Bishops cannot jump over other pieces.

### Queen

- [X] Queens move horizontally, vertically, or diagonally any number of squares.
- [X] Queens cannot jump over other pieces.

### King

- [X] Kings move one square in any direction.

## Movement Rules phase 2 (requiring piece specific game rules to be implemented)

### Capturing

- [X] Ensure pawns can move diagonally only when capturing an opponent’s piece.
- [X] Enforce that pawns can be promoted upon reaching the opposite end of the board.

### Check

- [X] Implement rules for check (king is under threat).
- [X] Disallow moves that would put the player in check.
- [ ] Implement rules for checkmate (king is in check and no legal moves can remove the threat).

### Miscelaneous rules
- [ ] Castling: The king and rook haven't moved
- [ ] Castling: The squares between a king and rook must be unocupied.
- [ ] En-passant capturing rules.

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

## REST API
### Design
- The API will call the Core library and essentially just be a plugin.
- The game should be fully playable via API calls.
- The code will run on an Azure Function.
- No game state will be stored on the server, but the server will be able to compute the next move based on current game
  state passed in the request.
- Rate limiting will be implemented to prevent abuse.
- Powershell and bash scripts will be provided to allow the game to be played from the command line without using the
  API directly.
  The API will have the following endpoints:

### Endpoints

#### GET /

- This endpoint returns a welcome message along with helpful information on how to use the API.
- Example response:

```
Welcome to DChess API!
- To get started with a new game, use call the /new endpoint.
```

#### GET /new

Returns a new game state with the initial board setup a link to the game state.

#### GET /game?bitboard={`bitboards`}&name={`name`}&colour={`black`|`white`}&turn={`black`|`white`}&level={`level`}&render={`true`|`false`}&move={`move`}

This endpoint returns a board with a given game state.

- `bitboard`: A string representation of the bitboards in hexadecimal.
- `name`: The current player's name.
- `colour`: The current player's colour, either `black` or `white`.
- `turn`: The current turn.
- `level` (optional - defaults to 1): The difficulty level of the AI player.
- `render` (optional defaults to `true`): If `true`, the board will be rendered as a string. If `false`, the bitboards
  will be returned.
- `move` (optional): The move to be made by the player. If the move is valid, the board will be updated with the new
  move and the AI will make its move. `move` can be specified as a string in algebraic notation (
  see https://en.wikipedia.org/wiki/Algebraic_notation_(chess)) or as a pair of coordinates e.g. `e2e4`.

# Optimisation

- [ ] Use bitboards to represent the board state
- [X] Use the flyweight pattern to reduce memory usage
