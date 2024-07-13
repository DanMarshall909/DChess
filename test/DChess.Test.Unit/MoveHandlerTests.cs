using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class MoveHandlerTests : GameTestBase
{
    [Theory(DisplayName = "Game score can be calculated for CurrentPlayer")]
    [InlineData("k7/p7/KPP5/8/8/8/8/8 w - - 0 1", 1)]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", 0)]
    public void game_score_can_be_calculated(string fenString, int expectedScore)
    {
        Sut.Set(fenString);
        MoveHandler.GetGameStateScore(Sut, White).Should().Be(expectedScore, $"Lichess url: {Sut.AsLichessUrl}");
    }

    [Fact(DisplayName =
        "A move that would result in the opponent having a better score on the next move should be avoided")]
    public void a_move_that_would_result_in_the_opponent_having_a_better_score_on_the_next_move_should_be_avoided()
    {
        Sut.Set("2k5/2p5/8/B7/1P6/8/8/K7 w - - 0 1");

        var bestMove = MoveHandler.GetBestMove(Sut, White, 2);
        var takeBlackPawn = new Move(a5, c7);
        ShouldNotBe(bestMove, takeBlackPawn, "the bishop will be taken on the next move");
    }

    [Theory(DisplayName = "Best move is calculated for CurrentPlayer", Skip = "Skip for now")]
    [InlineData("3k3b/5p2/5P2/p7/8/8/3N4/4K3 w - - 0 1", "d2e4")]
    public void best_move_would_be(string fenString, string expectedBestMoveString)
    {
        Sut.Set(fenString);
        var bestMove = MoveHandler.GetBestMove(Sut, White, 2);
        ShouldBe(bestMove, new Move(expectedBestMoveString));
    }

    private void ShouldBe(Move move, Move expected)
    {
        move.Format().Should().Be(expected.Format());
    }

    private void ShouldNotBe(Move move, Move expectedNotToBe, string because)
    {
        move.Format().Should().NotBe(expectedNotToBe.Format(), because);
    }

    [Fact(DisplayName = "The best move can be found by searching multiple moves ahead",
        Skip = "Leaving this for now until I have the simpler tests working")]
    public void the_best_move_can_be_found_by_searching_multiple_moves_ahead()
    {
        Sut.Set("k7/p7/KPP5/8/8/8/8/8 w - - 0 1");
        var takePawn = new Move(d7, b6);
        var moveKnightToSetUpCheckmateNextMove = new Move(d7, f6);

        var bestMove = MoveHandler.GetBestMove(Sut, White, 3);
        bestMove.Format().Should().NotBe(takePawn.Format());
        bestMove.Format().Should().Be(moveKnightToSetUpCheckmateNextMove.Format(),
            "The knight should move to set up a checkmate");
    }
}