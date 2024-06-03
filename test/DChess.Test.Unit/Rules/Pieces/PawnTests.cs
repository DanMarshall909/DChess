using DChess.Core.Board;

namespace DChess.Test.Unit.Rules.Pieces;

public class PawnTests : BoardTestBase
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

        Board.Pieces[b1].MoveTo(b2);
        Board.Pieces[b2].MoveTo(b3);
        Board.Pieces[b3].CheckMove(b5).Validity.Should().Be(PawnsCanOnlyMove2SquaresForwardFromStartingPosition,
            "the pawn has already moved");

        Board[b6] = BlackPawn;
        Board.Pieces[b6].MoveTo(b5);
        Board.Pieces[b5].CheckMove(b3).Validity.Should().Be(PawnsCanOnlyMove2SquaresForwardFromStartingPosition,
            "the pawn has already moved");
    }

    [Fact(DisplayName = "Pawns cannot move diagonally")]
    public void pawns_cannot_move_diagonally()
    {
        Board.Pieces.Should().BeEmpty();
        Board[a3] = WhitePawn;

        Board.Pieces[a3].CheckMove(b3).Validity.Should().Be(PawnsCannotMoveHorizontally);
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