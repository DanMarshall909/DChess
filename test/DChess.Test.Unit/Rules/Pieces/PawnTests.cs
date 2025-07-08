using DChess.Core.Game;

namespace DChess.Test.Unit.Rules.Pieces;

public class PawnTests : GameTestBase
{
    [Fact(DisplayName = "Pawns can move forward one square")]
    public void pawn_can_move_forward_one_square()
    {
        Sut.Board.Place(WhiteKing, f1);
        Sut.Board.Place(WhitePawn, a1);

        Sut.Move(a1, b1);
    }

    [Fact(DisplayName = "Pawns can only move forward two squares from starting position")]
    public void pawn_can_move_forward_two_squares_from_starting_position()
    {
        Sut.Board.Place(WhiteKing, f1);
        Sut.Board.Place(WhitePawn, b2);

        Sut.Move(b2, b3);
        Sut.Move(b3, b4);
        Sut.Pieces[b4].CheckMove(b6, Sut).Validity.Should().Be(
            PawnsCanOnlyMove1SquareForwardOr2SquaresForwardOnTheFirstMove,
            "the pawn has already moved");
    }

    [Fact(DisplayName = "Pawns cannot move diagonally")]
    public void pawns_cannot_move_diagonally()
    {
        Sut.Board.Place(WhiteKing, f1);
        Sut.Board.Place(WhitePawn, a3);

        Sut.Pieces[a3].CheckMove(b3, Sut).Validity.Should().Be(PawnsCannotMoveHorizontally);
    }

    [Fact(DisplayName = "White pawns can be promoted upon reaching the opposite end of the board")]
    public void pawns_can_be_promoted_upon_reaching_the_opposite_end_of_the_board()
    {
        Sut.Board.Place(BlackKing, e8);
        Sut.Board.Place(WhiteKing, e1);
        Sut.Board.Place(WhitePawn, f7);
        Sut.Board.Place(BlackPawn, f2);

        Sut.Move(f7, f8);
        Sut.Board[f8].Should().Be(WhiteQueen, "white pawns are promoted to queens");
        Sut.Move(f2, f1);
        Sut.Board[f1].Should().Be(BlackQueen, "black pawns are promoted to queens");
    }

    [Fact(DisplayName = "Pawns cannot take a piece to the side and 2 squares forward")]
    public void pawns_cannot_take_a_piece_to_the_side_and_2_squares_forward()
    {
        var text = """
                   █░█░k░█░
                   ░█░█░█░█
                   █░█░█░█░
                   ░█░█░█░█
                   █p█░█░█░
                   ░█░█░█░█
                   P░█░█░█░
                   ░█░█K█░█
                   """;
        Sut.Board.Set(text);

        Sut.Pieces[a2].CheckMove(b4, Sut).Validity.Should().Be(PawnsCanOnlySideStep1SquareWhenCapturing);
    }

    [Fact(DisplayName = "Pawns cannot take a piece forwards")]
    public void pawns_cannot_take_a_piece_forwards()
    {
        var text = """
                   █░█░k░█░
                   ░█░█░█░█
                   █░█░█░█░
                   ░█░█░█░█
                   █░█░█░█░
                   p█░█░█░█
                   P░█░█░█░
                   ░█░█K█░█
                   """;
        Sut.Board.Set(text);

        Sut.Pieces[a2].CheckMove(a3, Sut).Validity.Should().Be(PawnsCannotTakeForward);
    }
}