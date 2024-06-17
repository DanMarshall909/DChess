using DChess.Core.Game;

namespace DChess.Test.Unit;

public class MoveHandlerTests: GameTestBase
{
    [Fact(DisplayName = "A move score can be calculated")]
    public void a_move_score_can_be_calculated()
    {
        SetupBlackBishopCanBeTakenByWhiteKnight();

        var move = new Move(c3, d5);
        var score = MoveHandler.GetGameStateScore(move, Sut.GameState, Colour.White);

        score.Should().Be(3);
    }
    
    [Fact(DisplayName = "The best move can be found")]
    public void the_best_move_can_be_found()
    {
        SetupBlackBishopCanBeTakenByWhiteKnight();

        var bestMove = MoveHandler.GetBestMove(Colour.White, Sut.GameState);

        bestMove.Should().Be(new Move(c3, d5));
    }

    private void SetupBlackBishopCanBeTakenByWhiteKnight()
    {
        Sut.GameState.SetStandardLayout();
        
        Sut.GameState.BoardState.RemovePieceAt(b1);
        Sut.GameState.BoardState.SetPiece(c3, WhiteKnight);
        
        Sut.GameState.BoardState.RemovePieceAt(f8);
        Sut.GameState.BoardState.SetPiece(d5, BlackBishop);
    }
}