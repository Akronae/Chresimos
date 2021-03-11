using System;

namespace Chresimos.Core.Utils
{
    public static class ArrayUtils
    {
        public static T[] GetRange <T> (this T[] data, int index, int length)
        {
            var result = new T[length];
            Array.Copy(data, index, result, 0, length);

            return result;
        }
    }
}