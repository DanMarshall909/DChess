﻿using DChess.Core.Board;

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
    
    [Fact(DisplayName = "White pawns can be promoted upon reaching the opposite end of the board")]
    public void pawns_can_be_promoted_upon_reaching_the_opposite_end_of_the_board()
    {
        foreach (char file in Board.Files)
        {
            var from = new Coordinate(file, 7);
            var to = new Coordinate(file, 8);
            
            Board[from] = WhitePawn;
            Board.Pieces[from].MoveTo(to);
            var chessPiece = Board[to];
            chessPiece.Type.Should().Be(PieceType.Queen, "White pawns are promoted to Queens");
            chessPiece.Colour.Should().Be(Colour.White, "White pawns are promoted to Queens of the same colour");
            
            from = new Coordinate(file, 2);
            to = new Coordinate(file, 1);
            
            Board[from] = BlackPawn;
            Board.Pieces[from].MoveTo(to);
            chessPiece = Board[to];
            chessPiece.Type.Should().Be(PieceType.Queen, "Black pawns are promoted to Queens");
            chessPiece.Colour.Should().Be(Colour.Black, "Black pawns are promoted to Queens of the same colour");
        }
    }
}