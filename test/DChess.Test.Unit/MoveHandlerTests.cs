using DChess.Core.Game;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class MoveHandlerTests : GameTestBase
{
	private const string NoCheckNoMaterialAdvantage = "k7/8/8/8/8/8/8/K7 w - - 0 1";
	private const string BlackKingInCheckWith1PawnMaterialAdvantage = "k7/1P6/8/8/8/8/8/K7 w - - 0 1";
	private const string BlackCheckMateWith2PawnWhiteAdvantage = "k7/PP6/1K6/8/8/8/8/8 w - - 0 1";

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

	[Theory(DisplayName = "Best move is calculated for CurrentPlayer")]
	[InlineData("3k3b/5p2/5P2/p7/8/8/3N4/4K3 w - - 0 1", "d2e4")]
	public void best_move_is(string fenString, string move)
	{
		Sut.Set(fenString);

		var expectedMove = new Move(move);

		var bestMove = expectedMove;

		ShouldBe(bestMove, expectedMove);
	}

	private void ShouldBe(Move move, Move expected)
	{
		move.Format().Should().Be(expected.Format());
	}

	private void ShouldNotBe(Move move, Move expectedNotToBe, string because)
	{
		move.Format().Should().NotBe(expectedNotToBe.Format(), because);
	}

	[Fact(DisplayName = "At depth 2, the engine avoids capturing a pawn if it results in material loss")]
	public void bishop_does_not_capture_pawn_when_it_will_be_recaptured()
	{
		// Bxa3xc5 is legal, but leads to immediate Kxc5, losing the bishop
		Sut.Set("8/8/2k5/2p5/8/B7/8/K7 w - - 0 1");

		var losingMove = new Move(a3, c5); // Bxc5
		var bestMove = MoveHandler.GetBestMove(Sut, White, maxDepth: 2);

		bestMove.Should().NotBe(losingMove, "the bishop will be immediately recaptured by the king, resulting in a net loss");
	}


	[Theory(DisplayName = "Game state score correctly evaluates material advantage")]
	[InlineData(NoCheckNoMaterialAdvantage, 0)] // Equal material (just kings)
	[InlineData("k7/8/8/8/8/8/8/KQ6 w - - 0 1", 9)] // White has queen advantage
	[InlineData("k7/8/8/8/8/8/8/KR6 w - - 0 1", 5)] // White has rook advantage
	[InlineData("k7/8/8/8/8/8/8/KB6 w - - 0 1", 3)] // White has bishop advantage
	public void game_state_score_correctly_evaluates_material_advantage(string fenString, int expectedScore)
	{
		Sut.Set(fenString);
		MoveHandler.GetGameStateScore(Sut, White).Should().Be(expectedScore);
	}

	[Theory(DisplayName = "Game state score correctly evaluates check and checkmate")]
	[InlineData(NoCheckNoMaterialAdvantage, 0)]
	[InlineData(BlackKingInCheckWith1PawnMaterialAdvantage, Weights.Material.Pawn + Weights.GameState.Checkmate)]  // Checkmate with pawn
	[InlineData(BlackCheckMateWith2PawnWhiteAdvantage, Weights.Material.Pawn * 2 + Weights.GameState.Checkmate)] // Checkmate with 2 pawns
	public void game_state_score_correctly_evaluates_check_and_checkmate(string fenString, int expectedScore)
	{
		Sut.Set(fenString);
		MoveHandler.GetGameStateScore(Sut, White).Should().Be(expectedScore);
	}
}
