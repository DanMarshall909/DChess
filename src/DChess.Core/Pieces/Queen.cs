﻿using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public record Queen : Piece
{
    public Queen(Arguments arguments) : base(arguments)
    {
    }

    public override string PieceName => "Queen";

    protected override MoveResult ValidateMove(Coordinate to, GameState gameState)
    {
        var move = new Move(Coordinate, to);

        return move.IsDiagonal || move.IsVertical || move.IsHorizontal
            ? move.OkResult()
            : move.InvalidResult(QueenCanOnlyMoveDiagonallyOrInAStraightLine);
    }
}