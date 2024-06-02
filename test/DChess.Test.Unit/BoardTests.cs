using DChess.Core.Board;
using DChess.Core.Exceptions;

namespace DChess.Test.Unit;

public class BoardTests
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();
    private static ChessPiece WhitePawn => new(PieceType.Pawn, Colour.White);

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
        // ReSharper disable once ObjectCreationAsStatement
#pragma warning disable CA1806
        Action act = () => new Coordinate(positionName);
#pragma warning restore CA1806
        // Assert
        act.Should().Throw<InvalidCoordinateException>();
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
        IInvalidMoveHandler a = new TestInvalidMoveHandler();
        var board = new Board(a);

        board[a1] = WhitePawn;

        // Assert
        board[a1].Should().BeEquivalentTo(WhitePawn);
        board.HasPieceAt(a2).Should().BeFalse();
    }

    [Fact(DisplayName = "If there are no piece on the board, a cell's piece is null")]
    public void if_there_are_no_pieces_on_the_board_a_cells_piece_is_null()
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);

        // Act

        // Assert
        board.HasPieceAt(a1).Should().BeFalse();
    }

    [Theory(DisplayName = "A piece cannot be placed outside the board")]
    [InlineData('a', 254)]
    [InlineData('a', 0)]
    [InlineData('a', 9)]
    [InlineData('i', 1)]
    [InlineData('1', 1)]
    public void a_piece_cannot_be_placed_outside_the_board(char column, byte row)
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);

        // Act
        Action act = () => board[new Coordinate(column, row)] = WhitePawn;

        // Assert
        act.Should().Throw<InvalidCoordinateException>();
    }

    [Fact(DisplayName = "Cell shorthand can be used to get a cell")]
    public void cell_shorthand_can_be_used_to_get_a_cell()
    {
        // Arrange
        var testHandlier = new TestInvalidMoveHandler();
        var board = new Board(_invalidMoveHandler);
        board[a1] = WhitePawn;

        // Assert
        board["a1"].Should().BeEquivalentTo(WhitePawn);
    }

    [Theory(DisplayName = "Board can be created with a standard piece layout")]
    [InlineData("a8", PieceType.Rook, Colour.Black)]
    [InlineData("b8", PieceType.Knight, Colour.Black)]
    [InlineData("c8", PieceType.Bishop, Colour.Black)]
    [InlineData("d8", PieceType.Queen, Colour.Black)]
    [InlineData("e8", PieceType.King, Colour.Black)]
    [InlineData("f8", PieceType.Bishop, Colour.Black)]
    [InlineData("g8", PieceType.Knight, Colour.Black)]
    [InlineData("h8", PieceType.Rook, Colour.Black)]
    [InlineData("a7", PieceType.Pawn, Colour.Black)]
    [InlineData("b7", PieceType.Pawn, Colour.Black)]
    [InlineData("c7", PieceType.Pawn, Colour.Black)]
    [InlineData("d7", PieceType.Pawn, Colour.Black)]
    [InlineData("e7", PieceType.Pawn, Colour.Black)]
    [InlineData("f7", PieceType.Pawn, Colour.Black)]
    [InlineData("g7", PieceType.Pawn, Colour.Black)]
    [InlineData("h7", PieceType.Pawn, Colour.Black)]
    [InlineData("a2", PieceType.Pawn, Colour.White)]
    [InlineData("b2", PieceType.Pawn, Colour.White)]
    [InlineData("c2", PieceType.Pawn, Colour.White)]
    [InlineData("d2", PieceType.Pawn, Colour.White)]
    [InlineData("e2", PieceType.Pawn, Colour.White)]
    [InlineData("f2", PieceType.Pawn, Colour.White)]
    [InlineData("g2", PieceType.Pawn, Colour.White)]
    [InlineData("h2", PieceType.Pawn, Colour.White)]
    [InlineData("a1", PieceType.Rook, Colour.White)]
    [InlineData("b1", PieceType.Knight, Colour.White)]
    [InlineData("c1", PieceType.Bishop, Colour.White)]
    [InlineData("d1", PieceType.Queen, Colour.White)]
    [InlineData("e1", PieceType.King, Colour.White)]
    [InlineData("f1", PieceType.Bishop, Colour.White)]
    [InlineData("g1", PieceType.Knight, Colour.White)]
    [InlineData("h1", PieceType.Rook, Colour.White)]
    public void board_can_be_created_with_a_standard_piece_layout(string coordinateString, PieceType type,
        Colour colour)
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);
        board.SetStandardLayout();

        // Assert
        board[coordinateString].Should().BeEquivalentTo(new ChessPiece(type, colour));
    }

    [Fact(DisplayName = "A piece can be added to the board")]
    public void a_piece_can_be_added_to_the_board()
    {
        var board = new Board(_invalidMoveHandler);
        var whitePawn = WhitePawn;
        board[a1] = whitePawn;

        board[a1].Should().Be(whitePawn);
    }
}