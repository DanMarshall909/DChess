# C# Coding Standards for DChess

This document outlines the coding standards and best practices to follow when working on the DChess project.

## Code Organization

### File Structure

- One class per file (except for small related classes)
- Filename should match the primary class name
- Group related files in appropriate directories
- Follow the existing project structure:
  - `Game/` for core game logic
  - `Moves/` for move-related classes
  - `Flyweights/` for chess piece implementations
  - `Renderers/` for display-related code
  - `Errors/` for exception and error handling

### Class Structure

- Order class members as follows:
  1. Constants
  2. Fields
  3. Properties
  4. Constructors
  5. Public methods
  6. Protected methods
  7. Private methods
  8. Nested types

- Group related methods together
- Keep methods focused on a single responsibility

## Naming Conventions

### General Guidelines

- Use meaningful, descriptive names
- Avoid abbreviations except for widely accepted ones
- Use PascalCase for types, methods, properties, and constants
- Use camelCase for local variables and parameters
- Prefix private fields with underscore (`_fieldName`)
- Avoid Hungarian notation

### Specific Naming Patterns

- Interfaces should start with "I" (e.g., `IErrorHandler`)
- Abstract classes may end with "Base" if appropriate
- Exception classes should end with "Exception"
- Extension method classes should end with "Extensions"

## Coding Style

### Formatting

- Use 4 spaces for indentation (not tabs)
- Place braces on new lines
- Keep lines under 120 characters
- Use a single blank line to separate logical groups of code
- Use parentheses to clarify operator precedence, even when not strictly necessary

### Language Features

- Use C# 8.0+ features appropriately:
  - Nullable reference types
  - Pattern matching
  - Switch expressions
  - Using declarations
  - Default interface methods when appropriate
- Prefer expression-bodied members for simple methods and properties
- Use `var` when the type is obvious from the right side of the assignment
- Use object initializers and collection initializers when appropriate
- Prefer LINQ query methods over query syntax for consistency

### Error Handling

- Use the established error handling pattern with `IErrorHandler`
- Throw specific exceptions rather than generic ones
- Include meaningful error messages
- Document exceptions in XML comments

## Documentation

### XML Documentation

- Document all public APIs with XML comments
- Include `<summary>` for all public types and members
- Use `<param>`, `<returns>`, and `<exception>` tags as appropriate
- Provide examples for complex or non-obvious usage

Example:
```csharp
/// <summary>
/// Validates a move according to chess rules.
/// </summary>
/// <param name="move">The move to validate.</param>
/// <param name="game">The current game state.</param>
/// <returns>A <see cref="MoveResult"/> indicating whether the move is valid.</returns>
/// <exception cref="InvalidMoveException">Thrown when the move is invalid.</exception>
public MoveResult ValidateMove(Move move, Game game)
{
    // Implementation
}
```

### Comments

- Use comments to explain "why", not "what"
- Keep comments up-to-date with code changes
- Use `// TODO: ` comments for planned improvements
- Avoid commented-out code

## Performance Considerations

### General Guidelines

- Be mindful of memory allocations, especially in performance-critical paths
- Use value types (structs) appropriately for small, immutable data
- Consider using object pooling for frequently created/destroyed objects
- Avoid unnecessary LINQ operations in performance-critical code

### Chess-Specific Optimizations

- Use efficient board representations (consider bitboards for future optimization)
- Optimize move generation and validation
- Cache results of expensive calculations where appropriate
- Use the Flyweight pattern consistently for chess pieces

## Testing

### Test Organization

- Group tests by the class they're testing
- Name test classes with the pattern `[ClassUnderTest]Tests`
- Organize tests into logical categories

### Test Naming

- Use descriptive test names that explain the scenario and expected outcome
- Follow the pattern: `[Method_Scenario_ExpectedBehavior]`
- For theory tests, use display names that clearly describe the test case

### Test Implementation

- Follow the Arrange-Act-Assert pattern
- One assertion per test when possible
- Use theory tests for testing multiple similar scenarios
- Mock dependencies appropriately

## Version Control

### Commit Messages

- Write clear, descriptive commit messages
- Use the imperative mood ("Add feature" not "Added feature")
- Reference issue numbers when applicable

### Branching

- Use feature branches for new development
- Keep branches focused on a single feature or fix
- Regularly merge from the main branch to avoid conflicts

## Example Code

### Good Example

```csharp
public class MoveValidator
{
    private readonly IErrorHandler _errorHandler;
    
    public MoveValidator(IErrorHandler errorHandler)
    {
        _errorHandler = errorHandler ?? throw new ArgumentNullException(nameof(errorHandler));
    }
    
    /// <summary>
    /// Validates if a move is legal according to chess rules.
    /// </summary>
    /// <param name="move">The move to validate.</param>
    /// <param name="game">The current game state.</param>
    /// <returns>A result indicating if the move is valid and why.</returns>
    public MoveResult ValidateMove(Move move, Game game)
    {
        if (move == Move.NullMove)
        {
            return new MoveResult(move, MoveValidity.InvalidMove);
        }
        
        // Additional validation logic
        
        return new MoveResult(move, MoveValidity.Ok);
    }
}
```

### Bad Example (Avoid)

```csharp
// Don't do this
public class mv
{
    IErrorHandler e;
    
    public mv(IErrorHandler err)
    {
        e = err;
    }
    
    public MoveResult v(Move m, Game g)
    {
        if (m == Move.NullMove) return new MoveResult(m, MoveValidity.InvalidMove);
        // More validation
        return new MoveResult(m, MoveValidity.Ok);
    }
}
