using System.Collections.Generic;
using System.Threading.Tasks;

namespace CLup.Application.Shared.Extensions
{
     public static class EnumerableExtensions
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
}