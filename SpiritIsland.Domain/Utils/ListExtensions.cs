using System;
using System.Collections.Generic;

namespace SpiritIsland.Domain.Utils
{
    public static class ListExtensions
    {
        private static readonly Random RNG = new Random((int)DateTime.Now.Ticks);

        public static void Shuffle<T>(this IList<T> list)
        {
            var count = list.Count;
            while (count > 1)
            {
                count--;
                var current = RNG.Next(count + 1);
                var value = list[current];
                list[current] = list[count];
                list[count] = value;
            }
        }
    }
}