using System.Diagnostics.CodeAnalysis;
using DChess.Core.Exceptions;
using DChess.Core.Game;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

[SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
public class GameTests: GameTestBase
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();

    [Theory(DisplayName = "An invalid position should throw an exception")]
    [InlineData("a")]
    [InlineData("a11")]
    [InlineData("i1")]
    [InlineData("a0")]
    [InlineData("a9")]
    [InlineData("1a")]
    [InlineData("9a")]
    public void an_invalid_position_should_throw_an_exception(string coordinateString)
    {
        // ReSharper disable once ObjectCreationAsStatement
#pragma warning disable CA1806
        Action act = () => new Coordinate(coordinateString);
#pragma warning restore CA1806
        act.Should().Throw<InvalidCoordinateException>();
    }


    [Fact(DisplayName = "A position can be described by a file and rank")]
    public void a_position_can_be_described_by_a_file_and_rank()
    {
        var position = new Coordinate("a1");

        position.File.Should().Be('a');
        position.Rank.Should().Be(1);
    }

    [Fact(DisplayName = "A piece can be placed on the board")]
    public void a_piece_can_be_placed_on_the_board()
    {
        var game = base.Sut;

        game.GameState.Place(WhitePawn, a1);

        var properties = game.GameState.GetProperties(a1);
        properties.Should().BeEquivalentTo(WhitePawn);
        game.GameState.HasPieceAt(a2).Should().BeFalse();
    }

    [Fact(DisplayName = "If there are no properties on the board, a cell's properties is null")]
    public void if_there_are_no_pieces_on_the_board_a_cells_piece_is_null()
    {
        Sut.GameState.HasPieceAt(a1).Should().BeFalse();
    }

    [Theory(DisplayName = "A properties cannot be placed outside the board")]
    [InlineData('a', 254)]
    [InlineData('a', 0)]
    [InlineData('a', 9)]
    [InlineData('i', 1)]
    [InlineData('1', 1)]
    public void a_piece_cannot_be_placed_outside_the_board(char column, byte row)
    {
        var act = () => Sut.GameState.Place(WhitePawn, new Coordinate(column, row));

        act.Should().Throw<InvalidCoordinateException>();
    }

    [Theory(DisplayName = "Board can be created with a standard properties layout")]
    [InlineData("a8", PieceType.Rook, Black)]
    [InlineData("b8", PieceType.Knight, Black)]
    [InlineData("c8", PieceType.Bishop, Black)]
    [InlineData("d8", PieceType.Queen, Black)]
    [InlineData("e8", PieceType.King, Black)]
    [InlineData("f8", PieceType.Bishop, Black)]
    [InlineData("g8", PieceType.Knight, Black)]
    [InlineData("h8", PieceType.Rook, Black)]
    [InlineData("a7", PieceType.Pawn, Black)]
    [InlineData("b7", PieceType.Pawn, Black)]
    [InlineData("c7", PieceType.Pawn, Black)]
    [InlineData("d7", PieceType.Pawn, Black)]
    [InlineData("e7", PieceType.Pawn, Black)]
    [InlineData("f7", PieceType.Pawn, Black)]
    [InlineData("g7", PieceType.Pawn, Black)]
    [InlineData("h7", PieceType.Pawn, Black)]
    [InlineData("a2", PieceType.Pawn, White)]
    [InlineData("b2", PieceType.Pawn, White)]
    [InlineData("c2", PieceType.Pawn, White)]
    [InlineData("d2", PieceType.Pawn, White)]
    [InlineData("e2", PieceType.Pawn, White)]
    [InlineData("f2", PieceType.Pawn, White)]
    [InlineData("g2", PieceType.Pawn, White)]
    [InlineData("h2", PieceType.Pawn, White)]
    [InlineData("a1", PieceType.Rook, White)]
    [InlineData("b1", PieceType.Knight, White)]
    [InlineData("c1", PieceType.Bishop, White)]
    [InlineData("d1", PieceType.Queen, White)]
    [InlineData("e1", PieceType.King, White)]
    [InlineData("f1", PieceType.Bishop, White)]
    [InlineData("g1", PieceType.Knight, White)]
    [InlineData("h1", PieceType.Rook, White)]
    public void board_can_be_created_with_a_standard_piece_layout(string coordinateString, PieceType type,
        Colour colour)
    {
        Sut.SetStandardLayout();

        Sut.GameState.GetProperties(new Coordinate(coordinateString)).Should().BeEquivalentTo(new Properties(type, colour));
    }

    [Fact(DisplayName = "A properties can be added to the board")]
    public void a_piece_can_be_added_to_the_board()
    {;
        Sut.GameState.Place(WhitePawn, a1);
        Sut.GameState.GetProperties(a1).Should().Be(WhitePawn);
    }
    
    [Fact(DisplayName = "A new invalidMoveHandler starts with white as the current player")]
    public void a_new_game_starts_with_white_as_the_current_player()
    {
        Sut.CurrentPlayer.Should().Be(White);
    }
    
    [Fact (DisplayName = "After takiing a turn the current player changes")]
    public void after_taking_a_turn_the_current_player_changes()
    {
        Sut.SetStandardLayout();
        Sut.Move(a2, a4);
        Sut.CurrentPlayer.Should().Be(Black);
    }
}