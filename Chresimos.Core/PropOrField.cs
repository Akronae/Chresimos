using System;
using System.Reflection;

namespace Chresimos.Core
{
    public class PropOrField
    {
        public readonly MemberInfo MemberInfo;

        public PropOrField (MemberInfo memberInfo)
        {
            if (!(memberInfo is PropertyInfo) && !(memberInfo is FieldInfo))
            {
                throw new Exception(
                    $"{nameof(memberInfo)} must either be {nameof(PropertyInfo)} or {nameof(FieldInfo)}");
            }

            MemberInfo = memberInfo;
        }
        
        public PropOrField (object fromObject, string memberName)
        {
            if (fromObject.GetType().GetField(memberName) is { } f) MemberInfo = f;
            else if (fromObject.GetType().GetProperty(memberName) is { } p) MemberInfo = p;
        }

        public object GetValue (object source)
        {
            if (MemberInfo is PropertyInfo propertyInfo) return propertyInfo.GetValue(source);
            if (MemberInfo is FieldInfo fieldInfo) return fieldInfo.GetValue(source);

            return null;
        }

        public void SetValue (object target, object value)
        {
            if (MemberInfo is PropertyInfo propertyInfo) propertyInfo.SetValue(target, value);
            if (MemberInfo is FieldInfo fieldInfo) fieldInfo.SetValue(target, value);
        }
        
        public T1 SetStructValue <T1> (T1 target, object value)
        {
            object boxed = target;
            
            SetValue(boxed, value);

            return (T1) boxed;
        }

        public Type GetMemberType ()
        {
            if (MemberInfo is PropertyInfo propertyInfo) return propertyInfo.PropertyType;
            if (MemberInfo is FieldInfo fieldInfo) return fieldInfo.FieldType;

            return null;
        }
    }
}