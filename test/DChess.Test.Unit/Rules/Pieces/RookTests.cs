﻿namespace DChess.Test.Unit.Rules.Pieces;

public class RookTests(BoardFixture fixture) :BoardTestBase(fixture)
{
    [Fact(DisplayName = "Rooks can move horizontally")]
    public void rooks_can_move_horizontally()
    {
        Board[a1] = WhiteRook;

        Board.Pieces[a1].MoveTo(h1);
    }
}