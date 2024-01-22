using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuinDayCompany.Extensions
{
    internal static class EnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random randomizer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (randomizer == null) throw new ArgumentNullException(nameof(randomizer));

            return source._ShuffleRandomizeIterator(randomizer);
        }

        private static IEnumerable<T> _ShuffleRandomizeIterator<T>(
            this IEnumerable<T> source, Random randomizer)
        {
            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = randomizer.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}
