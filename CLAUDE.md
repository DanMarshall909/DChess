# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## 🚨 CRITICAL DEVELOPMENT RULES - MUST FOLLOW

### **TDD-Only Development Workflow**
This project follows **STRICT Test-Driven Development (TDD)**. You MUST:

1. **Red-Green-Refactor Cycle**: Always write failing tests first, make them pass, then refactor
2. **GitHub Issues Only**: Only work on features/bugs that have corresponding GitHub issues
3. **No Ad-hoc Work**: If asked to implement something without a GitHub issue, politely decline and remind the user to create an issue first

### **Workflow Enforcement**
When asked to implement any feature or fix any bug:

1. ✅ **Check for GitHub Issue**: Verify there's a corresponding GitHub issue
2. ❌ **No Issue = No Work**: If no issue exists, respond with:
   > "I notice there's no GitHub issue for this request. This project follows strict TDD practices and issue-driven development. Please create a GitHub issue first describing the feature/bug, acceptance criteria, and priority. I can help you create the issue if needed."

3. ✅ **TDD Process**: If issue exists, follow:
   - Write failing test(s) that capture the requirements
   - Implement minimum code to make tests pass  
   - Refactor for quality while keeping tests green
   - Update issue as completed

### **Exception Handling**
The ONLY exceptions to the GitHub issue requirement are:
- **Documentation updates** to existing files
- **Refactoring** existing code without changing functionality
- **Bug fixes** discovered during testing (but create issue immediately)
- **Emergency production fixes** (but create issue for tracking)

### **TDD Rules Reference**
See `MemoryBank/` directory for detailed TDD guidelines and follow the Red-Green-Refactor-Cover-Commit cycle documented there.

## Project Overview

DChess is a C# chess engine built with .NET 8.0, designed to be modular, extensible, and a demonstration of test-driven development (TDD). The project consists of multiple components including a core chess engine, UI applications, and an Azure Functions API.

## Architecture

### Core Components

- **DChess.Core** - Main chess engine library containing:
  - `Game/` - Core game logic (Board, Game state, piece movement)
  - `Flyweights/` - Chess piece implementations using flyweight pattern
  - `Moves/` - Move validation and handling logic
  - `Renderers/` - Board rendering interfaces and implementations
  - `Errors/` - Custom exception types and error handling

- **DChess.UI.Console** - Console application for playing chess
- **DChess.UI.WPF** - WPF desktop application with visual chess board
- **DChess.Function** - Azure Functions API for web-based chess gameplay
- **DChess.Test.Unit** - Comprehensive unit test suite using xUnit

### Key Design Patterns

1. **Flyweight Pattern** - Used for chess pieces to reduce memory usage
2. **Strategy Pattern** - Different piece types implement movement validation
3. **Repository Pattern** - Board acts as a collection of pieces
4. **Command Pattern** - Moves are represented as discrete objects

## Common Development Commands

### Build and Test
```bash
# Build entire solution
dotnet build DChess.sln

# Build in Release configuration
dotnet build DChess.sln --configuration Release

# Run all unit tests
dotnet test DChess.sln

# Run tests with coverage
dotnet test DChess.sln --collect:"XPlat Code Coverage"

# Run specific test project
dotnet test test/DChess.Test.Unit/DChess.Test.Unit.csproj
```

### Running Applications
```bash
# Console application
dotnet run --project src/DChess.UI.Console/DChess.UI.Console.csproj

# Azure Function locally
dotnet run --project DChess.Function/DChess.Function.csproj
```

### Azure Functions Development
```bash
# Build function
dotnet build DChess.Function/DChess.Function.csproj

# Run locally (requires Azure Functions Core Tools)
func start --csharp --port 7071
```

## Key Implementation Details

### Chess Logic
- **Board representation**: 8x8 array using `PieceAttributes` structs
- **Move validation**: Handled through `MoveHandler` class with piece-specific validation
- **Game state**: Tracked in `Game` class with support for check/checkmate detection
- **Piece movement**: Each piece type implements its own movement rules

