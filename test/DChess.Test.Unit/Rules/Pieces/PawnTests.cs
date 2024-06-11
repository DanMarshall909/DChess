using DChess.Core.Game;

namespace DChess.Test.Unit.Rules.Pieces;

public class PawnTests : GameTestBase
{
    [Fact(DisplayName = "Pawns can move forward one square")]
    public void pawn_can_move_forward_one_square()
    {
        Sut.GameState.Place(WhiteKing, f1);
        Sut.GameState.Place(WhitePawn, a1);

        Sut.Move(a1, b1);
    }

    [Fact(DisplayName = "Pawns can only move forward two squares from starting position")]
    public void pawn_can_move_forward_two_squares_from_starting_position()
    {
        Sut.GameState.Place(WhiteKing, f1);
        Sut.GameState.Place(WhitePawn, b2);

        Sut.Move(b2, b3);
        Sut.Move(b3, b4);
        Sut.GameState.Pieces[b4].CheckMove(b6, Sut.GameState).Validity.Should().Be(PawnsCanOnlyMove2SquaresForwardFromStartingPosition,
            "the pawn has already moved");
    }

    [Fact(DisplayName = "Pawns cannot move diagonally")]
    public void pawns_cannot_move_diagonally()
    {
        Sut.GameState.Place(WhiteKing, f1);
        Sut.GameState.Place(WhitePawn, a3);

        Sut.GameState.Pieces[a3].CheckMove(b3, Sut.GameState).Validity.Should().Be(PawnsCannotMoveHorizontally);
    }

    [Fact(DisplayName = "White pawns can be promoted upon reaching the opposite end of the board")]
    public void pawns_can_be_promoted_upon_reaching_the_opposite_end_of_the_board()
    {
        Sut.GameState.Place(BlackKing, e8);
        Sut.GameState.Place(WhiteKing, e1);
        for (byte rank = 1; rank <= 8; rank++)
        {
            var from = new Coordinate('g', rank);
            var to = new Coordinate('h', rank);

            Sut.GameState.Place(WhitePawn, from);
            
            Sut.Move(from, to);
            var chessPiece = Sut.GameState.GetProperties(to);
            chessPiece.Type.Should().Be(PieceType.Queen, "white pawns are promoted to Queens");
            chessPiece.Colour.Should().Be(Colour.White, "white pawns are promoted to Queens of the same colour");

            from = new Coordinate('b', rank);
            to = new Coordinate('a', rank);

            Sut.GameState.Place(BlackPawn, from);
            Sut.Move(from, to);
            chessPiece = Sut.GameState.GetProperties(to);
            chessPiece.Type.Should().Be(PieceType.Queen, "black pawns are promoted to Queens");
            chessPiece.Colour.Should().Be(Colour.Black, "black pawns are promoted to Queens of the same colour");
        }
    }
}