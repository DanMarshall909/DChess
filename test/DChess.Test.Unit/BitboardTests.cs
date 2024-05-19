using DChess.Core;
using static DChess.Core.CoordinateExtensions;

namespace DChess.Test.Unit;

public class BitboardTests
{
    [Fact(DisplayName = "A bitboard can be created with a default value of 0")]
    public void a_bitboard_can_be_created_with_a_default_value_of_0()
    {
        // Arrange
        var bitboard = new BitBoard();

        // Assert
        bitboard.UInt64Value.Should().Be(0);
    }
    
    [Fact(DisplayName = "A bitboard can be created with a specific value")]
    public void a_bitboard_can_be_created_with_a_specific_value()
    {
        // Arrange
        var bitboard = new BitBoard(1);

        // Assert
        bitboard.UInt64Value.Should().Be(1);
    }
    
    [Fact(DisplayName = "A bitboard can be indexed by a coordinate")]
    public void a_bitboard_can_be_indexed_by_a_coordinate()
    {
        // Arrange
        var bitboard = new BitBoard();

        // Act
        bitboard[b2] = true;
        // The bitboard should have a single bit set at d3 i.e. index ('b' - 'a') * 8 + 2 - 1 = (1) * 8 + 2 - 1= 9. This is 1000000000 (10 bits) in binary.
        b2.Index.Should().Be(9);
        const int expected = 0b_10_00000000;
        bitboard.UInt64Value.Should().Be(expected);
        
        // Assert
        bitboard[b2].Should().BeTrue();
        bitboard.ToString().Select(c => c).Count(c => c == '1').Should().Be(1, because: "the only bit set should be at d3");
    }
    
    [Fact(DisplayName = "A bitboard can be unset")]
    public void a_bitboard_can_be_set_and_unset()
    {
        // Arrange
        var bitboard = new BitBoard();

        // Act
        bitboard[b2] = true;

        // Assert
        bitboard[b2].Should().BeTrue();
        bitboard[b2] = false;
        bitboard[b2].Should().BeFalse();
    }
}