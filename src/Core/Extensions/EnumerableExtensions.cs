using System.Collections.Generic;
using System.Linq;

namespace Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> CrossJoin<T>(this IEnumerable<IEnumerable<T>> data)
        {
            return data.Skip(1)
                        .Aggregate(data.FirstOrDefault()?.Select(current => new List<T>() { current }),
                                    (previous, next) => previous?.SelectMany(p => next?.Select(d => new List<T>(p) { d })));
        }
    }
}
