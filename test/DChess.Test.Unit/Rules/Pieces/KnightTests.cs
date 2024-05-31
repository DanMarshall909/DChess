using DChess.Core.Moves;

namespace DChess.Test.Unit.Rules.Pieces;

public class KnightTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    private const int X = LegalPositionValue;

    private static MoveOffset[] LegalOffsets => (new byte[17, 17]
    {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
    }).ToMoveOffsets();

    [Fact(DisplayName = "Knights can only move in an L shape")]
    public void knights_can_move_in_an_L_shape()
    {
        WhiteKnight.ShouldBeAbleToMoveTo(LegalOffsets);
    }

    [Fact(DisplayName = "Knights can jump over other pieces")]
    public void knights_can_jump_over_other_pieces()
    {
        WhiteKnight.ShouldBeAbleToMoveTo(LegalOffsets,
            (board, coordinate) => board.SurroundWith(coordinate, WhitePawn));
    }


    [Fact(DisplayName = "Knights cannot move outside of an L shape")]
    public void knights_can_only_move_in_an_L_shape()
    {
        var invalidOffsets = LegalOffsets.Inverse().ToList().AsReadOnly();
        WhiteKnight.ShouldNotBeAbleToMoveTo(invalidOffsets);
    }
}