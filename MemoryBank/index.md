# DChess Memory Bank Index

This Memory Bank serves as a centralized knowledge repository for the DChess project. It contains documentation, guidelines, and reference materials to assist in the development and maintenance of the chess engine.

## Contents

1. [Memory Bank](./Memory_Bank.md) - Quick reference guide for the DChess project
2. [Chess Rules](./Rules.md) - Comprehensive reference for chess rules as implemented in DChess
3. [Cline Rules](./Cline_Rules.md) - Guidelines for AI assistance with the DChess project
4. [AI Guidelines](./AI_Guidelines.md) - Specialized guidance for chess AI development
5. [Coding Standards](./Coding_Standards.md) - C# coding standards and best practices for the project
6. [Test Plan](./Test_Plan.md) - Detailed plan for unit tests covering existing features
7. [Implementation Plan](./Implementation_Plan.md) - Step-by-step guide for implementing the tests
8. [Sample Check Detection Tests](./Sample_CheckDetectionTests.cs) - Example implementation of check detection tests
9. [Sample Stalemate Tests](./Sample_StalemateTests.cs) - Example implementation of stalemate detection tests
10. [Sample Illegal Move Tests](./Sample_IllegalMoveTests.cs) - Example implementation of illegal move tests
11. [Summary](./Summary.md) - Summary of Memory Bank initialization and next steps
12. [Example Tests](../test/DChess.Test.Unit/Examples/) - Example tests demonstrating visualization capabilities:
    - [Visualization Examples](../test/DChess.Test.Unit/Examples/VisualizationExampleTests.cs) - Basic visualization examples
    - [MoveHandler Visualization Examples](../test/DChess.Test.Unit/Examples/MoveHandlerVisualizationExampleTests.cs) - Move handler visualization
    - [Best Move Visualization Examples](../test/DChess.Test.Unit/Examples/BestMoveVisualizationExampleTests.cs) - Best move calculation visualization

## Purpose

The Memory Bank is designed to:

1. **Provide Quick Reference** - Easily accessible information about chess rules and project structure
2. **Maintain Consistency** - Ensure consistent coding practices and design patterns
3. **Guide Development** - Offer clear guidelines for implementing new features
4. **Document Decisions** - Record architectural and design decisions for future reference
5. **Assist Onboarding** - Help new contributors understand the project quickly

## How to Use

- **For Quick Reference**: Refer to the Memory_Bank.md file for a high-level overview
- **For Chess Rules**: Consult Rules.md for detailed chess rule implementations
- **For Development Guidelines**: Use Cline_Rules.md and Coding_Standards.md when writing code
- **For AI Development**: Follow AI_Guidelines.md when working on the chess engine's AI components

## Maintenance

This Memory Bank should be kept up-to-date as the project evolves:

1. Update documentation when implementing new features
2. Revise guidelines based on evolving best practices
3. Add new sections as needed to cover additional aspects of the project
4. Review and refine existing content periodically

## Test Coverage Reference

Current test coverage focuses on:

- Basic piece movement rules
- Check and checkmate detection
- Game state tracking
- Move validation
- Pawn promotion
- Visualization capabilities for test failures
- Best move calculations

Areas needing additional test coverage:

- Edge cases for existing rules
- Comprehensive stalemate detection
- Game state transitions
- Pinned piece scenarios
- Detailed check resolution tests
- Advanced AI decision making
- Performance testing for move calculations
