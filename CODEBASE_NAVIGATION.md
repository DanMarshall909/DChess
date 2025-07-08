# DChess Codebase Navigation Guide

This document provides comprehensive summaries to help navigate the DChess codebase efficiently. It breaks down the architecture, key components, and their relationships to help you quickly understand and work with the code.

## Quick Reference

### Core Components Map
- **Game Logic**: `src/DChess.Core/Game/` - Board, Game state, piece management
- **Move System**: `src/DChess.Core/Moves/` - Move validation, AI, path finding
- **Pieces**: `src/DChess.Core/Flyweights/` - Individual piece implementations
- **Rendering**: `src/DChess.Core/Renderers/` - Board visualization interfaces
- **UI Applications**: `src/DChess.UI.Console/`, `src/DChess.UI.WPF/` - User interfaces
- **API**: `DChess.Function/` - Azure Functions web API
- **Tests**: `test/DChess.Test.Unit/` - Comprehensive test suite

### Key Entry Points
- **Game Creation**: `new Game(board, errorHandler, maxDepth)`
- **Move Execution**: `game.Make(move)` or `moveHandler.Make(move, game)`
- **Board Rendering**: `board.RenderToText()` or `board.Visualize()`
- **AI Move**: `moveHandler.GetBestMove(game, colour, depth)`

## 1. Core Chess Engine Components

### Game Class (`src/DChess.Core/Game/Game.cs`)
**Main Hub for Chess Game State**

- **Purpose**: Orchestrates all chess game operations and maintains game state
- **Key Responsibilities**:
  - Game state management (current player, move history, game status)
  - Move execution coordination through MoveHandler
  - Check/checkmate/stalemate detection
  - Player and turn management
  - Game state cloning for AI simulation

**Essential Methods**:
- `Make(Move move)` - Execute a chess move
- `IsInCheck(Colour colour)` - Check if player is in check
- `Status(Colour colour)` - Get game status (Check, Checkmate, Stalemate)
- `FriendlyPieces(Colour colour)` - Get all pieces for a color
- `AsClone()` - Create deep copy for AI lookahead

**Integration Points**:
- Uses `Board` for piece storage and positioning
- Delegates to `MoveHandler` for move execution and AI
- Interacts with `ChessPiece` hierarchy for move validation

### Board Class (`src/DChess.Core/Game/Board.cs`)
**Efficient Chess Board Representation**

- **Purpose**: Stores and manages chess piece positions using optimized data structures
- **Data Structure**: Flat array of 64 `PieceAttributes` structs for O(1) access
- **Key Features**:
  - Multiple indexing methods (Square, file/rank, zero-based)
  - Efficient piece search and lookup
  - Memory-optimized with readonly record struct design
  - Thread-safe immutable operations

**Essential Methods**:
- `this[Square square]` - Get/set piece at square
- `HasPieceAt(Square square)` - Check square occupancy
- `Find(predicate)` - Search for pieces matching criteria
- `KingSquare(Colour colour)` - Locate king position

**Performance Features**:
- Zero-allocation design for frequent operations
- Bit manipulation for efficient file/rank calculations
- Compact memory footprint with struct-based storage

### Chess Piece Hierarchy (`src/DChess.Core/Flyweights/`)
**Flyweight Pattern Implementation**

- **Purpose**: Implements piece-specific chess rules while minimizing memory usage
- **Design Pattern**: Flyweight pattern with shared piece instances
- **Architecture**: Abstract base class with concrete implementations

**Base Class - ChessPiece**:
- `CheckMove(Square to, Game game)` - Complete move validation
- `ValidatePath(Square to, Game game)` - Piece-specific validation (abstract)
- `MoveValidities(Game game)` - All valid moves for piece

**Piece Implementations**:
- **Pawn**: Complex forward movement, diagonal capture, promotion logic
- **Rook**: Horizontal/vertical movement with path blocking
- **Knight**: L-shaped movement, implements `IIgnorePathCheck`
- **Bishop**: Diagonal movement only
- **Queen**: Combined rook and bishop movement
- **King**: Single-square movement in any direction

**Factory System**:
- `ChessPieceFactory` - Thread-safe piece pooling with `ConcurrentDictionary`
- `PieceContext` - Combines piece attributes with position
- `PieceAttributes` - Compact piece type and color storage

## 2. Move Validation and Handling System

