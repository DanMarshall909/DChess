namespace DChess.Core.Errors;

public class NoKingFoundException(Colour colour) : DChessException($"No {colour} king found");
public class OpponentIsInCheckException() : DChessException($"Opponent should not be able to be in check after move");