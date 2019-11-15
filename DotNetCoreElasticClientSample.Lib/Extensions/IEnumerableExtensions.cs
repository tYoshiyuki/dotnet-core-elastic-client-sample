using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCoreElasticClientSample.Lib.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TItem> Distinct<TItem, TKey>(this IEnumerable<TItem> source, Func<TItem, TKey> keySelector)
        {
            IEnumerable<TItem> Enumerate()
            {
                var set = new HashSet<TKey>();
                foreach (var item in source)
                    if (set.Add(keySelector(item))) yield return item;
            }

            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            return Enumerate();
        }

    }
}
