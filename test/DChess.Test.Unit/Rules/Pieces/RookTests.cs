﻿namespace DChess.Test.Unit.Rules.Pieces;

public class RookTests : GameTestBase
{
    private const int X = LegalPositionValue;

    [Fact(DisplayName = "Rooks can only move vertically or horizontally")]
    public void rooks_can_only_move_vertically()
    {
        var moveOffsets = new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { X, X, X, X, X, X, X, 0, X, X, X, X, X, X, X },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 }
        }.ToMoveOffsets();
        WhiteRook.ShouldOnlyBeAbleToMoveTo(moveOffsets, new TestErrorHandler());
    }

    [Fact(DisplayName = "Rooks cannot jump over other pieces")]
    public void rooks_cannot_jump_over_other_pieces()
    {
        WhiteRook.ShouldOnlyBeAbleToMoveTo(new byte[,]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, X, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        }.ToMoveOffsets(), ErrorHandler, (board, square) =>
            board.Surround2CellsFrom(square, WhitePawn));
    }
}