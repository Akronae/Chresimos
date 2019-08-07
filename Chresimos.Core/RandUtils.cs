using System;
using System.Collections.Generic;
using System.Linq;

namespace Chresimos.Core
{
    public static class RandUtils
    {
        public static readonly Random Rand = new Random();
        private static long _idCount;

        public static float Range (this Random random, float minimum, float maximum)
        {
            return (float) random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static int Range (this Random random, int minimum, int maximum)
        {
            return random.Next(minimum, maximum + 1);
        }

        public static float Range (float minimum, float maximum)
        {
            return Rand.Range(minimum, maximum);
        }

        public static float Range (double minimum, double maximum)
        {
            return Rand.Range((float) minimum, (float) maximum);
        }

        public static int Range (int minimum, int maximum)
        {
            return Rand.Range(minimum, maximum);
        }

        public static string RandomId ()
        {
            _idCount += 1;
            return Rand.Next(0, 10000).ToString() + _idCount;
        }

        public static T GetRandFromList <T> (this Random random, params T[] objs)
        {
            if (objs.Length == 0) return default(T);
            var rand = random.Range(0, objs.Length - 1);
            return objs[rand];
        }

        public static T GetRandFromList <T> (this IEnumerable<T> objs)
        {
            return GetRandFromList(Rand, objs.ToArray());
        }
    }
}