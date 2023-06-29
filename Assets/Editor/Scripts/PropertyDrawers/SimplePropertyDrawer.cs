using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(SimplePropertyAttribute))]
public class SimplePropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var targetObject = property.serializedObject.targetObject;
        var targetObjectClassType = targetObject.GetType();
        var field = targetObjectClassType.GetField(property.propertyPath);

        if (field != null)
        {
            var value = field.GetValue(targetObject);
            EditorGUI.LabelField(position, ObjectNames.NicifyVariableName(property.name), value.ToString());
        }
    }
}