using DChess.Core.Board;

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
        Board.Pieces[d1].CheckMove(f1).Message.Should().BeNull("the pawn has already moved");

        Board[f2] = BlackPawn;
        Board.Pieces[f2].MoveTo(d2);
        Board.Pieces[d2].CheckMove(b2).Message.Should().BeNull("the pawn has already moved");
    }

    [Fact(DisplayName = "Pawns can take pieces diagonally")]
    public void pawns_can_take_pieces_diagonally()
    {
        Board[a1] = WhitePawn;
        Board[b2] = BlackPawn;
        Board[d3] = BlackPawn;
        Board.Pieces[a1].MoveTo(b2);
        // cannot move diagonally without a piece to take
        Board.Pieces[b2].CheckMove(c3).Message.Should().NotBeEmpty("there is no piece to take");
        // cannot move more than 1 square horizontally while taking a piece
        var moveResult = Board.Pieces[b2].CheckMove(d3);
        moveResult.Message.Should().BeNull("pawns can only move 1 square diagonally when capturing");
    }
    
    [Fact(DisplayName = "White pawns can be promoted upon reaching the opposite end of the board")]
    public void pawns_can_be_promoted_upon_reaching_the_opposite_end_of_the_board()
    {
        foreach (byte rank in Board.Ranks)
        {
            var from = new Coordinate('g', rank);
            var to = new Coordinate('h', rank);
            
            Board[from] = WhitePawn;
            Board.Pieces[from].MoveTo(to);
            var chessPiece = Board[to];
            chessPiece.Type.Should().Be(PieceType.Queen, "white pawns are promoted to Queens");
            chessPiece.Colour.Should().Be(Colour.White, "white pawns are promoted to Queens of the same colour");
            
            from = new Coordinate('b', rank);
            to = new Coordinate('a', rank);
            
            Board[from] = BlackPawn;
            Board.Pieces[from].MoveTo(to);
            chessPiece = Board[to];
            chessPiece.Type.Should().Be(PieceType.Queen, "black pawns are promoted to Queens");
            chessPiece.Colour.Should().Be(Colour.Black, "black pawns are promoted to Queens of the same colour");
        }
    }
}