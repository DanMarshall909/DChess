using DChess.Core.Game;

namespace DChess.Test.Unit.Rules;

public class MovementTestingExtensionsTests
{
    public readonly byte[,] Matrix =
    {
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    [Fact(DisplayName = "ToMoveOffsets converts matrix to offsets")]
    public void ToMoveOffsets_converts_matrix_to_offsets()
    {
        var moveOffsets = new MoveOffset[] { new(1, 0), new(0, 1) };
        moveOffsets.Length.Should().Be(2);
        Matrix.ToMoveOffsets().Should().BeEquivalentTo(moveOffsets);
    }

    [Fact(DisplayName = "Invert inverts a matrix")]
    public void Invert_inverts_an_offset_matrix()
    {
        var inverse = Matrix.ToMoveOffsets().Inverse().ToArray();

        const int totalOffsets = 15 * 15;
        inverse.Length.Should().Be(totalOffsets - 2);
        var moveOffsets = new MoveOffset[] { new(1, 0), new(0, 1) };
        inverse.Should().NotContain(moveOffsets);
        inverse.Should().Contain(new MoveOffset[]
            { new(1, 1), new(-1, -1), new(-7, -7), new(7, -7), new(7, 7), new(-7, 7) });
        inverse.Should().NotContain(new MoveOffset[]
            { new(-8, -8), new(8, -8), new(8, 8), new(-8, 8) });
    }
    
    [Fact(DisplayName = "A text board can be converted to a board")]
    public void a_text_board_can_be_converted_to_a_board()
    {
        var text = @"
         abcdefgh
        8♖♘♗♕♔♗♘♖
        7♙♙♙♙♙♙♙♙
        6█░█░█░█░
        5░█░█░█░█
        4█░█░█░█░
        3░█░█░█░█
        2♟♟♟♟♟♟♟♟
        1♜♞♝♛♚♝♞♜
        ";
        var board = BoardExtensions.FromText(text);
        board[a1].Should().Be(PieceAttributes.BlackRook);
        board[h8].Should().Be(PieceAttributes.WhiteRook);
        board[e1].Should().Be(PieceAttributes.BlackKing);
        board[e8].Should().Be(PieceAttributes.WhiteKing);
    }
}