using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Errors;

public class ExceptionErrorHandler : IErrorHandler
{
    public void HandleInvalidMove(MoveResult result)
    {
        throw new InvalidMoveException(result.Move, $"Invalid move: {result.Validity}");
    }

    public void HandleNoKingFound(Colour missingKingColour)
    {
        throw new NoKingFoundException(missingKingColour);
    }

    public void HandleNoPieceAt(Coordinate moveFrom)
    {
        throw new NotImplementedException();
    }
}