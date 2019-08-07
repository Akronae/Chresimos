using System;

namespace Chresimos.Core
{
    public static class MathUtils
    {
        public static float Lerp (float a, float b, float t)
        {
            return a + t * (b - a);
        }

        public static double Lerp (double a, double b, double t)
        {
            return a + t * (b - a);
        }

        public static int RoundAwayZero (float a)
        {
            if (a > 0) return (int) Math.Ceiling(a);
            if (a < 0) return (int) Math.Floor(a);

            return 0;
        }

        public static int RoundToZero (float a)
        {
            if (a >= 0 && a <= 1) return 0;
            if (a >= -1 && a <= 0) return 0;

            if (a > 0) return (int) Math.Floor(a);
            return (int) Math.Ceiling(a);
        }

        public static int BringCloserFromZero (this int a, int amount)
        {
            if (a < 0) return a + amount;
            if (a > 0) return a - amount;

            return 0;
        }

        public static int Mod (int x, int m)
        {
            return (x % m + m) % m;
        }

        public static bool SameSign (float a, float b)
        {
            return a < 0 == b < 0;
        }

        public static bool IsBetween (this float value, float min, float max)
        {
            return value >= min && value <= max;
        }
    }
}