using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(FloatingPoint))]
public class FloatingPointDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Split the position into three equal parts for x, y, and z values
        float singleFieldWidth = position.width / 3f;

        // Create sub-properties for x, y, and z
        SerializedProperty xProp = property.FindPropertyRelative("x");
        SerializedProperty yProp = property.FindPropertyRelative("y");
        SerializedProperty zProp = property.FindPropertyRelative("z");

        // Draw labels for x, y, and z
        Rect xLabelRect = new Rect(position.x, position.y, singleFieldWidth, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(xLabelRect, "X:");

        Rect yLabelRect = new Rect(position.x + singleFieldWidth + 5, position.y, singleFieldWidth, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(yLabelRect, "Y:");

        Rect zLabelRect = new Rect(position.x + 2 * singleFieldWidth + 5, position.y, singleFieldWidth, EditorGUIUtility.singleLineHeight);
        EditorGUI.LabelField(zLabelRect, "Z:");

        // Draw the x, y, and z fields side by side
        Rect xRect = new Rect(position.x + (singleFieldWidth * 0.15f), position.y, singleFieldWidth * 0.8f, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(xRect, xProp, GUIContent.none);

        Rect yRect = new Rect(position.x + singleFieldWidth + (singleFieldWidth * 0.15f) + 5, position.y, singleFieldWidth * 0.8f, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(yRect, yProp, GUIContent.none);

        Rect zRect = new Rect(position.x + 2 * singleFieldWidth + (singleFieldWidth * 0.15f) + 5, position.y, singleFieldWidth * 0.8f, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(zRect, zProp, GUIContent.none);

        EditorGUI.EndProperty();
    }
}
