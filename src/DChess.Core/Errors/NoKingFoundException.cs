using DChess.Core.Game;

namespace DChess.Core.Errors;

public class NoKingFoundException(Colour colour) : DChessException($"No {colour} king found");