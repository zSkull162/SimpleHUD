#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SliderResetButton)), CanEditMultipleObjects]
public class SliderResetButtonEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty hudSettings;
    SerializedProperty slider;
    private void OnEnable()
    {
        hudSettings = serializedObject.FindProperty("hudSettings");
        slider = serializedObject.FindProperty("slider");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        InspectorUtils.TitleLabel(ThemeColor.Col2, "Slider Reset Button", true);

        serializedObject.Update();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col3, "System");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(hudSettings);
        EditorGUILayout.PropertyField(slider);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col4, "Info");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.Description("This script saves the slider's value on start, so that when this button is clicked any time after, it can reset the slider to it's original value.");
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif