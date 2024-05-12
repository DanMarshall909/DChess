namespace DChess.Test.Unit;

public readonly record struct Position
{
    private readonly char _file;
    private readonly int _rank;

    public Position(string postion)
    {
        if (postion.Length != 2)
        {
            throw new ArgumentException("Position must be 2 characters long");
        }

        File = postion[0];
        Rank = postion[1] - '0';
    }
    public Position(char File, int Rank)
    {
        this.File = File;
        this.Rank = Rank;
    }

    public char File
    {
        get => _file;
        init
        {
            if (value < 'a' || value > 'g')
            {
                throw new ArgumentOutOfRangeException(nameof(value), "File must be between 'a' and 'h'");
            }
            _file = value;
        }
    }

    public int Rank
    {
        get => _rank;
        init
        {
            if (value < 1 || value > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Rank must be between 1 and 8");
            }
            _rank = value;
        }
    }

    public void Deconstruct(out char File, out int Rank)
    {
        File = this.File;
        Rank = this.Rank;
    }
}