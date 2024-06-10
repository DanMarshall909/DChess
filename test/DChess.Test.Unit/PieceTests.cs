using DChess.Core.Game;
using DChess.Core.Pieces;
using static DChess.Core.Game.Colour;

namespace DChess.Test.Unit;

public class PieceTests
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();

    [Fact(DisplayName = "A piece can be moved")]
    public void a_piece_can_be_moved()
    {
        // Arrange
        var game = new Game(_invalidMoveHandler);
        game.GameState.Place(WhitePawn, a2);
        game.GameState.Place(WhiteKing, f1);

        // Act
        var piece = game.GameState.Pieces[a2];
        piece.MoveTo(b3);

        // Assert
        // ReSharper disable once HeapView.BoxingAllocation
        game.GameState.Pieces[b3].Properties.Should()
            .BeEquivalentTo(WhitePawn, "the properties should be moved");

        game.GameState.FriendlyPieces(White).Count().Should()
            .Be(2, "the properties should be moved, not duplicated");
    }
    
    [Fact(DisplayName = "A queen can move backwards")]
    public void a_queen_can_move_backwards()
    {
        // Arrange
        var game = new Game(_invalidMoveHandler);

        game.GameState.Place(WhiteQueen, b2);
        game.GameState.Place(WhiteKing, f1);
        game.GameState.FriendlyPieces(White).Count().Should().Be(2);

        // Act
        var piece = game.GameState.Pieces[b2];
        piece.MoveTo(a3);

        // Assert
        var args = new Piece.Arguments(WhiteQueen, a3, game, _invalidMoveHandler);
        game.GameState.Pieces[a3].Should()
            .BeEquivalentTo(new Queen(args), "the properties should be moved");

        game.GameState.FriendlyPieces(White).Count().Should()
            .Be(2, "the properties should be moved, not duplicated");
    }


    [Fact(DisplayName = "Invalid move should not be allowed")]
    public void invalid_move_should_not_be_allowed()
    {
        // Arrange
        var game = new Game(_invalidMoveHandler);
        game.GameState.Place(WhiteKing, f1);
        game.GameState.Place(new Properties(PieceType.Pawn, White), a1);
        var piece = game.GameState.Pieces[a1];

        // Act
        piece.MoveTo(a6);

        // Assert
        _invalidMoveHandler.InvalidMoves.Should().HaveCount(1);
    }
}