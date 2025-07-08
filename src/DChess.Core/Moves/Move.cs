using System.Diagnostics;

namespace DChess.Core.Moves;

[DebuggerDisplay("{Format()}")]
public readonly record struct Move
{
    public Move(Square From, Square To)
    {
        this.From = From;
        this.To = To;
        Offset = new MoveOffset(this);
    }

    public Move(string moveString)
    {
        From = new Square(moveString[..2]);
        To = new Square(moveString[2..]);
        Offset = new MoveOffset(this);
    }

    public static Move NullMove => new(NullSquare, NullSquare);
    public Distance Distance => new Memo<Move, Distance>(move => new Distance(move)).Execute(this);
    public bool HasPath => SquaresAlongPath.Any();
    public bool IsDiagonal => Math.Abs(To.File - From.File) == Math.Abs(To.Rank - From.Rank);
    public bool IsVertical => From.File == To.File;
    public bool IsHorizontal => From.Rank == To.Rank;
    public bool IsAdjacent => Distance.Total == 1;

    public IEnumerable<Square> SquaresAlongPath =>
        new Memo<Move, IEnumerable<Square>>(PathFinder.GetPath).Execute(this);

    public MoveOffset Offset { get; internal init; }
    public Square From { get; init; }
    public Square To { get; init; }

    public bool Equals(Move? other) => other is not null && From == other?.From && To == other?.To;

    public bool IsBackwards(Colour colour) => colour == White
        ? To.Rank < From.Rank
        : To.Rank > From.Rank;

    public bool IsBlocked(Board board) =>
        SquaresAlongPath.SkipLast(1).Any(square => board.HasPieceAt(square));


    public string Format() => $"({From}, {To})";
    public MoveResult AsOkResult() => new(this, Ok);
    public MoveResult AsInvalidBecause(MoveValidity invalidReason) => new(this, invalidReason);

    public void Deconstruct(out Square From, out Square To)
    {
        From = this.From;
        To = this.To;
    }
}