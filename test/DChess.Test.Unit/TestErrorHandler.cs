using DChess.Core.Errors;
using DChess.Core.Game;

namespace DChess.Test.Unit;

public class TestErrorHandler : IErrorHandler
{
    public readonly List<MoveResult> InvalidMoves = new();
    public bool NoKingFoundExceptionThrown { get; private set; } = false;
    public bool NoPieceAtExceptionThrown { get; private set; } = false;

    public void HandleInvalidMove(MoveResult result)
    {
        InvalidMoves.Add(result);
    }

    public void HandleNoKingFound(Colour missingKingColour) => NoKingFoundExceptionThrown = true;

    public void HandleNoPieceAt(Coordinate moveFrom) => NoPieceAtExceptionThrown = true;
}