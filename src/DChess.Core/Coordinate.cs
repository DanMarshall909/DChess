namespace DChess.Core;

public readonly record struct Coordinate
{
    private readonly char _file;
    private readonly byte _rank;

    public Coordinate(string positionName)
    {
        if (positionName.Length != 2)
        {
            throw new ArgumentException("Position name must be 2 characters long");
        }

        File = positionName[0];
        Rank = (byte)(positionName[1] - '0');
    }

    public Coordinate(char File, byte Rank)
    {
        this.File = File;
        this.Rank = Rank;
    }

    public override string ToString() => $"{File}{Rank}";

    public char File
    {
        get => _file;
        private init
        {
            if (value is < 'a' or > 'h')
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"File must be between 'a' and 'h' but found {_file.ToString()}");
            }

            _file = value;
        }
    }

    public byte Rank
    {
        get => (byte)_rank;
        private init
        {
            if (value is < 1 or > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"Rank must be between 1 and 8 but found {value}");
            }

            _rank = value;
        }
    }
}