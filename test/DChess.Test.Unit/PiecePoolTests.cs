﻿using DChess.Core.Board;
using DChess.Core.Pieces;
using static DChess.Core.Board.Colour;

namespace DChess.Test.Unit;

public class PiecePoolTests
{
    private readonly TestInvalidMoveHandler _invalidMoveHandler = new();

    [Fact(DisplayName = "A piece can be retrieved from the pool")]
    public void a_piece_can_be_retrieved_from_the_pool()
    {
        // Arrange
        var board = new Board(_invalidMoveHandler);
        var chessPiece = new ChessPiece(PieceType.Pawn, White);

        // Act
        var pool = new PiecePool(board, _invalidMoveHandler);
        var piece = pool.Get(a1, chessPiece);
        var secondPieceWithSameDefinition = pool.Get(a1, chessPiece);

        // Assert
        piece.Should().BeEquivalentTo(new Pawn(new Piece.Arguments(chessPiece, a1, board, _invalidMoveHandler)));
        piece.Should().Be(secondPieceWithSameDefinition, "the piece should be the same instance");

        piece.GetType().Should().Be<Pawn>();
    }
}