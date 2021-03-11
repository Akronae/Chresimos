using System;
using System.Collections;
using System.Collections.Generic;

namespace Chresimos.Core.Utils
{
    public static class DictionaryUtils
    {
        public static bool IsIDictionaryType (this Type type)
        {
            return ((IList) type.GetInterfaces()).Contains(typeof(IDictionary));
        }

        public static IDictionary CreateDictionaryOfTypes (Type keyType, Type valueType)
        {
            var genericDictionaryType = typeof(Dictionary<,>).MakeGenericType(keyType, valueType);

            return (IDictionary) Activator.CreateInstance(genericDictionaryType);
        }

        public static void AddRange (this IDictionary dic1, IDictionary dic2)
        {
            foreach (DictionaryEntry entry in dic2) dic1.Add(entry.Key, entry.Value);
        }

        public static void AddRange <T1, T2> (this IDictionary<T1, T2> dic1, IDictionary<T1, T2> dic2,
            Func<KeyValuePair<T1, T2>, T1> key, Func<KeyValuePair<T1, T2>, T2> value)
        {
            foreach (var entry in dic2) dic1.Add(key(entry), value(entry));
        }
    }
}