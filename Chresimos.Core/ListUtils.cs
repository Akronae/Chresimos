using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chresimos.Core
{
    public static class ListUtils
    {
        public static readonly byte[] EmptyByteArray = new byte[0];

        public static bool IsIListType (this Type type)
        {
            return type.GetInterfaces().Contains(typeof(IList));
        }

        public static IList CreateListOfType (Type type)
        {
            var genericListType = typeof(List<>).MakeGenericType(type);

            return (IList) Activator.CreateInstance(genericListType);
        }

        public static Type GetListElementTypeOrDefault (this IList list)
        {
            return list.GetType().GetGenericArguments().SingleOrDefault();
        }

        public static string FormattedString <T> (this IEnumerable<T> list, Func<T, string> predicate,
            string separator = ", ")
        {
            if (list is null) return string.Empty;
            var enumerable = list as T[] ?? list.ToArray();
            if (!enumerable.Any()) return string.Empty;

            var sb = new StringBuilder();

            foreach (var elem in enumerable) sb.Append(predicate(elem) + separator);

            sb.Remove(sb.Length - separator.Length, separator.Length);

            return sb.ToString();
        }

        public static string FormattedString <T> (this IEnumerable<T> list)
        {
            string predicate (T input)
            {
                return ReferenceEquals(input, null) ? "<null>" : input.ToString();
            }

            return FormattedString(list, predicate);
        }

        public static void AddRangeUnsafe (this IList iList, IEnumerable toAdd)
        {
            foreach (var value in toAdd)
            {
                iList.Add(value);
            }
        }

        public static void AddIfNotContained <T> (this List<T> list, T obj)
        {
            if (!list.Contains(obj)) list.Add(obj);
        }

        public static List<T> Shuffle <T> (this IEnumerable<T> list)
        {
            var shuffled = new List<T>(list);

            var n = shuffled.Count;
            while (n > 1)
            {
                n--;
                var k = RandUtils.Rand.Next(n + 1);
                var value = shuffled[k];
                shuffled[k] = shuffled[n];
                shuffled[n] = value;
            }

            return shuffled;
        }

        public static void CopyToList (this ICollection collection, IList list)
        {
            foreach (var element in collection)
            {
                list.Add(element);
            }
        }
    }
}