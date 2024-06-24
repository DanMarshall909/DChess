using DChess.Core.Game;
using static DChess.Core.Game.PieceType;

namespace DChess.Test.Unit;

public class MoveHandlerTests : GameTestBase
{
    [Fact(DisplayName = "A move score can be calculated")]
    public void a_move_score_can_be_calculated()
    {
        Sut.Board.SetStandardLayout();
        Sut.Board.RemovePieceAt(b1);
        Sut.Board.Place(WhiteKnight, c3);
        Sut.Board.RemovePieceAt(f8);
        Sut.Board.Place(BlackBishop, d5);
        MoveHandler.GetGameStateScore(Sut, Colour.White).Should().Be(0);

        var takeBishop = new Move(c3, d5);
        Sut.Move(takeBishop);
        MoveHandler.GetGameStateScore(Sut, Colour.White).Should().Be(Knight.Value());
        MoveHandler.GetGameStateScore(Sut, Colour.Black).Should().Be(-Knight.Value());
    }

    [Fact(DisplayName = "The best move can be found when there are multiple options")]
    public void the_best_move_can_be_found_when_there_are_multiple_options()
    {
        Sut.Board.SetStandardLayout();
        Sut.Board.RemovePieceAt(b1);
        Sut.Board.RemovePieceAt(f8);

        var takeBishop = new Move(c3, d5);
        Sut.Board.Place(WhiteKnight, c3);

        Sut.Board.Place(BlackBishop, d5);
        MoveHandler.GetBestMove(Sut, Colour.White).Should().Be(takeBishop);

        Sut.Board.Place(BlackPawn, e4);
        MoveHandler.GetBestMove(Sut, Colour.White).Should().Be(takeBishop);

        Sut.Board.Place(BlackQueen, b5);
        var takeQueen = new Move(c3, b5);

        MoveHandler.GetBestMove(Sut, Colour.White).Should().Be(takeQueen);
    }

    [Fact(DisplayName = "The best move can be found by searching multiple moves ahead")]
    public void the_best_move_can_be_found_by_searching_multiple_moves_ahead()
    {
        Sut.Board.SetStandardLayout();
        Sut.Board.RemovePieceAt(b1);
        Sut.Board.RemovePieceAt(f8);

        var takeBishop = new Move(c3, d5);
        Sut.Board.Place(WhiteKnight, c3);

        Sut.Board.Place(BlackBishop, d5);
        MoveHandler.GetBestMove(Sut, Colour.White).Should().Be(takeBishop);

        Sut.Board.Place(BlackPawn, e4);
        MoveHandler.GetBestMove(Sut, Colour.White).Should().Be(takeBishop);

        Sut.Board.Place(BlackQueen, b5);
        var takeQueen = new Move(c3, b5);

        MoveHandler.GetBestMove(Sut, Colour.White).Should().Be(takeQueen);
    }
}