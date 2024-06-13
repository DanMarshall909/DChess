using DChess.Core.Game;

namespace DChess.Core.Moves;

public interface IErrorHandler
{
    void HandleInvalidMove(MoveResult result);
    void HandleNoKingFound();
    void HandleNoPieceAt(Coordinate moveFrom);
}