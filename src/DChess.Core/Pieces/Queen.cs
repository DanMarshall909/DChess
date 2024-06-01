using DChess.Core.Moves;

namespace DChess.Core.Pieces;

public record Queen : Piece
{
    public Queen(Arguments arguments) : base(arguments)
    {
    }

    public override string PieceName => "Queen";

    protected override MoveResult ValidateMove(Coordinate to)
    {
        var move = new Move(Current, to);

        if (!(move.IsDiagonal || move.IsVertical || move.IsHorizontal))
            return move.AsInvalidResult("Queen can only move diagonally, or in a straight line");

        return move.AsOkResult;
    }

    public IEnumerable<Coordinate> GetPath(Move move) => throw new NotImplementedException();
}