### Test Strategy
- Uses xUnit with FluentAssertions for readable test assertions
- Includes visualization helpers for debugging failed tests
- Test coverage focuses on piece movement, game rules, and edge cases
- Example tests demonstrate visualization capabilities

### AI Implementation
- Currently uses minimax algorithm with alpha-beta pruning
- Simple material-based evaluation function
- Fixed-depth search (configurable)
- See `MemoryBank/AI_Guidelines.md` for improvement roadmap

## Important Files and Locations

- **Core game logic**: `src/DChess.Core/Game/Game.cs`
- **Board representation**: `src/DChess.Core/Game/Board.cs`
- **Move handling**: `src/DChess.Core/Moves/MoveHandler.cs`
- **Piece implementations**: `src/DChess.Core/Flyweights/`
- **Test helpers**: `test/DChess.Test.Unit/TestHelpers/`
- **Example tests**: `test/DChess.Test.Unit/Examples/`

## Development Guidelines

### **Mandatory TDD Process**
Before writing ANY production code:
1. **Identify GitHub Issue**: Confirm work is tracked in GitHub issues
2. **Write Failing Test**: Create test(s) that fail and capture requirements
3. **Red Phase**: Verify test fails for the right reason
4. **Green Phase**: Write minimal code to make test pass
5. **Refactor Phase**: Improve code quality while keeping tests green
6. **Commit**: Commit with meaningful message referencing issue

### Code Quality
- Target .NET 8.0 with nullable reference types enabled
- Follow C# coding standards documented in `MemoryBank/Coding_Standards.md`
- Use comprehensive XML documentation for public APIs
- Implement proper error handling with custom exception types

### Testing Approach
- **ALWAYS write tests first** (TDD requirement)
- Use descriptive test names that explain the scenario
- Include visualization tests for complex game states
- Test both valid and invalid moves for each piece type
- Use the excellent visualization framework for debugging failures

### Chess Rule Implementation
- All standard chess rules are implemented except castling and en passant
- Check and checkmate detection is functional
- Stalemate detection needs additional work
- Pawn promotion is implemented

## Memory Bank Resources

The `MemoryBank/` directory contains extensive documentation:
- `Memory_Bank.md` - Quick reference guide
- `Rules.md` - Chess rule implementations
- `AI_Guidelines.md` - Chess AI development guidance
- `Test_Plan.md` - Comprehensive testing strategy
- `Implementation_Plan.md` - Step-by-step development guide

## Current Status & GitHub Issues

The project implements core chess functionality with 20 prioritized GitHub issues for development:

### **Completed Features:**
- ✅ Basic piece movement for all piece types
- ✅ Check and checkmate detection
- ✅ Move validation and illegal move prevention
- ✅ Game state tracking
- ✅ Multiple UI implementations (Console, WPF)
- ✅ REST API via Azure Functions

### **Active GitHub Issues:**
See the [GitHub Issues](https://github.com/DanMarshall909/DChess/issues) for current work items:

**Critical Priority (Must Fix First):**
- #1: Fix nullable reference type violation in Game class
- #2: Implement basic console chess game (MVP)
- #3: Fix performance issue in FriendlyPieces method

**High Priority:**
- #4: Implement castling rules
- #5: Implement en passant capture
- #6: Add WPF click-to-move functionality
- #7: Optimize Pieces property performance

**Medium Priority:** Issues #8-12 (testing, AI improvements, API design)
**Low Priority:** Issues #13-17 (advanced AI, documentation)
**Infrastructure:** Issues #18-20 (benchmarking, logging, deployment)

### **Development Rule:**
🚨 **ALL work must be done through GitHub issues. No ad-hoc feature requests.**

## CI/CD

The project uses Azure DevOps pipeline (`azure-pipelines.yml`) with:
- NuGet package restoration
- Solution build in Release configuration
- Unit test execution
- Qodana static analysis integration