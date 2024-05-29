using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

[CustomPropertyDrawer(typeof(FloatingPoint))]
public class FloatingPointDrawer : OdinValueDrawer<FloatingPoint>
{
    protected override void DrawPropertyLayout(GUIContent label)
    {
        var value = this.ValueEntry.SmartValue;

        SirenixEditorGUI.BeginBox();
        SirenixEditorGUI.BeginBoxHeader();

        if (label != null)
        {
            EditorGUILayout.LabelField(label);
        }

        SirenixEditorGUI.EndBoxHeader();

        EditorGUILayout.BeginHorizontal();

        // Draw the X field
        EditorGUILayout.LabelField("X:", GUILayout.Width(20));
        value.x = EditorGUILayout.DoubleField(value.x, GUILayout.MinWidth(100));

        // Draw the Y field
        EditorGUILayout.LabelField("Y:", GUILayout.Width(20));
        value.y = EditorGUILayout.DoubleField(value.y, GUILayout.MinWidth(100));

        // Draw the Z field
        EditorGUILayout.LabelField("Z:", GUILayout.Width(20));
        value.z = EditorGUILayout.DoubleField(value.z, GUILayout.MinWidth(100));

        EditorGUILayout.EndHorizontal();

        SirenixEditorGUI.EndBox();
    }
}
