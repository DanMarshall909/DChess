﻿using DChess.Core.Moves;

namespace DChess.Core.Pieces;

internal record Bishop : Piece
{
    public Bishop(Arguments arguments) : base(arguments)
    {
    }
    
    public override string PieceName => "Bishop";

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Current, to);
        if (!move.IsDiagonal)
            return move.AsInvalidResult("Bishop can only move diagonally");
        
        return move.AsOkResult;
    }
}