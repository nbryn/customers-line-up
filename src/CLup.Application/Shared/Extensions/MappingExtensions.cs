namespace CLup.Application.Shared.Extensions;

public static class MappingExtensions
{
    public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> items)
    {
        var results = new List<T>();
        await foreach (var item in items)
        {
            results.Add(item);
        }

        return results;
    }

}
