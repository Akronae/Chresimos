using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chresimos.Core.Utils
{
    public static class TypeUtils
    {
        public static bool IsSimpleType (this Type type)
        {
            return
                type.IsPrimitive ||
                new[]
                {
                    typeof(bool),
                    typeof(sbyte),
                    typeof(byte),
                    typeof(char),
                    typeof(short),
                    typeof(ushort),
                    typeof(int),
                    typeof(uint),
                    typeof(long),
                    typeof(ulong),
                    typeof(float),
                    typeof(decimal),
                    typeof(double),
                    typeof(string),
                    typeof(decimal),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object;
        }

        public static bool IsSubclassOfRawGeneric (this Type toCheck, Type generic)
        {
            while (toCheck != null && toCheck != typeof(object))
            {
                var cur = toCheck.IsGenericType ? toCheck.GetGenericTypeDefinition() : toCheck;
                if (generic == cur) return true;
                toCheck = toCheck.BaseType;
            }

            return false;
        }

        public static bool AreFirstGenericArgumentsOfSameType (this Type first, Type second)
        {
            var firstGen = first.GetGenericArguments().First();
            var secondGen = second.GetGenericArguments().First();

            return firstGen == secondGen;
        }

        public static List<T> GetCustomAttributes <T> (this Type t, bool inherit) where T : Attribute
        {
            var attrs = t.GetCustomAttributes(typeof(T), inherit);

            return attrs.Cast<T>().ToList();
        }

        public static T GetCustomAttribute <T> (this Type t, bool inherit) where T : Attribute
        {
            return GetCustomAttributes<T>(t, inherit).SingleOrDefault();
        }

        public static List<T> GetCustomAttributes <T> (this FieldInfo f, bool inherit) where T : Attribute
        {
            var attrs = f.GetCustomAttributes(inherit);

            var casted = new List<T>();

            foreach (var attr in attrs)
            {
                if (attr is T tAttr)
                {
                    casted.Add(tAttr);
                }
            }

            return casted;
        }

        public static List<Type> GetInheritanceList (this Type type, Type stopTo, bool startFromTreeBase)
        {
            var types = new List<Type> {type};

            if (type == stopTo) return types;

            while (true)
            {
                var last = types.Last();

                types.Add(last.BaseType);

                if (last.BaseType == stopTo) break;
            }

            if (startFromTreeBase) types.Reverse();
            return types;
        }

        public static string NameWithGeneric (this Type type)
        {
            var name = type.Name;
            if (type.IsGenericType) name = name.Remove(name.Length - 2);

            var gens = type.GetGenericArguments().FormattedString(a => a.Name);

            return gens == string.Empty ? name : $"{name} [{gens}]";
        }

        public static bool HasParameterlessConstructor (this Type type)
        {
            return type.GetConstructors().Any(c => c.GetParameters().Length == 0) || type.IsValueType;
        }
    }
}