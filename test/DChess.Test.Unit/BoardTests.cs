﻿using DChess.Core;
using static DChess.Core.PieceColour;
using static DChess.Core.PieceType;

namespace DChess.Test.Unit;

public class BoardTests
{
    private readonly Piece _whitePawn = new(Pawn, White);

    [Fact(DisplayName = "The board should be displayed correctly")]
    public void the_board_should_be_displayed_correctly()
    {
        // Arrange
        var board = new Board();
        var renderer = new BoardRenderer();

        // Assert
        renderer.Render(board);
        renderer.LastRender.Should().BeEquivalentTo(
            """
            █░█░█░█░
            ░█░█░█░█
            █░█░█░█░
            ░█░█░█░█
            █░█░█░█░
            ░█░█░█░█
            █░█░█░█░
            ░█░█░█░█
            """);
    }

    [Fact(DisplayName = "A standard  board should be displayed correctly with pieces")]
    public void a_standard_board_should_be_displayed_correctly_with_pieces()
    {
        // Arrange
        var board = new Board();
        board.SetStandardLayout();
        var renderer = new BoardRenderer();

        renderer.Render(board);
        renderer.LastRender.Should().BeEquivalentTo(
            """
            ♖♘♗♕♔♗♘♖
            ♙♙♙♙♙♙♙♙
            █░█░█░█░
            ░█░█░█░█
            █░█░█░█░
            ░█░█░█░█
            ♟♟♟♟♟♟♟♟
            ♜♞♝♛♚♝♞♜
            """);
    }

    [Theory(DisplayName = "An invalid position should throw an exception")]
    [InlineData("a")]
    [InlineData("a11")]
    [InlineData("i1")]
    [InlineData("a0")]
    [InlineData("a9")]
    [InlineData("1a")]
    [InlineData("9a")]
    public void an_invalid_position_should_throw_an_exception(string positionName)
    {
        // Arrange
        Action act = () => new Coordinate(positionName);
        // Assert
        act.Should().Throw<ArgumentException>();
    }


    [Fact(DisplayName = "A position can be described by a file and rank")]
    public void a_position_can_be_described_by_a_file_and_rank()
    {
        // Arrange
        var position = new Coordinate("a1");

        // Assert
        position.File.Should().Be('a');
        position.Rank.Should().Be(1);
    }

    [Fact(DisplayName = "A piece can be placed on the board")]
    public void a_piece_can_be_placed_on_the_board()
    {
        // Arrange
        var board = new Board
        {
            ['a', 1] =
            {
                Piece = _whitePawn
            }
        };

        // Assert
        board['a', 1].Should().BeEquivalentTo(new Cell(_whitePawn));
        board['a', 2].Should().BeEquivalentTo(new Cell(null));
    }

    [Fact(DisplayName = "If there are no pieces on the board, a cell's piece is null")]
    public void if_there_are_no_pieces_on_the_board_a_cells_piece_is_null()
    {
        // Arrange
        var board = new Board();

        // Act
        var cell = board['a', 1];
        var piece = cell.Piece;

        // Assert
        piece.Should().BeNull();
    }

    [Theory(DisplayName = "A piece cannot be placed outside the board")]
    [InlineData('a', -1)]
    [InlineData('a', 0)]
    [InlineData('a', 9)]
    [InlineData('i', 1)]
    [InlineData('1', 1)]
    public void a_piece_cannot_be_placed_outside_the_board(char column, int row)
    {
        // Arrange
        var board = new Board();

        // Act
        Action act = () => board[column, row].Piece = _whitePawn;

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact(DisplayName = "Cell shorthand can be used to get a cell")]
    public void cell_shorthand_can_be_used_to_get_a_cell()
    {
        // Arrange
        var board = new Board();
        board['a', 1].Piece = _whitePawn;

        // Assert
        board["a1"].Piece.Should().Be(_whitePawn);
    }

    [Theory(DisplayName = "Board can be created with a standard piece layout")]
    [InlineData("a8", Rook, Black)]
    [InlineData("b8", Knight, Black)]
    [InlineData("c8", Bishop, Black)]
    [InlineData("d8", Queen, Black)]
    [InlineData("e8", King, Black)]
    [InlineData("f8", Bishop, Black)]
    [InlineData("g8", Knight, Black)]
    [InlineData("h8", Rook, Black)]
    [InlineData("a7", Pawn, Black)]
    [InlineData("b7", Pawn, Black)]
    [InlineData("c7", Pawn, Black)]
    [InlineData("d7", Pawn, Black)]
    [InlineData("e7", Pawn, Black)]
    [InlineData("f7", Pawn, Black)]
    [InlineData("g7", Pawn, Black)]
    [InlineData("h7", Pawn, Black)]
    [InlineData("a2", Pawn, White)]
    [InlineData("b2", Pawn, White)]
    [InlineData("c2", Pawn, White)]
    [InlineData("d2", Pawn, White)]
    [InlineData("e2", Pawn, White)]
    [InlineData("f2", Pawn, White)]
    [InlineData("g2", Pawn, White)]
    [InlineData("h2", Pawn, White)]
    [InlineData("a1", Rook, White)]
    [InlineData("b1", Knight, White)]
    [InlineData("c1", Bishop, White)]
    [InlineData("d1", Queen, White)]
    [InlineData("e1", King, White)]
    [InlineData("f1", Bishop, White)]
    [InlineData("g1", Knight, White)]
    [InlineData("h1", Rook, White)]
    public void board_can_be_created_with_a_standard_piece_layout(string position, PieceType type, PieceColour colour)
    {
        // Arrange
        var board = new Board();
        board.SetStandardLayout();

        // Assert
        board[position].Piece.Should().Be(new Piece(type, colour));
    }
}