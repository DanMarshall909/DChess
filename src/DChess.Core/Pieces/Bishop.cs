using DChess.Core.Game;
using DChess.Core.Moves;
using static DChess.Core.Moves.MoveValidity;

namespace DChess.Core.Pieces;

internal record Bishop : Piece
{
    public Bishop(Arguments arguments) : base(arguments)
    {
    }

    public override string PieceName => "Bishop";

    protected override MoveResult ValidateMove(Coordinate to, GameState gameState)
    {
        var move = new Move(Coordinate, to);
        if (!move.IsDiagonal)
            return move.InvalidResult(BishopCanOnlyMoveDiagonally);

        return move.OkResult();
    }
}