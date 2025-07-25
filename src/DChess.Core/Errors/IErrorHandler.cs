﻿namespace DChess.Core.Errors;

public interface IErrorHandler
{
    void HandleInvalidMove(MoveResult result);
    void HandleNoKingFound(Colour missingKingColour);
    void HandleNoPieceAt(Square moveFrom);
}