﻿using DChess.Core.Board;
using DChess.Core.Moves;

namespace DChess.Core.Pieces;

internal record Queen : Piece
{
    public Queen(Arguments arguments) : base(arguments)
    {
    }

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Position, to);
        
        if (!(move.IsDiagonal || move.IsVertical || move.IsHorizontal))
            return move.AsInvalidResult("Queen can only move diagonally, or in a straight line");
        
        return move.AsOkResult;
    }
}