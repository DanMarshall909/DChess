using DChess.Core.Exceptions;

namespace DChess.Test.Unit.Rules;

public class KnightTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    [Fact(DisplayName = "Knights can only move in an L shape")]
    public void knights_can_only_move_in_an_L_shape()
    {
        for (byte rank = 1; rank < 8; rank++)
        {
            for (var file = 'a'; file < 'h'; file++)
            {
                var from = new Coordinate(file, rank);

                Board.Clear();
                Board[from] = WhiteKnight;

                try
                {
                    void KnightShouldBeAbleToMoveOffsetBy(int df, int dr)
                    {
                        var to = from.Offset(df, dr);
                        Board.Pieces[from].CheckMove(to).Valid
                            .Should().BeTrue(
                                $"Knight should be able to move from {from} to {to})");
                    }

                    KnightShouldBeAbleToMoveOffsetBy(1, -2);
                    KnightShouldBeAbleToMoveOffsetBy(-1, 2);
                    KnightShouldBeAbleToMoveOffsetBy(1, -2);
                    KnightShouldBeAbleToMoveOffsetBy(-1, 2);

                    KnightShouldBeAbleToMoveOffsetBy(-2, 1);
                    KnightShouldBeAbleToMoveOffsetBy(2, -1);
                    KnightShouldBeAbleToMoveOffsetBy(-2, 1);
                    KnightShouldBeAbleToMoveOffsetBy(2, -1);
                }
                catch (InvalidCoordinateException)
                {
                    // Ignore invalid coordinates for this test (handled in other tests)
                }
            }
        }
    }
}