using System.Runtime.CompilerServices;
using System.Text;

namespace DChess.Core;

/// <summary>
/// A memory-efficient  representation of a chessboard using bitboards.
/// This optimisation is necessary due to the huge number of possible chessboard
/// states that will need to be evaluated.
/// </summary>
public struct BitBoard
{
    public UInt64 UInt64Value => _bitBoard;
     
    // Human-readable representation of the bitboard for debugging purposes
    public override string ToString()
    {
        // return binary representation of the bitboard splitting it into 8 bytes
        // pad the binary string with zeros to make it 64 characters long
        var bin = $"0b{Convert.ToString((long)_bitBoard, 2).PadLeft(64, '0')}";
        
        var sb = new StringBuilder();
        for (var i = 0; i < 64; i += 8)
        {
            sb.AppendLine(bin.Substring(i+2, 8));
        }

        sb.Length--; // Remove the last underscore
        return sb.ToString();
    }

    public BitBoard()
    {
        _bitBoard = 0;
    }

    private ulong _bitBoard;

    public BitBoard(ulong bitBoard)
    {
        _bitBoard = bitBoard;
    }

    public bool this[Coordinate coordinate]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this[coordinate.File, coordinate.Rank];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => this[coordinate.File, coordinate.Rank] = value;
    }

    public bool this[char file, byte rank]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => IsSet(file, rank);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value)
                Set(file, rank);
            else
                Clear(file, rank);
        }
    }

    /// <summary>
    /// Sets the bit position based on the file and rank
    /// </summary>
    /// <param name="file"></param>
    /// <param name="rank"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // The bitwise OR operator (|=) is used to set the bit at that position to 1.
    public void Set(char file, byte rank)
    {
        byte index = GetIndex(file, rank);
        // set the bit at the given index to 1
        _bitBoard |= (uint)(1 << index);
    }

    /// <summary>
    /// Clears the bit position based on the file and rank
    /// </summary>
    /// <param name="file"></param>
    /// <param name="rank"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    // The bitwise AND operator (&=) with the bitwise complement operator (~) is used to set the bit at that position to 0.
    public void Clear(char file, byte rank)
    {
        byte indexOf = Coordinate.IndexOf(file, rank);
        ulong clearMask = ~(1UL << (indexOf));
        _bitBoard &= clearMask;
    }

    /// <summary>
    /// Clears the bit position based on the file and rank
    /// </summary>
    /// <param name="coordinate"></param>
    public void Clear(Coordinate coordinate) => Clear(coordinate.File, coordinate.Rank);

    /// <summary>
    /// Checks if a square on the chessboard is set (contains a piece).
    /// </summary>
    /// <param name="file"></param>
    /// <param name="rank"></param>
    /// <returns>True if a piece is set at the given coordinates, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSet(char file, byte rank) => (_bitBoard & 1UL <<GetIndex(file, rank)) != 0;

    /// <summary>
    /// Checks if a square on the chessboard is set (contains a piece).
    /// </summary>
    /// <param name="file"></param>
    /// <param name="rank"></param>
    /// <returns>True if a piece is set at the given coordinates, false otherwise.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsSet(Coordinate coordinate) => (_bitBoard & GetIndex(coordinate.File, coordinate.Rank)) != 0;

    /// <summary>
    /// Sets the bit position based on the file and rank
    /// </summary>
    /// <param name="coordinate"></param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public readonly void Set(Coordinate coordinate) => Set(coordinate.File, coordinate.Rank);

    /// <summary>
    /// Calculates the zero-based index of a chessboard square given its file and rank
    /// </summary>
    /// <param name="file">A character ('a' to 'h') representing the file (column) on the chessboard.</param>
    /// <param name="rank">A byte (1 to 8) representing the rank (row) on the chessboard.</param>
    /// <returns>The zero-based index of a chessboard</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static byte GetIndex(char file, byte rank) => (byte)(((file - 'a') + ((rank - 1) << 3)));
    // Calculation Steps:
    // 1. file - 'a': Converts the file character to a zero-based index (e.g., 'a' to 0, 'b' to 1).
    // 2. rank - 1: Converts the rank to a zero-based index (e.g., 1 to 0, 2 to 1).
    // 3. (rank - 1) << 3: Shifts the zero-based rank by 3 bits to the left, effectively multiplying it by 8.
    //    This positions the rank index in the correct bit positions for an 8x8 board.
    // 4. Adding the file index to the shifted rank index gives the final zero-based index for the chessboard square.
    //
    // Example:
    // For file = 'b' and rank = 3:
    // - file - 'a' is 1 (since 'b' is the second file).
    // - rank - 1 is 2 (since rank 3 is the third row).
    // - (2 << 3) is 16 (since shifting 2 by 3 bits to the left is equivalent to multiplying by 8).
    // - The final index is 1 + 16 = 17.
    //
    // Thus, GetIndex('b', 3) returns 17, the zero-based index for the square b3 on the chessboard.

    public static implicit operator ulong(BitBoard b) => b._bitBoard;
    public static implicit operator BitBoard(ulong value) => new(value);

    private static readonly ulong[] RankMask = new ulong[8]
    {
        0b_11111111_00000000_00000000_00000000_00000000_00000000_00000000_00000000,
        0b_00000000_11111111_00000000_00000000_00000000_00000000_00000000_00000000,
        0b_00000000_00000000_11111111_00000000_00000000_00000000_00000000_00000000,
        0b_00000000_00000000_00000000_11111111_00000000_00000000_00000000_00000000,
        0b_00000000_00000000_00000000_00000000_11111111_00000000_00000000_00000000,
        0b_00000000_00000000_00000000_00000000_00000000_11111111_00000000_00000000,
        0b_00000000_00000000_00000000_00000000_00000000_00000000_11111111_00000000,
        0b_00000000_00000000_00000000_00000000_00000000_00000000_00000000_11111111
    };

    private static readonly ulong[] FileMask = new ulong[8]
    {
        0b_10000000_10000000_10000000_10000000_10000000_10000000_10000000_10000000,
        0b_01000000_01000000_01000000_01000000_01000000_01000000_01000000_01000000,
        0b_00100000_00100000_00100000_00100000_00100000_00100000_00100000_00100000,
        0b_00010000_00010000_00010000_00010000_00010000_00010000_00010000_00010000,
        0b_00001000_00001000_00001000_00001000_00001000_00001000_00001000_00001000,
        0b_00000100_00000100_00000100_00000100_00000100_00000100_00000100_00000100,
        0b_00000010_00000010_00000010_00000010_00000010_00000010_00000010_00000010,
        0b_00000001_00000001_00000001_00000001_00000001_00000001_00000001_00000001
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IEnumerable<Coordinate> GetSetBits()
    {
        for (char file = 'a'; file < 'i'; file++)
        {
            bool isInFile = (_bitBoard & FileMask[file - 'a']) != 0;
            if (!isInFile) continue;

            for (byte rank = 1; rank < 9; rank++)
            {
                if (IsSet(file, rank))
                {
                    yield return new Coordinate(file, rank);
                }
            }
        }
    }
}