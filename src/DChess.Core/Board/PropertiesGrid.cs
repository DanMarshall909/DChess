namespace DChess.Core.Board;

public record struct PropertiesGrid(Properties[,] Grid)
{
    public PropertiesGrid() : this(new Properties[8, 8])
    {
    }

    public Properties this[Coordinate coordinate]
    {
        get => Grid[coordinate.File - 'a', coordinate.Rank - 1];
        set => Grid[coordinate.File - 'a', coordinate.Rank - 1] = value;
    }

    public Properties this[int file, int rank]
    {
        get => Grid[file, rank];
        set => Grid[file, rank] = value;
    }

    public static PropertiesGrid CloneOrEmptyIfNull(PropertiesGrid? properties)
        => properties is not null
            ? new PropertiesGrid(properties.Value.Grid.Clone() as Properties[,] ?? new Properties[,] { })
            : new PropertiesGrid();
}