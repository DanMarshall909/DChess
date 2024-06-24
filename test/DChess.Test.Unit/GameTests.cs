using System.Diagnostics.CodeAnalysis;
using DChess.Core.Errors;
using DChess.Core.Game;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

[SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
public class GameTests : GameTestBase
{
    [Theory(DisplayName = "An invalid position should throw an exception")]
    [InlineData("a")]
    [InlineData("a11")]
    [InlineData("i1")]
    [InlineData("a0")]
    [InlineData("a9")]
    [InlineData("1a")]
    [InlineData("9a")]
    public void an_invalid_position_should_throw_an_exception(string squareString)
    {
        // ReSharper disable once ObjectCreationAsStatement
#pragma warning disable CA1806
        Action act = () => new Square(squareString);
#pragma warning restore CA1806
        act.Should().Throw<InvalidSquareException>();
    }

    [Fact(DisplayName = "A position can be described by a file and rank")]
    public void a_position_can_be_described_by_a_file_and_rank()
    {
        var position = new Square("a1");

        position.File.Should().Be('a');
        position.Rank.Should().Be(1);
    }

    [Fact(DisplayName = "A piece can be placed on the board")]
    public void a_piece_can_be_placed_on_the_board()
    {
        Sut.Board.Place(WhitePawn, a1);

        var properties = Sut.Board[a1];
        properties.Should().BeEquivalentTo(WhitePawn);
        Sut.Board.HasPieceAt(a2).Should().BeFalse();
    }

    [Fact(DisplayName = "If there are no pieceAttributes on the board, a cell's pieceAttributes is null")]
    public void if_there_are_no_pieces_on_the_board_a_cells_piece_is_null()
    {
        Sut.Board.HasPieceAt(a1).Should().BeFalse();
    }

    [Theory(DisplayName = "A piece cannot be placed outside the board")]
    [InlineData('a', 254)]
    [InlineData('a', 0)]
    [InlineData('a', 9)]
    [InlineData('i', 1)]
    [InlineData('1', 1)]
    public void a_piece_cannot_be_placed_outside_the_board(char column, byte row)
    {
        var act = () => Sut.Board.Place(WhitePawn, new Square(column, row));

        act.Should().Throw<InvalidSquareException>();
    }

    [Theory(DisplayName = "Board can be created with a standard pieceAttributes layout")]
    [InlineData("a8", Piece.Kind.Rook, Black)]
    [InlineData("b8", Piece.Kind.Knight, Black)]
    [InlineData("c8", Piece.Kind.Bishop, Black)]
    [InlineData("d8", Piece.Kind.Queen, Black)]
    [InlineData("e8", Piece.Kind.King, Black)]
    [InlineData("f8", Piece.Kind.Bishop, Black)]
    [InlineData("g8", Piece.Kind.Knight, Black)]
    [InlineData("h8", Piece.Kind.Rook, Black)]
    [InlineData("a7", Piece.Kind.Pawn, Black)]
    [InlineData("b7", Piece.Kind.Pawn, Black)]
    [InlineData("c7", Piece.Kind.Pawn, Black)]
    [InlineData("d7", Piece.Kind.Pawn, Black)]
    [InlineData("e7", Piece.Kind.Pawn, Black)]
    [InlineData("f7", Piece.Kind.Pawn, Black)]
    [InlineData("g7", Piece.Kind.Pawn, Black)]
    [InlineData("h7", Piece.Kind.Pawn, Black)]
    [InlineData("a2", Piece.Kind.Pawn, White)]
    [InlineData("b2", Piece.Kind.Pawn, White)]
    [InlineData("c2", Piece.Kind.Pawn, White)]
    [InlineData("d2", Piece.Kind.Pawn, White)]
    [InlineData("e2", Piece.Kind.Pawn, White)]
    [InlineData("f2", Piece.Kind.Pawn, White)]
    [InlineData("g2", Piece.Kind.Pawn, White)]
    [InlineData("h2", Piece.Kind.Pawn, White)]
    [InlineData("a1", Piece.Kind.Rook, White)]
    [InlineData("b1", Piece.Kind.Knight, White)]
    [InlineData("c1", Piece.Kind.Bishop, White)]
    [InlineData("d1", Piece.Kind.Queen, White)]
    [InlineData("e1", Piece.Kind.King, White)]
    [InlineData("f1", Piece.Kind.Bishop, White)]
    [InlineData("g1", Piece.Kind.Knight, White)]
    [InlineData("h1", Piece.Kind.Rook, White)]
    public void board_can_be_created_with_a_standard_piece_layout(string squareString, Piece.Kind kind,
        Colour colour)
    {
        Sut.Board.SetStandardLayout();

        var square = new Square(squareString);
        Sut.Board[square].Should().BeEquivalentTo(new PieceAttributes(kind, colour));
    }

    [Fact(DisplayName = "A piece can be added to the board")]
    public void a_piece_can_be_added_to_the_board()
    {
        Sut.Board.Place(WhitePawn, b2);
        Sut.Board[b2].Should().Be(WhitePawn);
    }

    [Fact(DisplayName = "A new invalidMoveHandler starts with white as the current player")]
    public void a_new_game_starts_with_white_as_the_current_player()
    {
        Sut.CurrentPlayer.Should().Be(White);
    }

    [Fact(DisplayName = "After taking a turn the current player changes")]
    public void after_taking_a_turn_the_current_player_changes()
    {
        Sut.Board.SetStandardLayout();
        Sut.Move(a2, a4);
        Sut.CurrentPlayer.Should().Be(Black);
    }

    [Fact(DisplayName = "After taking two turns the current player changes back to white")]
    public void after_taking_two_turns_the_current_player_changes_back_to_white()
    {
        Sut.Board.SetStandardLayout();
        Sut.Move(a2, a4);
        Sut.Move(a7, a6);
        Sut.CurrentPlayer.Should().Be(White);
    }

    [Fact(DisplayName = "A player can only move their own pieces")]
    public void a_player_can_only_move_their_own_pieces()
    {
        Sut.Board.SetStandardLayout();
        Sut.Pieces[a7].CheckMove(a5, Sut).Validity.Should().Be(CannotMoveOpponentsPiece);
    }

    [Fact(DisplayName = "Legal moves are detected")]
    public void legal_moves_are_detected()
    {
        Sut.Board.Place(WhiteKing, f1);
        Sut.Board.Place(BlackQueen, a2);
        Sut.Board.Place(BlackKing, f8);
        MoveHandler.HasLegalMoves(White, Sut).Should().BeTrue("White king can move to e1 or g1");
    }

    [Fact(DisplayName = "Game state reflects king in check")]
    public void game_state_reflects_king_in_check()
    {
        Sut.Board.Place(WhiteKing, f1);
        Sut.Board.Place(BlackQueen, a2);
        Sut.Status(White).Should().Be(InPlay);

        Sut.CurrentPlayer = Black;
        Sut.Move(a2, a6);
        Sut.Status(White).Should().Be(Check);
    }

    [Fact(DisplayName = "Game state reflects king in checkmate")]
    public void game_state_reflects_king_in_checkmate()
    {
        Sut.Board.Place(WhiteKing, f1);
        Sut.Board.Place(BlackKing, f8);
        Sut.Board.Place(BlackQueen, c2);
        Sut.Board.Place(BlackRook, b8);
        Sut.Status(White).Should().Be(InPlay);

        Sut.Move(b8, b1);
        Sut.Status(White).Should().Be(Checkmate);
    }
}