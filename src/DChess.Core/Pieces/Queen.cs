using DChess.Core.Game;
using DChess.Core.Moves;
using static DChess.Core.Moves.MoveValidity;

namespace DChess.Core.Pieces;

public record Queen : Piece
{
    public Queen(Arguments arguments) : base(arguments)
    {
    }

    public override string PieceName => "Queen";

    protected override MoveResult ValidatePath(Coordinate to, Game.Game game)
    {
        var move = new Move(Coordinate, to);

        return move.IsDiagonal || move.IsVertical || move.IsHorizontal
            ? move.AsOkResult()
            : move.AsInvalidBecause(QueenCanOnlyMoveDiagonallyOrInAStraightLine);
    }
}