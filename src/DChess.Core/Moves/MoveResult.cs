namespace DChess.Core.Moves;

public readonly record struct MoveResult(Move Move, MoveValidity Validity)
{
    public bool IsValid => Validity == Ok;

    public override string ToString() =>
        IsValid ? $"Validity move {Move}" : $"Invalid move {Move}: {Validity.Message()}";

    public static MoveResult InvalidMove(string reason) => new(new Move(), MoveValidity.InvalidMove);
}