using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Utility.Editor
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (MeetsConditions(property))
            {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (!MeetsConditions(property)) return 0;
            var showIfAttribute = (ShowIfAttribute)attribute;
            return base.GetPropertyHeight(property, label) * showIfAttribute.HeightMultiplier;
        }

        private bool MeetsConditions(SerializedProperty property)
        {
            var showIfAttribute = (ShowIfAttribute)attribute;
            var target = SerializedPropertyHelpers.GetPropertyPathObject(property);

            var condition = showIfAttribute.Condition;
            var conditionField = ReflectionHelpers.FindField(5, new HashSet<object>() { target }, condition, out var newTarget);
            if (conditionField == null)
            {
                return true;
            }
            var conditionValue = conditionField.GetValue(newTarget);

            return (bool) conditionValue == showIfAttribute.Value;
        }
    }
}