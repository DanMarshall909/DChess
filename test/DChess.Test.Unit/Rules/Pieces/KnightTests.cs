namespace DChess.Test.Unit.Rules.Pieces;

public class KnightTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    private const int X = LegalPositionValue;

    private static MoveOffset[] LegalOffsets => new byte[15, 15]
    {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, X, 0, 0, 0, X, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    }.ToMoveOffsets();

    [Fact(DisplayName = "Knights can only move in an L shape")]
    public void knights_can_only_move_in_an_L_shape()
    {
        WhiteKnight.ShouldOnlyBeAbleToMoveTo(LegalOffsets);
    }

    [Fact(DisplayName = "Knights can jump over other pieces")]
    public void knights_can_jump_over_other_pieces()
    {
        WhiteKnight.ShouldOnlyBeAbleToMoveTo(LegalOffsets,
            (board, coordinate) => board.Surround(coordinate, WhitePawn));
    }
}