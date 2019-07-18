using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CircleImage), true)]
[CanEditMultipleObjects]
public class CircleImageEditor : UnityEditor.UI.ImageEditor
{
    SerializedProperty _fillPercent;
    SerializedProperty _segements;

    protected override void OnEnable()
    {
        base.OnEnable();
        _fillPercent = serializedObject.FindProperty("showPercent");
        _segements = serializedObject.FindProperty("segements");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        serializedObject.Update();
        EditorGUILayout.Slider(_fillPercent, 0, 1, new GUIContent("showPercent"));

        EditorGUILayout.PropertyField(_segements);

        serializedObject.ApplyModifiedProperties();
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }

    }
}