### Move Representation (`src/DChess.Core/Moves/Move.cs`)
**Immutable Move Data Structure**

- **Purpose**: Represents chess moves with computed properties for validation
- **Key Properties**:
  - `From`, `To` - Source and destination squares
  - `IsDiagonal`, `IsVertical`, `IsHorizontal` - Move direction analysis
  - `Distance` - Euclidean distance calculation
  - `SquaresAlongPath` - Intermediate squares (memoized)

**Constructor Options**:
- `Move(Square from, Square to)` - Direct square construction
- `Move(string move)` - Algebraic notation parsing (e.g., "e2e4")

### Move Validation Flow
**Multi-Layer Validation System**

**Layer 1: General Validation**
- Turn validation (current player owns piece)
- Destination validation (not same square, not own piece)
- Path blocking detection (except pieces with `IIgnorePathCheck`)
- Check prevention (simulates move to ensure king safety)

**Layer 2: Piece-Specific Validation**
- Each piece type implements `ValidatePath()` method
- Validates movement patterns specific to piece type
- Returns `MoveResult` with detailed validation information

### MoveHandler Class (`src/DChess.Core/Moves/MoveHandler.cs`)
**Central Move Processing and AI**

- **Purpose**: Orchestrates move execution and implements chess AI
- **Key Responsibilities**:
  - Move execution with automatic pawn promotion
  - Legal move generation for any color
  - AI decision-making using minimax with alpha-beta pruning
  - Game state evaluation and scoring

**Essential Methods**:
- `Make(Move move, Game game)` - Execute validated move
- `LegalMoves(Colour colour, Game game)` - Generate all valid moves
- `GetBestMove(Game game, Colour colour, int depth)` - AI move selection
- `HasLegalMoves(Game game, Colour colour)` - Stalemate detection

**AI Implementation**:
- Minimax algorithm with alpha-beta pruning
- Configurable search depth (default 3 levels)
- Position evaluation combining material and positional factors
- Checkmate scoring (1,000,000 points)
- Standard material values (Pawn=1, Knight/Bishop=3, Rook=5, Queen=9)

### Error Handling System
**Comprehensive Validation and Error Management**

- **MoveValidity Enum**: 24 specific validation failure reasons
- **MoveResult Record**: Combines move with validation result
- **IErrorHandler Interface**: Pluggable error handling strategies
- **Custom Exceptions**: Chess-specific exception hierarchy

## 3. Test Structure and Patterns

### Test Organization
**Comprehensive Test Suite with Visualization**

- **Base Classes**: `GameTestBase`, `VisualizationTestBase`, `MoveHandlerTestBase`
- **Test Categories**: Unit tests, integration tests, AI behavior tests
- **Visualization**: Automatic board display on test failures
- **Coverage**: Piece movement, game rules, AI decisions, edge cases

### Testing Patterns
**Advanced Testing Methodologies**

**Matrix-Based Movement Testing**:
- 15x15 matrices define valid moves visually
- Comprehensive edge case coverage
- Clear visual representation of movement patterns

**Visualization-Integrated Testing**:
- `VisualizeBoardAndWait()` - Interactive debugging
- `VisualizeBestMove()` - AI decision visualization
- `AssertBoardState()` - Assertion failure debugging

**FluentAssertions Integration**:
- Custom extensions for chess-specific assertions
- Readable test code with meaningful error messages
- Board state assertions with automatic visualization

### Test Utilities
**Specialized Testing Infrastructure**

- **TestErrorHandler**: Captures errors without throwing exceptions
- **MovementTestingExtensions**: Piece movement validation utilities
- **BoardAssertions**: Chess board specific assertions
- **Example Tests**: Demonstration of visualization capabilities

## 4. UI Components and Rendering

### Rendering Architecture
**Multi-Context Rendering System**

- **IBoardRenderer Interface**: Abstraction for different rendering targets
- **TextRenderer**: Console-based Unicode character rendering
- **WpfBoardRenderer**: Visual WPF-based rendering
- **Extension Methods**: `RenderToText()`, `Visualize()` for easy use

### Console UI (`src/DChess.UI.Console/`)
**Text-Based Chess Interface**

- **Current State**: Placeholder implementation
- **TextRenderer Features**:
  - Unicode block characters for squares
  - Standard chess piece symbols
  - File/rank labels for navigation
  - Suitable for debugging and text output

