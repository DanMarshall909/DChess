namespace DChess.Test.Unit.Rules.Pieces;

public class KingTests: GameTestBase
{
    private const int X = LegalPositionValue;

    [Fact(DisplayName = "Kings can only move diagonally, vertically or horizontally")]
    public void kings_can_only_move_diagonally_vertically_or_horizontally()
    {
        WhiteKing.ShouldOnlyBeAbleToMoveTo(new byte[15, 15]
        {
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, X, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, 0, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, X, X, X, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
        }.ToMoveOffsets(), ErrorHandler);
    }

    [Fact(DisplayName = "A piece cannot move such that the king is in check")]
    public void a_piece_cannot_move_such_that_the_king_is_in_check()
    {
        Sut.Board.Place(WhiteKing, a1);
        Sut.Board.Place(WhiteBishop, a2);
        Sut.Board.Place(BlackRook, a8);

        var result = Sut.Pieces[a2].CheckMove(b3, Sut);

        result.Validity.Should().Be(CannotMoveIntoCheck);
    }
}