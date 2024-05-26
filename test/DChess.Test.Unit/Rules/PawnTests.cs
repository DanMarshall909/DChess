using DChess.Core.Pieces;

namespace DChess.Test.Unit.Rules;

public class PawnTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    [Fact(DisplayName = "Paws can move forward one square")]
    public void pawn_can_move_forward_one_square()
    {
        Board[a1] = ChessPiece.WhitePawn;
    }
}