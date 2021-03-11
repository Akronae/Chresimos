using System;
using System.Collections.Generic;
using System.Reflection;

namespace Chresimos.Core.Utils
{
    public static class MemberUtils
    {
        public static Type GetValueType (this MemberInfo memberInfo)
        {
            switch (memberInfo)
            {
                case PropertyInfo propertyInfo:
                    return propertyInfo.PropertyType;
                case FieldInfo fieldInfo:
                    return fieldInfo.FieldType;
                default:
                    throw new ArgumentOutOfRangeException(nameof(memberInfo));
            }
        }

        public static void SetValue (this MemberInfo memberInfo, object instance, object value)
        {
            switch (memberInfo)
            {
                case FieldInfo field:
                    field.SetValue(instance, value);
                    break;
                case PropertyInfo prop:
                    prop.SetValue(instance, value, null);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(memberInfo));
            }
        }

        public static object GetValue (this MemberInfo memberInfo, object instance)
        {
            switch (memberInfo)
            {
                case FieldInfo field:
                    return field.GetValue(instance);
                case PropertyInfo prop:
                    return prop.GetValue(instance, null);

                default:
                    throw LogUtils.Throw(new ArgumentOutOfRangeException(nameof(memberInfo)));
            }
        }

        public static List<T> GetCustomAttributes <T> (this PropertyInfo p, bool inherit) where T : Attribute
        {
            var attrs = p.GetCustomAttributes(inherit);

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
    }
}