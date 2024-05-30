namespace DChess.Test.Unit.Rules.Pieces;

public class PawnTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    [Fact(DisplayName = "Pawns can move forward one square")]
    public void pawn_can_move_forward_one_square()
    {
        Board[a1] = WhitePawn;

        Board.Pieces[a1].MoveTo(b1);
    }

    [Fact(DisplayName = "Pawns can only move forward two squares from starting position")]
    public void pawn_can_move_forward_two_squares_from_starting_position()
    {
        Board[b1] = WhitePawn;

        Board.Pieces[b1].MoveTo(c1);
        Board.Pieces[c1].MoveTo(d1);
        Board.Pieces[d1].CheckMove(f1).Valid.Should().BeFalse("The pawn has already moved");

        Board[f2] = BlackPawn;
        Board.Pieces[f2].MoveTo(d2);
        Board.Pieces[d2].CheckMove(b2).Valid.Should().BeFalse("The pawn has already moved");
    }

    [Fact(DisplayName = "Pawns can take pieces diagonally")]
    public void pawns_can_take_pieces_diagonally()
    {
        Board[a1] = WhitePawn;
        Board[b2] = BlackPawn;
        Board[d3] = BlackPawn;
        Board.Pieces[a1].MoveTo(b2);
        // cannot move diagonally without a piece to take
        Board.Pieces[b2].CheckMove(c3).Valid.Should().BeFalse("There is no piece to take");
        // cannot move more than 1 square horizontally while taking a piece
        var moveResult = Board.Pieces[b2].CheckMove(d3);
        moveResult.Valid.Should().BeFalse("Can only move one square diagonally");
    }
}