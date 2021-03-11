using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Chresimos.Core.Utils
{
    public static class EnumUtils
    {
        private static void CheckIsEnum <T> (bool withFlags)
        {
            if (typeof(T) == typeof(byte) || typeof(T) == typeof(int)) return;

            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException($"Type '{typeof(T).FullName}' is not an enum");
            }

            if (withFlags && !Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
            {
                throw new ArgumentException($"Type '{typeof(T).FullName}' doesn't have the 'Flags' attribute");
            }
        }

        public static bool IsFlagSet <T> (this T value, T flag) where T : struct
        {
            CheckIsEnum<T>(true);

            var lValue = Convert.ToInt64(value);
            var lFlag = Convert.ToInt64(flag);
            return (lValue & lFlag) == lFlag;
        }

        public static IEnumerable<T> GetFlags <T> (this T value) where T : struct
        {
            CheckIsEnum<T>(true);
            foreach (var flag in Enum.GetValues(typeof(T)).Cast<T>())
            {
                if (value.IsFlagSet(flag))
                {
                    yield return flag;
                }
            }
        }

        public static T SetFlags <T> (this T value, T flags, bool on) where T : struct
        {
            CheckIsEnum<T>(true);
            var lValue = Convert.ToInt64(value);
            var lFlag = Convert.ToInt64(flags);
            if (on)
            {
                lValue |= lFlag;
            }
            else
            {
                lValue &= ~lFlag;
            }

            return (T) Enum.ToObject(typeof(T), lValue);
        }

        public static T SetFlags <T> (this T value, T flags) where T : struct
        {
            return value.SetFlags(flags, true);
        }

        public static T ClearFlags <T> (this T value, T flags) where T : struct
        {
            return value.SetFlags(flags, false);
        }

        public static T CombineFlags <T> (this IEnumerable<T> flags) where T : struct
        {
            CheckIsEnum<T>(true);
            long lValue = 0;
            foreach (var flag in flags)
            {
                var lFlag = Convert.ToInt64(flag);
                lValue |= lFlag;
            }

            return (T) Enum.ToObject(typeof(T), lValue);
        }

        public static string GetDescription <T> (this T value) where T : struct
        {
            CheckIsEnum<T>(false);
            var name = Enum.GetName(typeof(T), value);
            if (name != null)
            {
                var field = typeof(T).GetField(name);
                if (field != null)
                {
                    var attr =
                        Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }

            return null;
        }
    }
}