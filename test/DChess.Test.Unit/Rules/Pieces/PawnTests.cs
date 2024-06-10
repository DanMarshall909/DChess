using DChess.Core.Game;

namespace DChess.Test.Unit.Rules.Pieces;

public class PawnTests : PieceTestBase
{
    [Fact(DisplayName = "Pawns can move forward one square")]
    public void pawn_can_move_forward_one_square()
    {
        Game.GameState.Set(a1, WhitePawn);

        Game.GameState.Pieces[a1].MoveTo(b1);
    }

    [Fact(DisplayName = "Pawns can only move forward two squares from starting position")]
    public void pawn_can_move_forward_two_squares_from_starting_position()
    {
        Game.GameState.Set(b1, WhitePawn);

        Game.GameState.Pieces[b1].MoveTo(b2);
        Game.GameState.Pieces[b2].MoveTo(b3);
        Game.GameState.Pieces[b3].CheckMove(b5).Validity.Should().Be(PawnsCanOnlyMove2SquaresForwardFromStartingPosition,
            "the pawn has already moved");

        Game.GameState.Set(b6, BlackPawn);
        Game.GameState.Pieces[b6].MoveTo(b5);
        Game.GameState.Pieces[b5].CheckMove(b3).Validity.Should().Be(PawnsCanOnlyMove2SquaresForwardFromStartingPosition,
            "the pawn has already moved");
    }

    [Fact(DisplayName = "Pawns cannot move diagonally")]
    public void pawns_cannot_move_diagonally()
    {
        Game.GameState.Pieces.Should().BeEmpty();
        Game.GameState.Set(a3, WhitePawn);

        Game.GameState.Pieces[a3].CheckMove(b3).Validity.Should().Be(PawnsCannotMoveHorizontally);
    }

    [Fact(DisplayName = "White pawns can be promoted upon reaching the opposite end of the board")]
    public void pawns_can_be_promoted_upon_reaching_the_opposite_end_of_the_board()
    {
        foreach (byte rank in Game.Ranks)
        {
            var from = new Coordinate('g', rank);
            var to = new Coordinate('h', rank);

            Game.GameState.Set(from, WhitePawn);
            Game.GameState.Pieces[from].MoveTo(to);
            var chessPiece = Game.GameState.GetProperties(to);
            chessPiece.Type.Should().Be(PieceType.Queen, "white pawns are promoted to Queens");
            chessPiece.Colour.Should().Be(Colour.White, "white pawns are promoted to Queens of the same colour");

            from = new Coordinate('b', rank);
            to = new Coordinate('a', rank);

            Game.GameState.Set(from, BlackPawn);
            Game.GameState.Pieces[from].MoveTo(to);
            chessPiece = Game.GameState.GetProperties(to);
            chessPiece.Type.Should().Be(PieceType.Queen, "black pawns are promoted to Queens");
            chessPiece.Colour.Should().Be(Colour.Black, "black pawns are promoted to Queens of the same colour");
        }
    }
}