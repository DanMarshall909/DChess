namespace DChess.Core.Board;

public static class PropertiesExtensions
{
    public static Properties[,] CloneOrEmptyIfNull(this Properties[,]? properties)
    {
        var propertiesArray = properties?.Clone() as Properties[,];
        return (Properties[,])(propertiesArray ?? Empty.Clone());
    }

    private static Properties[,] Empty { get; } = new Properties[8, 8];
}