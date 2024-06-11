using DChess.Core.Game;

namespace DChess.Test.Unit.Rules.Pieces;

public class PawnTests : GameTestBase
{
    [Fact(DisplayName = "Pawns can move forward one square")]
    public void pawn_can_move_forward_one_square()
    {
        Game.GameState.Place(WhiteKing, f1);
        Game.GameState.Place(WhitePawn, a1);

        Game.GameState.Pieces[a1].MoveTo(b1);
    }

    [Fact(DisplayName = "Pawns can only move forward two squares from starting position")]
    public void pawn_can_move_forward_two_squares_from_starting_position()
    {
        Game.GameState.Place(WhiteKing, f1);
        Game.GameState.Place(WhitePawn, b2);

        Game.GameState.Pieces[b2].MoveTo(b3);
        Game.GameState.Pieces[b3].MoveTo(b4);
        Game.GameState.Pieces[b4].CheckMove(b6).Validity.Should().Be(PawnsCanOnlyMove2SquaresForwardFromStartingPosition,
            "the pawn has already moved");
    }

    [Fact(DisplayName = "Pawns cannot move diagonally")]
    public void pawns_cannot_move_diagonally()
    {
        Game.GameState.Place(WhiteKing, f1);
        Game.GameState.Place(WhitePawn, a3);

        Game.GameState.Pieces[a3].CheckMove(b3).Validity.Should().Be(PawnsCannotMoveHorizontally);
    }

    [Fact(DisplayName = "White pawns can be promoted upon reaching the opposite end of the board")]
    public void pawns_can_be_promoted_upon_reaching_the_opposite_end_of_the_board()
    {
        Game.GameState.Place(BlackKing, e8);
        Game.GameState.Place(WhiteKing, e1);
        for (byte rank = 1; rank <= 8; rank++)
        {
            var from = new Coordinate('g', rank);
            var to = new Coordinate('h', rank);

            Game.GameState.Place(WhitePawn, from);
            
            Game.GameState.Pieces[from].MoveTo(to);
            var chessPiece = Game.GameState.GetProperties(to);
            chessPiece.Type.Should().Be(PieceType.Queen, "white pawns are promoted to Queens");
            chessPiece.Colour.Should().Be(Colour.White, "white pawns are promoted to Queens of the same colour");

            from = new Coordinate('b', rank);
            to = new Coordinate('a', rank);

            Game.GameState.Place(BlackPawn, from);
            Game.GameState.Pieces[from].MoveTo(to);
            chessPiece = Game.GameState.GetProperties(to);
            chessPiece.Type.Should().Be(PieceType.Queen, "black pawns are promoted to Queens");
            chessPiece.Colour.Should().Be(Colour.Black, "black pawns are promoted to Queens of the same colour");
        }
    }
}