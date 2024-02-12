using System.Collections.Generic;

namespace Core.Extensions
{
    public static class ListExtensions
    {
        public static T PopAt<T>(this List<T> list, int index)
        {
            if (list.Count <= index)
                return default;

            T r = list[index];

            list.RemoveAt(index);

            return r;
        }
    }
}
