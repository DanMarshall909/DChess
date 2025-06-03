# AI Implementation Guidelines for DChess

This document provides specific guidelines for working with and improving the AI components of the DChess project.

## Chess AI Architecture

### Current Implementation

The DChess AI currently uses:
- Minimax algorithm with alpha-beta pruning
- Simple material-based evaluation function
- Fixed-depth search

### Improvement Areas

When enhancing the chess AI, focus on these areas in order of priority:

## 1. Evaluation Function

### Material Evaluation
- Maintain the basic piece values (Pawn=1, Knight=3, Bishop=3, Rook=5, Queen=9)
- Consider implementing piece-square tables for positional evaluation
- Add bonuses/penalties for specific piece configurations

```csharp
// Example piece-square table for pawns (simplified)
int[,] pawnTable = {
    {0,  0,  0,  0,  0,  0,  0,  0},
    {50, 50, 50, 50, 50, 50, 50, 50},
    {10, 10, 20, 30, 30, 20, 10, 10},
    {5,  5, 10, 25, 25, 10,  5,  5},
    {0,  0,  0, 20, 20,  0,  0,  0},
    {5, -5,-10,  0,  0,-10, -5,  5},
    {5, 10, 10,-20,-20, 10, 10,  5},
    {0,  0,  0,  0,  0,  0,  0,  0}
};
```

### Positional Evaluation
- Center control: Bonus for pieces controlling central squares
- King safety: Penalties for exposed king, especially in the middlegame
- Pawn structure: Evaluate doubled, isolated, and passed pawns
- Piece mobility: Count legal moves for each piece as a measure of activity

### Game Phase Recognition
- Implement detection of opening, middlegame, and endgame phases
- Apply different evaluation weights based on the game phase
- In endgames, prioritize king activity and pawn advancement

## 2. Search Improvements

### Iterative Deepening
- Start with shallow searches and progressively increase depth
- Use results from previous depths to improve move ordering
- Set a time limit rather than a fixed depth

```csharp
public Move GetBestMoveWithTimeLimit(Game game, Colour forColour, int timeLimit)
{
    Move bestMove = null;
    DateTime startTime = DateTime.Now;
    
    for (int depth = 1; depth <= MaxDepth; depth++)
    {
        // Check if we've exceeded the time limit
        if ((DateTime.Now - startTime).TotalMilliseconds > timeLimit * 0.8)
            break;
            
        Move move = GetBestMove(game, forColour, depth);
        bestMove = move; // Update best move
    }
    
    return bestMove;
}
```

### Move Ordering
- Implement move ordering to improve alpha-beta pruning efficiency:
  1. Captures (ordered by MVV-LVA: Most Valuable Victim - Least Valuable Aggressor)
  2. Promotions
  3. Checks
  4. Killer moves (non-capturing moves that caused beta cutoffs)
  5. History heuristic (moves that have been good in similar positions)

### Transposition Tables
- Implement a hash table to store previously evaluated positions
- Use Zobrist hashing for efficient position identification
- Store evaluation scores, best moves, and search depth

## 3. Advanced Techniques

### Quiescence Search
- Extend search in "noisy" positions with captures and checks
- Prevents the horizon effect where tactical sequences are cut off

### Null Move Pruning
- Skip a turn to quickly identify positions where one side is so strong that even giving up a move won't change the evaluation
- Only use in non-zugzwang positions

### Late Move Reduction
- Search less promising moves with reduced depth
- If the reduced search indicates a good move, re-search at full depth

## Implementation Guidelines

1. **Incremental Approach**
   - Implement one improvement at a time
   - Measure performance impact before and after each change
   - Use test positions to verify correctness

2. **Performance Considerations**
   - Profile the code to identify bottlenecks
   - Optimize the most frequently called functions
   - Consider using parallel search for higher depths

3. **Testing**
   - Create a suite of test positions with known best moves
   - Implement a way to measure nodes searched per second
   - Compare performance against previous versions

## Chess AI Resources

When implementing these improvements, refer to these resources:

- **Chess Programming Wiki**: https://www.chessprogramming.org/
- **Crafty Chess Engine**: Study its evaluation function and search techniques
- **Stockfish**: Open-source chess engine with excellent search algorithms

## Benchmarking

Regularly benchmark the AI against:

1. Previous versions to measure improvement
2. Standard test suites like "Strategic Test Suite" or "Tactical Test Suite"
3. Time-to-depth measurements to evaluate search efficiency
