using System;
using System.Linq;

namespace Chresimos.Core.Utils
{
    public static class StringUtils
    {
        public static string ToUnderscoreCase (this string str)
        {
            var strs = str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString());
            var result = strs.Aggregate(string.Empty, (current, piece) => current + piece);

            return result.ToLower();
        }

        public static string FirstCharToUpper (this string input)
        {
            switch (input)
            {
                case null:
                    throw new ArgumentNullException(nameof(input));
                case "":
                    throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default:
                    return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static string FirstCharToLower (this string input)
        {
            switch (input)
            {
                case null:
                    throw new ArgumentNullException(nameof(input));
                case "":
                    throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default:
                    return input.First().ToString().ToLower() + input.Substring(1);
            }
        }

        public static string ReplaceLast (this string source, string find, string replace)
        {
            var place = source.LastIndexOf(find, StringComparison.Ordinal);

            if (place == -1) return source;

            var result = source.Remove(place, find.Length).Insert(place, replace);
            return result;
        }

        public static string Remove (this string source, string toRemove)
        {
            return source.Replace(toRemove, string.Empty);
        }

        public static string RemoveLast (this string source, string toRemove)
        {
            return source.ReplaceLast(toRemove, string.Empty);
        }
    }
}