using DChess.Core.Board;
using DChess.Core.Exceptions;

namespace DChess.Test.Unit.Rules.Pieces;

public class KnightTests(BoardFixture fixture) : BoardTestBase(fixture)
{
    private (int, int)[] _validOffsetsForKnightsFromStartingPosition =
    {
        (1, 2),
        (1, -2),
        (-1, 2),
        (-1, -2),

        (2, 1),
        (2, -1),
        (-2, 1),
        (-2, -1)
    };

    [Fact(DisplayName = "Knights can only move in an L shape")]
    public void knights_can_move_in_an_L_shape()
    {
        for (byte rank = 3; rank < 6; rank++)
        for (var file = 'c'; file < 'f'; file++)
        {
            var from = new Coordinate(file, rank);

            Board.Clear();
            Board[from] = WhiteKnight;

            foreach ((int df, int dr) in _validOffsetsForKnightsFromStartingPosition) KnightShouldBeAbleToMoveByOffset(df, dr);

            continue;

            void KnightShouldBeAbleToMoveByOffset(int df, int dr)
            {
                var to = from.Offset(df, dr);
                Board.Pieces[from].CheckMove(to).Valid
                    .Should().BeTrue(
                        $"Knight should be able to move in a from {from} to {to})");
            }
        }
    }
    
    [Fact(DisplayName = "Knights can jump over other pieces")]
    public void knights_can_jump_over_other_pieces()
    {
        for (byte rank = 3; rank < 6; rank++)
        for (var file = 'c'; file < 'f'; file++)
        {
            var from = new Coordinate(file, rank);

            Board.Clear();
            Board[from] = WhiteKnight;
            SurroundWithWhitePawns(from);

            foreach ((int df, int dr) in _validOffsetsForKnightsFromStartingPosition) KnightShouldBeAbleToMoveByOffset(df, dr);

            continue;

            void KnightShouldBeAbleToMoveByOffset(int df, int dr)
            {
                var to = from.Offset(df, dr);
                Board.Pieces[from].CheckMove(to).Valid
                    .Should().BeTrue(
                        $"Knight should be able to move in a from {from} to {to})");
            }
        }
    }
    
    private void SurroundWithWhitePawns(Coordinate from)
    {
        Board[from.Offset(-1, 1)] = WhitePawn;
        Board[from.Offset(1, 1)] = WhitePawn;
        Board[from.Offset(1, 1)] = WhitePawn;

        Board[from.Offset(-1, 0)] = WhitePawn;
        Board[from.Offset(1, 0)] = WhitePawn;

        Board[from.Offset(-1, -1)] = WhitePawn;
        Board[from.Offset(1, -1)] = WhitePawn;
        Board[from.Offset(1, -1)] = WhitePawn;
    }

    [Fact(DisplayName = "Knights can ONLY move in an L shape")]
    public void knights_can_only_move_in_an_L_shape()
    {
        // check every possible starting position from a3 to f5 excluding out of bounds positions
        for (byte fromRank = 1; fromRank < 8; fromRank++)
        for (var fromFile = 'a'; fromFile < 'h'; fromFile++)
        {
            var from = new Coordinate(fromFile, fromRank);

            Board.Clear();
            Board[from] = WhiteKnight;

            // check every possible move and make sure it's not valid except for the offsets specified in _validOffsetsForKnightsFromStartingPosition
            TestInvalidMoves(from);
        }
    }

    private void TestInvalidMoves(Coordinate from)
    {
        for (byte toRank = 1; toRank < 8; toRank++)
        for (var toFile = 'a'; toFile < 'h'; toFile++)
            try
            {
                int df = from.File - toFile;
                int dr = from.Rank - toRank;

                if (_validOffsetsForKnightsFromStartingPosition.Contains((df, dr)))
                    continue;

                var to = from.Offset(df, dr);
                bool valid = Board.Pieces[from].CheckMove(to).Valid;

                valid.Should().BeFalse($"Knight should not be able to move from {from} to {to})");
            }
            catch (InvalidCoordinateException)
            {
                // ignore out of bounds coordinates
            }
    }
}