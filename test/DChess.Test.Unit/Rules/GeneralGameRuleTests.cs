﻿using DChess.Core.Game;

namespace DChess.Test.Unit.Rules;

public class GeneralGameRuleTests : GameTestBase
{
    [Fact(DisplayName = "A piece cannot take its own pieceAttributes")]
    public void a_piece_cannot_take_its_own_piece()
    {
        var whitePawn = new PieceAttributes(Colour.White, Piece.Kind.Pawn);

        Sut.Board.Place(whitePawn, a1);
        Sut.Board.Place(whitePawn, b2);

        var piece = Sut.Pieces[a1];
        var result = piece.CheckMove(b2, Sut);

        result.IsValid.Should().BeFalse();
    }
}