### WPF UI (`src/DChess.UI.WPF/`)
**Rich Visual Chess Interface**

- **ChessBoardControl**: 8x8 grid with proper chess visualization
- **Features**:
  - Unicode chess piece symbols (♔♕♖♗♘♙)
  - Alternating square colors (Wheat/SaddleBrown)
  - Highlight system for move indication
  - Thread-safe rendering with UI dispatcher

**WPF Architecture**:
- 500x500 pixel window with 50x50 pixel squares
- Dynamic square creation and piece placement
- Color-coded pieces with Segoe UI Symbol font
- Extensible for future user interaction

### Azure Functions API (`DChess.Function/`)
**Web-Based Chess API**

- **Single Endpoint**: `/api/game` with GET requests
- **Parameters**: colour, current-player, move via query string
- **Processing Flow**:
  1. Create standard board layout
  2. Process player move
  3. Calculate best AI move (depth 5)
  4. Return JSON with board state

**API Integration**:
- Uses `ExceptionErrorHandler` for error management
- Returns text representation via `RenderToText()`
- Integrates with core engine through `Game` class

### Rendering Integration
**UI-Engine Integration Patterns**

- **Direct Board Access**: UI components read from `Board` via indexer
- **Extension Methods**: `BoardExtensions` provides rendering utilities
- **Data Flow**: Core engine → Board state → UI rendering
- **Testing Integration**: Automatic visualization on assertion failures

## 5. Key Design Patterns

### Flyweight Pattern
- **Implementation**: Chess pieces with shared instances
- **Benefit**: Reduced memory usage through object pooling
- **Components**: `ChessPiece` hierarchy, `ChessPieceFactory`

### Strategy Pattern
- **Implementation**: Piece-specific move validation methods
- **Benefit**: Encapsulated piece behavior, easy extensibility
- **Components**: `ValidatePath()` methods, `IIgnorePathCheck` interface

### Template Method Pattern
- **Implementation**: `ChessPiece.CheckMove()` validation flow
- **Benefit**: Consistent validation process with customizable steps
- **Components**: General validation + piece-specific validation

### Factory Pattern
- **Implementation**: `ChessPieceFactory` for piece creation
- **Benefit**: Centralized creation with pooling optimization
- **Components**: Thread-safe piece pooling with `ConcurrentDictionary`

## 6. Performance Optimizations

### Memory Efficiency
- **Flyweight Pattern**: Shared piece instances
- **Struct-Based Storage**: `PieceAttributes`, `Square` as value types
- **Zero-Allocation Design**: Minimal garbage collection pressure

### Computation Efficiency
- **Memoization**: Cached path calculations and distances
- **Alpha-Beta Pruning**: Significant AI search space reduction
- **O(1) Board Access**: Flat array with computed indices
- **Lazy Evaluation**: Deferred calculations until needed

### Threading Considerations
- **Thread-Safe Factories**: `ConcurrentDictionary` for piece pooling
- **Immutable Data Structures**: Safe sharing across threads
- **UI Thread Handling**: Proper dispatcher usage in WPF

## 7. Future Extension Points

### Planned Features
- **Castling Rules**: Special king-rook movement
- **En Passant**: Pawn capture variation
- **Advanced AI**: Transposition tables, move ordering
- **User Interaction**: Click-to-move interface

### Architecture Readiness
- **Pluggable Error Handling**: Easy to add new error strategies
- **Extensible Rendering**: New UI contexts via `IBoardRenderer`
- **Modular AI**: Configurable search algorithms and evaluation functions
- **Test Infrastructure**: Comprehensive testing support for new features

## 8. Common Development Workflows

### Adding New Piece Types
1. Create new class inheriting from `ChessPiece`
2. Implement `ValidatePath()` method
3. Add to `ChessPieceFactory` creation logic
4. Create comprehensive tests using matrix-based patterns

### Implementing New UI
1. Implement `IBoardRenderer` interface
2. Create rendering logic for your target platform
3. Add extension methods for easy integration
4. Include in test visualization system

### Extending AI Capabilities
1. Modify `MoveHandler` evaluation function
2. Add new scoring factors to position evaluation
3. Implement new search optimizations
4. Add performance benchmarks and tests

This navigation guide provides the foundation for understanding and working with the DChess codebase. Each component is designed with clear responsibilities and extensible architecture, making it straightforward to add new features or modify existing behavior.