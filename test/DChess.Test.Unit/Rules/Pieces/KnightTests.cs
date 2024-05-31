using DChess.Core.Moves;

namespace DChess.Test.Unit.Rules.Pieces;

public class KnightTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    // ReSharper disable once InconsistentNaming
    private readonly Offset[] _LShapedOffsetsFromCurrentPosition =
    {
        (1, 2),
        (1, -2),
        (-1, 2),
        (-1, -2),

        (2, 1),
        (2, -1),
        (-2, 1),
        (-2, -1)
    };

    [Fact(DisplayName = "Knights can only move in an L shape")]
    public void knights_can_move_in_an_L_shape()
    {
        WhiteKnight.ShouldBeAbleToMoveToOffsets(_LShapedOffsetsFromCurrentPosition);
    }

    [Fact(DisplayName = "Knights can jump over other pieces")]
    public void knights_can_jump_over_other_pieces()
    {
        WhiteKnight.ShouldBeAbleToMoveToOffsets(
            offsetsFromCurrentPosition: _LShapedOffsetsFromCurrentPosition,
            setupBoard: (board, coordinate) => board.SurroundWith(coordinate, WhitePawn));
    }


    [Fact(DisplayName = "Knights cannot move outside of an L shape")]
    public void knights_can_only_move_in_an_L_shape()
    {
        var invalidOffsets = _LShapedOffsetsFromCurrentPosition.Inverse().ToList().AsReadOnly();
        WhiteKnight.ShouldNotBeAbleToMoveTo(invalidOffsets);
    }
}