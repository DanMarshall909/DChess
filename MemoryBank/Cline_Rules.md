# Rules for Cline When Working with DChess

This document outlines specific guidelines for Cline (AI assistant) when working with the DChess project.

## Code Modification Guidelines

### General Approach

1. **Understand Before Modifying**

   - Always read and understand the relevant code files before making changes
   - Pay special attention to the architecture and design patterns used in the project
   - Consider how changes might affect other parts of the codebase

2. **Maintain Design Patterns**

   - Preserve the Flyweight pattern used for chess pieces
   - Respect the separation of concerns between game logic, moves, and pieces
   - Follow the established factory pattern for creating chess pieces

3. **Code Style Consistency**

   - Value idiomatic C# practices and prioritize clarity,
   - Use clear and descriptive variable names
   - Avoid overly complex expressions; prefer simple, straightforward code
   - Rank readability and maintainability over cleverness
   - Abstract away complex logic into well-named methods
   - Follow the existing code style (indentation, naming conventions, etc.)
   - Use C# 8.0+ features appropriately but consistently with the codebase
   - Maintain XML documentation style for public APIs

### Testing Requirements

1. **Test-Driven Development**

   - ALWAYS write or update tests before implementing new features
   - Ensure all tests pass after making changes
   - Maintain high test coverage for all components

2. **Test Specific Chess Rules**
   - When implementing chess rules, create specific test cases for edge cases
   - Test both valid and invalid moves for each piece type
   - Verify check, checkmate, and stalemate detection

## Feature Implementation Priority

When implementing new features, follow this priority order:

1. **Core Chess Rules**

   - Complete any missing basic chess rules (castling, en passant)
   - Ensure all game end conditions are properly detected

2. **Performance Optimizations**

   - Implement bitboard representation for improved performance
   - Optimize move generation and validation
   - Enhance position evaluation with more sophisticated criteria

3. **User Experience**
   - Improve the console UI for better visualization
   - Enhance the API for better integration with other systems
   - Add move suggestions and explanations

## Code Review Checklist

Before submitting changes, verify:

- [ ] All tests pass
- [ ] Code follows the project's style guidelines
- [ ] New features are properly documented
- [ ] Performance impact has been considered
- [ ] No regression in existing functionality

## Technical Debt Management

When encountering technical debt:

1. **Document the Issue**

   - Add a comment with a TODO that clearly describes the issue
   - Include the reason why it's a problem and potential solutions

2. **Prioritize Fixes**

   - Address high-impact technical debt first (e.g., memory leaks, performance bottlenecks)
   - Balance fixing technical debt with implementing new features

3. **Refactor Safely**
   - Make small, incremental changes when refactoring
   - Ensure tests pass after each refactoring step

## Communication Guidelines

When discussing the DChess project:

1. **Be Precise**

   - Use correct chess terminology
   - Reference specific files and line numbers when discussing code
   - Be explicit about which chess rules are being discussed

2. **Provide Context**

   - Explain the reasoning behind suggested changes
   - Reference relevant design patterns or best practices
   - Connect changes to the project's goals and roadmap

3. **Offer Alternatives**
   - When suggesting a solution, provide alternatives with pros and cons
   - Consider different approaches based on performance, maintainability, and complexity
