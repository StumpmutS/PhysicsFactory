using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditor;
using Utility.Editor;

public static class SerializedPropertyHelpers
{
    public static object GetPropertyPathObject(SerializedProperty property)
    {
        object target = property.serializedObject.targetObject;
        if (!property.propertyPath.Contains('.')) return target;
        var fieldNames = property.propertyPath.Replace("Array.", "").Split('.').SkipLast(1);
        
        //Move through hierarchy to get object of attributed field
        foreach (var fieldName in fieldNames)
        {
            if (fieldName.StartsWith("data[") && target is IEnumerable enumerable)
            {
                var index = int.Parse(fieldName.Substring(5, fieldName.Length - 6));
                var enumerator = enumerable.GetEnumerator();
                for (int i = -1; i < index; i++)
                {
                    enumerator.MoveNext();
                }

                target = enumerator.Current;
            }
            else
            {
                var type = target.GetType();
                ReflectionHelpers.TryGetInheritedField(fieldName, type, out var info);
                target = info.GetValue(target);
            }
        }

        return target;
    }
}
