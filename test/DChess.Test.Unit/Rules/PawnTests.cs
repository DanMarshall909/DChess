using DChess.Core.Pieces;

namespace DChess.Test.Unit.Rules;

public class PawnTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    [Fact(DisplayName = "Pawns can move forward one square")]
    public void pawn_can_move_forward_one_square()
    {
        Board[a1] = ChessPiece.WhitePawn;

        Board.Pieces[a1].MoveTo(b1);
    }
    
    
    [Fact(DisplayName = "Pawns can only move forward two squares from starting position")]
    public void pawn_can_move_forward_two_squares_from_starting_position()
    {
        Board[a1] = ChessPiece.WhitePawn;

        Board.Pieces[a1].MoveTo(c1);
        var result = Board.Pieces[c1].CheckMove(c1);
    }
}