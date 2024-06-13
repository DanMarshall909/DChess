using DChess.Core.Game;

namespace DChess.Test.Unit.Rules;

public static class MovementTestingExtensions
{
    public const int LegalPositionValue = 1;

    /// <summary>
    ///     Test that a properties can move to a given set of offsets from its current position. Offsets resulting in invalid
    ///     coordinates are ignored.
    /// </summary>
    /// <param name="properties">The properties that will be evaluated e.g. WhiteKnight</param>
    /// <param name="offsetsFromCurrentPosition">
    ///     An array of offsets that the properties should be able to move to from its current
    ///     position. For example A knight would have offsets in an L shape i.e. (1, 2), (1, -2), (-1, 2), (-1, -2), (2, 1),
    ///     (2, -1), (-2, 1), (-2, -1)
    /// </param>
    /// <param name="testErrorHandler"></param>
    /// <param name="setupBoard">
    ///     An optional action that can be used to setup the board before each test. The action takes in
    ///     the board and the coordinate of the properties being tested
    /// </param>
    public static void ShouldBeAbleToMoveTo(this Properties properties,
        IReadOnlyCollection<MoveOffset> offsetsFromCurrentPosition, IErrorHandler testErrorHandler, Action<GameState, Coordinate>? setupBoard = null)
        => AbleToMoveWhenOffsetBy(properties, offsetsFromCurrentPosition, true, testErrorHandler, setupBoard);

    /// <summary>
    ///     Tests that every offset in invalidOffsetsFromCurrentPosition array results in an invalid move.  Offsets resulting
    ///     in invalid coordinates are ignored.
    /// </summary>
    /// <param name="properties"></param>
    /// <param name="invalidOffsetsFromCurrentPosition"></param>
    /// <param name="errorHandler"></param>
    /// <param name="setupBoard"></param>
    public static void ShouldNotBeAbleToMoveTo(this Properties properties,
        IReadOnlyCollection<MoveOffset> invalidOffsetsFromCurrentPosition, IErrorHandler errorHandler, Action<GameState, Coordinate>? setupBoard = null)
        => AbleToMoveWhenOffsetBy(properties, invalidOffsetsFromCurrentPosition, false, errorHandler, setupBoard);

    public static void ShouldOnlyBeAbleToMoveTo(this Properties properties,
        IReadOnlyCollection<MoveOffset> validOffsetsFromCurrentPosition, IErrorHandler errorHandler, Action<GameState, Coordinate>? setupBoard = null)
    {
        properties.ShouldBeAbleToMoveTo(validOffsetsFromCurrentPosition, errorHandler, setupBoard);
        var invalidOffsetsFromCurrentPosition = validOffsetsFromCurrentPosition.Inverse().ToList().AsReadOnly();
        properties.ShouldNotBeAbleToMoveTo(invalidOffsetsFromCurrentPosition, errorHandler, setupBoard);
    }

    /// <summary>
    ///     Tests if a properties can move to a given set of offsets from its current position. Offsets resulting in invalid
    ///     coordinates are ignored.
    /// </summary>
    /// <param name="properties"></param>
    /// <param name="offsetsFromCurrentPosition"></param>
    /// <param name="shouldBeAbleToMove"></param>
    /// <param name="errorHandler"></param>
    /// <param name="setupBoard"></param>
    private static void AbleToMoveWhenOffsetBy(this Properties properties,
        IReadOnlyCollection<MoveOffset> offsetsFromCurrentPosition, bool shouldBeAbleToMove, IErrorHandler errorHandler,
        Action<GameState, Coordinate>? setupBoard = null)
    {
        var gameState = new GameState(new PiecePool(), new BoardState(), errorHandler);
        for (byte rank = 1; rank < 8; rank++)
        for (var file = 'a'; file < 'h'; file++)
        {
            var from = new Coordinate(file, rank);

            gameState.Clear();
            gameState.Place(properties, from);
            setupBoard?.Invoke(gameState, from);

            var pieceAtFrom = gameState.Pieces[from];
            foreach (var offset in offsetsFromCurrentPosition)
                if (from.TryApplyOffset(offset, out var to))
                {
                    var moveResult = pieceAtFrom.CheckMove(to, gameState);
                    moveResult
                        .IsValid
                        .Should().Be(shouldBeAbleToMove,
                            $"{pieceAtFrom.Properties} should {(shouldBeAbleToMove ? "" : "not ")}be able to move from {from} to {to})");
                }
        }
    }

    public static void SetOffsetPositions(this GameState gameState, Coordinate from, IEnumerable<MoveOffset> offsets,
        Properties properties)
    {
        foreach (var offset in offsets)
            if (from.TryApplyOffset(offset, out var to))
                gameState.Place(properties, to);
    }

    /// <summary>
    ///     Sets the positions of the board to be 2 cells away from the given coordinate in all directions.
    /// </summary>
    /// <param name="game"></param>
    /// <param name="coordinate">The coordinate to set the positions around</param>
    /// <param name="properties"></param>
    public static void Surround2CellsFrom(this GameState game, Coordinate coordinate, Properties properties)
    {
        SetOffsetPositions(game, coordinate, new MoveOffset[]
        {
            (-2, -2), (-1, -2), (0, -2), (1, -2), (2, -2),
            (-2, -1), (2, -1),
            (-2, 0), (2, 0),
            (-2, 1), (2, 1),
            (-2, 2), (-1, 2), (0, 2), (1, 2), (2, 2)
        }, properties);
    }

    public static void Surround(this GameState game, Coordinate coordinate, Properties properties)
    {
        SetOffsetPositions(game, coordinate, new MoveOffset[]
        {
            (-1, 1),
            (1, 1),
            (1, 1),
            (-1, 0),
            (1, 0),
            (-1, -1),
            (1, -1),
            (1, -1)
        }, properties);
    }


    /// <summary>
    ///     Returns an "inverse" of the given offsets. The inverse of a set of offsets is the set of all offsets that are not
    ///     in the given set bounded by -8 to 8. Note that not all of these offsets are valid coordinates on a chess board, so
    ///     they should be checked before use.
    /// </summary>
    /// <param name="offsets">a collection of offsets</param>
    /// <returns></returns>
    public static IEnumerable<MoveOffset> Inverse(this IReadOnlyCollection<MoveOffset> offsets)
    {
        for (int df = -7; df <= 7; df++)
        for (int dr = -7; dr <= 7; dr++)
        {
            if (offsets.Contains(new MoveOffset(dr, df)))
                continue;

            yield return new MoveOffset(df, dr);
        }
    }

    /// <summary>
    ///     Converts a 15x15 matrix of bytes to an array of MoveOffsets. The matrix should be centered at the center of the
    ///     matrix i.e. (8, 8).
    /// </summary>
    /// <param name="movesFromCenter"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static MoveOffset[] ToMoveOffsets(this byte[,] movesFromCenter)
    {
        var offsets = new List<MoveOffset>();
        int files = movesFromCenter.GetLength(0);
        int ranks = movesFromCenter.GetLength(1);
        if (files != 15 || ranks != 15) throw new ArgumentException("Matrix must be 15x15");

        for (var i = 0; i < files; i++)
        for (var j = 0; j < ranks; j++)
            if (movesFromCenter[i, j] == LegalPositionValue)
                offsets.Add((i - 7, j - 7));

        return offsets.ToArray();
    }
}