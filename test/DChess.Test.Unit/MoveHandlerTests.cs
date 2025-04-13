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
        bestMove.ShouldNotBeMoveWithBoard(takeBlackPawn, Sut.Board, "the bishop will be taken on the next move");
    }

    [Theory(DisplayName = "Best move is calculated for CurrentPlayer")]
    [InlineData("3k3b/5p2/5P2/p7/8/8/3N4/4K3 w - - 0 1", "d2e4")]
    public void best_move_would_be(string fenString, string expectedBestMoveString)
    {
        Sut.Set(fenString);
        var bestMove = MoveHandler.GetBestMove(Sut, White, 2);
        bestMove.ShouldBeMoveWithBoard(new Move(expectedBestMoveString), Sut.Board);
    }

    [Fact(DisplayName = "At depth 2, the engine avoids capturing a pawn if it results in material loss")]
    public void bishop_does_not_capture_pawn_when_it_will_be_recaptured()
    {
        // Bxa3xc5 is legal, but leads to immediate Kxc5, losing the bishop
        Sut.Set("8/8/2k5/2p5/8/B7/8/K7 w - - 0 1");

        var losingMove = new Move(a3, c5); // Bxc5
        var bestMove = MoveHandler.GetBestMove(Sut, White, maxDepth: 2);

        bestMove.ShouldNotBeMoveWithBoard(losingMove, Sut.Board, "the bishop will be immediately recaptured by the king, resulting in a net loss");
    }

    [Theory(DisplayName = "Game state score correctly evaluates material advantage")]
    [InlineData("k7/8/8/8/8/8/8/K7 w - - 0 1", 0)] // Equal material (just kings)
    [InlineData("k7/8/8/8/8/8/8/KQ6 w - - 0 1", 9)] // White has queen advantage
    [InlineData("k7/8/8/8/8/8/8/KR6 w - - 0 1", 5)] // White has rook advantage
    [InlineData("k7/8/8/8/8/8/8/KB6 w - - 0 1", 3)] // White has bishop advantage
    public void game_state_score_correctly_evaluates_material_advantage(string fenString, int expectedScore)
    {
        Sut.Set(fenString);
        MoveHandler.GetGameStateScore(Sut, White).Should().Be(expectedScore);
    }

    [Theory(DisplayName = "Game state score correctly evaluates check and checkmate")]
    [InlineData("k7/8/8/8/8/8/8/K7 w - - 0 1", 0)] // No check
    [InlineData("k7/8/8/8/8/8/Q7/K7 w - - 0 1", 19)] // Check with queen (material + check bonus)
    [InlineData("k7/Q7/K7/8/8/8/8/8 b - - 0 1", -1000000)] // Checkmate
    public void game_state_score_correctly_evaluates_check_and_checkmate(string fenString, int expectedScore)
    {
        Sut.Set(fenString);
        MoveHandler.GetGameStateScore(Sut, White).Should().Be(expectedScore);
    }
}