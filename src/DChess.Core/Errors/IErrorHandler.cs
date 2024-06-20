using DChess.Core.Game;
using DChess.Core.Moves;

namespace DChess.Core.Errors;

public interface IErrorHandler
{
    void HandleInvalidMove(MoveResult result);
    void HandleNoKingFound(Colour missingKingColour);
    void HandleNoPieceAt(Coordinate moveFrom);
}