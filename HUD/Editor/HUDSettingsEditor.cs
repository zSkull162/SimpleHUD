#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HUDSettings)), CanEditMultipleObjects]
public class HUDSettingsEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty hudObject;
    SerializedProperty hudAnimator;
    SerializedProperty sliders;
    SerializedProperty onColor;
    SerializedProperty offColor;
    private void OnEnable()
    {
        hudObject = serializedObject.FindProperty("hudObject");
        hudAnimator = serializedObject.FindProperty("hudAnimator");
        sliders = serializedObject.FindProperty("sliders");
        onColor = serializedObject.FindProperty("togglesOnColor");
        offColor = serializedObject.FindProperty("togglesOffColor");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        InspectorUtils.TitleLabel(ThemeColor.Col1, "HUD Settings", true);

        serializedObject.Update();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col2, "System");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(hudObject);
        EditorGUILayout.PropertyField(hudAnimator);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col3, "Objects");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(sliders);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col4, "Toggles");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(onColor);
        EditorGUILayout.PropertyField(offColor);
        EditorGUILayout.Space(2);
        EditorGUILayout.HelpBox("The toggle colors are only relevant if this HUD settings UI includes toggles.", MessageType.Info);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif