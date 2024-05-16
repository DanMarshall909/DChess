namespace DChess.Core;

public readonly record struct Coordinate
{
    private readonly char _file;
    private readonly int _rank;

    public Coordinate(string positionName)
    {
        if (positionName.Length != 2)
        {
            throw new ArgumentException("Position must be 2 characters long");
        }

        File = positionName[0];
        Rank = positionName[1] - '0';
    }
    public Coordinate(char File, int Rank)
    {
        this.File = File;
        this.Rank = Rank;
    }

    public char File
    {
        get => _file;
        private init
        {
            if (value is < 'a' or > 'h')
            {
                throw new ArgumentOutOfRangeException(nameof(value), $"File must be between 'a' and 'g' but found {_file}");
            }
            _file = value;
        }
    }

    public int Rank
    {
        get => _rank;
        private init
        {
            if (value is < 1 or > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Rank must be between 1 and 8");
            }
            _rank = value;
        }
    }
}