using DChess.Core.Board;
using DChess.Core.Pieces;
using static DChess.Core.Board.Colour;

namespace DChess.Test.Unit;

public class PieceTests
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();

    [Fact(DisplayName = "A piece can be moved")]
    public void a_piece_can_be_moved()
    {
        // Arrange
        var board = new Game(_invalidMoveHandler);
        var chessPiece = new Properties(PieceType.Pawn, White);

        board.GameState.Set(a2, chessPiece);
        board.GameState.FriendlyPiecesByCoordinate(White).Count().Should().Be(1);

        // Act
        var piece = board.GameState.Pieces[a2];
        piece.MoveTo(b3);

        // Assert
        var args = new Piece.Arguments(chessPiece, b3, board, _invalidMoveHandler);
        board.GameState.Pieces[b3].Should()
            .BeEquivalentTo(new Pawn(args), "the properties should be moved");

        board.GameState.FriendlyPiecesByCoordinate(White).Count().Should()
            .Be(1, "the properties should be moved, not duplicated");
    }
    
    [Fact(DisplayName = "A queen can move backwards")]
    public void a_queen_can_move_backwards()
    {
        // Arrange
        var board = new Game(_invalidMoveHandler);

        board.GameState.Set(b2, WhiteQueen);
        board.GameState.FriendlyPiecesByCoordinate(White).Count().Should().Be(1);

        // Act
        var piece = board.GameState.Pieces[b2];
        piece.MoveTo(a3);

        // Assert
        var args = new Piece.Arguments(WhiteQueen, a3, board, _invalidMoveHandler);
        board.GameState.Pieces[a3].Should()
            .BeEquivalentTo(new Queen(args), "the properties should be moved");

        board.GameState.FriendlyPiecesByCoordinate(White).Count().Should()
            .Be(1, "the properties should be moved, not duplicated");
    }


    [Fact(DisplayName = "Invalid move should not be allowed")]
    public void invalid_move_should_not_be_allowed()
    {
        // Arrange
        var board = new Game(_invalidMoveHandler);
        board.GameState.Set(a1, new Properties(PieceType.Pawn, White));
        var piece = board.GameState.Pieces[a1];

        // Act
        piece.MoveTo(a6);

        // Assert
        _invalidMoveHandler.InvalidMoves.Should().HaveCount(1);
    }
}