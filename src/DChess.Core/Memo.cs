namespace DChess.Core;

/// <summary>
/// Utility class to memoize a function that takes in 
/// </summary>
/// <typeparam name="TInput"> The input type for the function to memoize</typeparam>
/// <typeparam name="TOutput">The output type for the function</typeparam>
public class Memo<TInput, TOutput> where TInput : notnull
{
    private readonly Dictionary<TInput, TOutput> _cache = new();
    private readonly Func<TInput, TOutput> _function;
    
    /// <summary>
    /// Create a new memoized function
    /// </summary>
    /// <param name="function">The function to memoize</param>
    public Memo(Func<TInput, TOutput> function) => _function = function;

    public TOutput Execute(TInput input)
    {
        if (_cache.TryGetValue(input, out var result))
            return result;

        result = _function(input);
        _cache[input] = result;

        return result;
    }
}