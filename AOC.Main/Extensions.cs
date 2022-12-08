using System;
using System.Collections.Generic;

namespace AOC
{
    public static class Extensions
    {
        public static IEnumerable<T> TakeWhileIncluding<T>(this IEnumerable<T> list, Func<T, bool> predicate)
        {
            foreach (T el in list)
            {
                yield return el;
                if (!predicate(el))
                    yield break;
            }
        }

    }
}