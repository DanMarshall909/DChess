using System.Diagnostics.CodeAnalysis;
using DChess.Core.Errors;
using DChess.Core.Game;
using static DChess.Core.Game.Colour;
using static DChess.Core.Game.Piece.Kind;

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
        properties.Should().BeEquivalentTo(WhitePawn, "the piece should be a white pawn", Sut,
            "Piece Placement Visualization");
        Sut.Board.HasPieceAt(a2).Should().BeFalse("a2 should be empty", Sut, "Empty Square Visualization");
    }

    [Fact(DisplayName = "If there are no pieceAttributes on the board, a cell's pieceAttributes is null")]
    public void if_there_are_no_pieces_on_the_board_a_cells_piece_is_null()
    {
        Sut.Board.HasPieceAt(a1).Should().BeFalse("the cell should be empty", Sut, "Empty Board Visualization");
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
    public void board_can_be_created_with_a_standard_piece_layout(string squareString, Piece.Kind kind,
        Colour colour)
    {
        Sut.Board.SetStandardLayout();

        var square = new Square(squareString);
        Sut.Board[square].Should().BeEquivalentTo(new PieceAttributes(colour, kind));
    }

    [Fact(DisplayName = "A piece can be added to the board")]
    public void a_piece_can_be_added_to_the_board()
    {
        Sut.Board.Place(WhitePawn, b2);
        Sut.Board[b2].Should().Be(WhitePawn, "the piece should be a white pawn", Sut, "Piece Addition Visualization");
    }

    [Fact(DisplayName = "A new invalidMoveHandler starts with white as the current player")]
    public void a_new_game_starts_with_white_as_the_current_player()
    {
        Sut.CurrentPlayer.Should().Be(White, "white should be the starting player", Sut, "New Game Visualization");
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
        Sut.Set("k7/8/1P6/8/8/8/8/K7 w - - 0 1");
        Sut.Status(White).Should().Be(InPlay, "white should be in play", Sut, "Initial Position Visualization");

        Sut.Move(b6, b7);
        Sut.Status(Black).Should().Be(Check, "black king should be in check", Sut, "Check Position Visualization");
    }

    [Fact(DisplayName = "Game state reflects king in checkmate")]
    public void game_state_reflects_king_in_checkmate()
    {
        Sut.Set("1r3k2/8/8/8/8/8/2q5/5K2 w - - 0 1");
        Sut.Status(White).Should().Be(InPlay, "white should be in play", Sut, "Pre-Checkmate Visualization");

        Sut.Move(b8, b1);
        Sut.Status(White).Should().Be(Checkmate, "white king should be in checkmate", Sut, "Checkmate Visualization");
    }

    [Theory(DisplayName = "Friendly pieces can be found")]
    [InlineData("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1",
        new[]
        {
            Rook, Knight, Bishop, King, Queen, Bishop, Knight, Rook, Pawn, Pawn, Pawn, Pawn, Pawn, Pawn, Pawn, Pawn
        })]
    [InlineData("k7/p7/KPP5/8/8/8/8/8 w - - 0 1", new[] { King, Pawn, Pawn })]
    [InlineData("k7/p7/K1P5/8/8/8/8/8 w - - 0 1", new[] { King, Pawn })]
    public void friendly_pieces_can_be_found(string fenString, Piece.Kind[] pieces)
    {
        Sut.Set(fenString);
        Sut.FriendlyPieces(White).ToList().Select(x => x.Kind).Should().BeEquivalentTo(pieces);
    }
}