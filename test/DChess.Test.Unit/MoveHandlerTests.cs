using DChess.Core.Game;

namespace DChess.Test.Unit;

public class MoveHandlerTests: GameTestBase
{
    [Fact(DisplayName = "A move score can be calculated")]
    public void a_move_score_can_be_calculated()
    {
        SetupBlackBishopCanBeTakenByWhiteKnight();

        var move = new Move(c3, d5);
        var score = MoveHandler.GetGameStateScore(move, Sut, Colour.White);

        score.Should().Be(3);
    }
    
    [Fact(DisplayName = "The best move can be found")]
    public void the_best_move_can_be_found()
    {
        SetupBlackBishopCanBeTakenByWhiteKnight();

        var bestMove = MoveHandler.GetBestMove(Colour.White, Sut);

        bestMove.Should().Be(new Move(c3, d5));
    }

    private void SetupBlackBishopCanBeTakenByWhiteKnight()
    {
        Sut.SetStandardLayout();
        
        Sut.BoardState.RemovePieceAt(b1);
        Sut.BoardState.SetPiece(c3, WhiteKnight);
        
        Sut.BoardState.RemovePieceAt(f8);
        Sut.BoardState.SetPiece(d5, BlackBishop);
    }
}