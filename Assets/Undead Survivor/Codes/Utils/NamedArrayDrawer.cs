#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        try
        {
            int index = int.Parse(property.propertyPath.Split('[', ']')[1]);
            EditorGUI.PropertyField(position, property, new GUIContent(((NamedArrayAttribute)attribute).names[index]));
        }
        catch
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif