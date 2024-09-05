#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HUDToggleButton)), CanEditMultipleObjects]
public class HUDToggleButtonEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty hudSettings;
    SerializedProperty _target;
    private void OnEnable()
    {
        hudSettings = serializedObject.FindProperty("hudSettings");
        _target = serializedObject.FindProperty("target");
    }
    #endregion


    public override void OnInspectorGUI()
    {
        InspectorUtils.TitleLabel(ThemeColor.Col1, "HUD Toggle button", true);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col2, "System");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(hudSettings);
        EditorGUILayout.PropertyField(_target);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
    }
}
#endif