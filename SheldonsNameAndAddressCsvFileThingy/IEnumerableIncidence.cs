using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SheldonsNameAndAddressCsvFileThingy
{    

    public static class IEnumerableIncidence
    {
        public struct Result<T>
        {
            public T Value { get; set; }
            public int Count { get; set; }
        }

        public static IEnumerable<Result<TKey>> GetIncidence<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.GroupBy(keySelector).Select(group => new Result<TKey>
            {
                Value = group.Key,
                Count = group.Count()
            });
        }
        
    }
}
