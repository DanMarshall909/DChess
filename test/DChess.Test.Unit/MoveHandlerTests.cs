using DChess.Core.Game;

namespace DChess.Test.Unit;

public class MoveHandlerTests : GameTestBase
{
    [Fact(DisplayName = "A move score can be calculated")]
    public void a_move_score_can_be_calculated()
    {
        Sut.SetStandardLayout();

        Sut.Board.RemovePieceAt(b1);
        Sut.Board.Place(WhiteKnight, c3);
        Sut.Board.RemovePieceAt(f8);
        Sut.Board.Place(BlackBishop, d5);

        var moveToTakeBishop = new Move(c3, d5);
        var score = MoveHandler.GetGameStateScore(moveToTakeBishop, Sut, Colour.White);

        score.Should().Be(3);
    }

    [Fact(DisplayName = "The best move can be found")]
    public void the_best_move_can_be_found()
    {
        Sut.SetStandardLayout();

        Sut.Board.RemovePieceAt(b1);
        Sut.Board.RemovePieceAt(f8);

        Sut.Board.Place(WhiteKnight, c3);
        Sut.Board.Place(BlackBishop, d5);
        Sut.Board.Place(BlackPawn, e4);
        Sut.Board.Place(BlackQueen, b5);

        var bestMove = MoveHandler.GetBestMove(Colour.White, Sut);

        var moveToTakeQueen = new Move(c3, b5);
        bestMove.Should().Be(moveToTakeQueen);
    }
}