using DChess.Core.Exceptions;
using DChess.Core.Moves;

namespace DChess.Core.Board;

public readonly record struct Coordinate
{
    private readonly char _file;
    private readonly byte _rank;

    /// <summary>
    ///  Creates a new Coordinate from a string representation e.g. a1. Note that this is case sensitive
    /// </summary>
    /// <param name="coordinateAsString">The string representation of the coordinate</param>
    /// <exception cref="InvalidCoordinateException">Thrown if the string is not 2 characters long or if the file or rank is out of bounds</exception>
    public Coordinate(string coordinateAsString)
    {
        if (coordinateAsString.Length != 2)
            throw new InvalidCoordinateException("Position name must be 2 characters long");

        File = coordinateAsString[0];
        Rank = (byte)(coordinateAsString[1] - '0');
    }

    /// <summary>
    ///  Creates a new Coordinate from 
    /// </summary>
    /// <param name="File">The file of the coordinate (a-h) running from left to right on a chess board</param>
    /// <param name="Rank">The rank of the coordinate (1-8) running from bottom to top on a chess board</param>
    /// <exception cref="InvalidCoordinateException">Thrown if the string is not 2 characters long or if the file or rank is out of bounds</exception>
    public Coordinate(char File, byte Rank)
    {
        this.File = File;
        this.Rank = Rank;
    }

    public override string ToString() => $"{File}{Rank}";

    /// <summary>
    /// The file of the coordinate (a-h) running from left to right on a chess board
    /// </summary>
    /// <exception cref="InvalidCoordinateException">Thrown if out of bounds</exception>
    public char File
    {
        get => _file;
        private init
        {
            if (value is < 'a' or > 'h')
                throw new InvalidCoordinateException(_file, _rank,
                    $"File must be between 'a' and 'h' but found {_file.ToString()}");

            _file = value;
        }
    }

    public static bool IsValid(char file, byte rank) => file is >= 'a' and <= 'h' && rank is >= 1 and <= 8;

    /// <summary>
    /// The rank of the coordinate (1-8) running from bottom to top on a chess board
    /// </summary>
    /// <exception cref="InvalidCoordinateException">Thrown if out of bounds</exception>
    public byte Rank
    {
        get => (byte)_rank;
        private init
        {
            if (value is < 1 or > 8)
                throw new InvalidCoordinateException(_file, _rank, $"Rank must be between 1 and 8 but found {value}");

            _rank = value;
        }
    }

    /// <summary>
    /// Returns a new Coordinate that is offset by the given offset
    /// </summary>
    /// <param name="offset">the offset to apply</param>
    /// <returns></returns>
    public Coordinate OffsetBy(MoveOffset moveOffset) => new((char)(File + moveOffset.FileOffset), (byte)(Rank + moveOffset.RankOffset));
    
    
    /// <summary>
    /// Returns true if the given offset is a valid coordinate on a chess board
    /// </summary>
    /// <param name="dFile"></param>
    /// <param name="dRank"></param>
    /// <returns></returns>
    public bool IsValidOffset(MoveOffset moveOffset) => IsValid((char)(File + moveOffset.FileOffset), (byte)(Rank + moveOffset.RankOffset));
    
    public bool TryOffset(MoveOffset moveOffset, out Coordinate? newCoordinate)
    {
        newCoordinate = IsValidOffset(moveOffset) ? OffsetBy(moveOffset) : null;
        return newCoordinate is not null;
    }
}