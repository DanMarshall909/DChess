﻿using DChess.Core.Pieces;

namespace DChess.Test.Unit.Rules;

public class GameRuleTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    [Fact(DisplayName = "A piece cannot take its own piece")]
    public void a_piece_cannot_take_its_own_piece()
    {
        // Arrange
        var whitePawn = new ChessPiece(PieceType.Pawn, Colour.White);

        Fixture.Board[a1] = whitePawn;
        Fixture.Board[b2] = whitePawn;

        // Act
        var boardPiece = Board.Pieces[a1];
        var result = boardPiece.CheckMove(b2);

        result.Valid.Should().BeFalse();
    }
}