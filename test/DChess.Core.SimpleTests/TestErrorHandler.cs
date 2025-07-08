using DChess.Core.Errors;
using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.SimpleTests;

public class TestErrorHandler : IErrorHandler
{
    public void HandleInvalidMove(MoveResult result)
    {
    }

    public void HandleNoKingFound(Colour missingKingColour)
    {
    }
    
    public void HandleNoPieceAt(Square moveFrom)
    {
    }
}