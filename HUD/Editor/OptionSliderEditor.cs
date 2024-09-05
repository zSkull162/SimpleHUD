#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OptionSlider)), CanEditMultipleObjects]
public class OptionSliderEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty hudSettings;
    SerializedProperty eventType;
    private void OnEnable()
    {
        hudSettings = serializedObject.FindProperty("hudSettings");
        eventType = serializedObject.FindProperty("eventType");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        InspectorUtils.TitleLabel(ThemeColor.Col3, "Option Slider", true);

        serializedObject.Update();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col4, "Object");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(hudSettings);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col5, "Event");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(eventType);
        EditorGUILayout.Space(2);
        if (eventType.enumValueIndex == 0)
        {
            // InspectorUtils.Description(ThemeColor.Col5, "Calls \"SetPosX\" using the slider's value on HUD Settings.");
            EditorGUILayout.HelpBox("Calls \"SetPosX\" using the slider's value on HUD Settings.", MessageType.Info);
        }
        else if (eventType.enumValueIndex == 1)
        {
            // InspectorUtils.Description(ThemeColor.Col5, "Calls \"SetPosY\" using the slider's value on HUD Settings.");
            EditorGUILayout.HelpBox("Calls \"SetPosY\" using the slider's value on HUD Settings.", MessageType.Info);
        }
        else if (eventType.enumValueIndex == 2)
        {
            // InspectorUtils.Description(ThemeColor.Col5, "Calls \"SetDistance\" using the slider's value on HUD Settings.");
            EditorGUILayout.HelpBox("Calls \"SetDistance\" using the slider's value on HUD Settings.", MessageType.Info);
        }
        else if (eventType.enumValueIndex == 3)
        {
            // InspectorUtils.Description(ThemeColor.Col5, "Calls \"SetScale\" using the slider's value on HUD Settings.");
            EditorGUILayout.HelpBox("Calls \"SetScale\" using the slider's value on HUD Settings.", MessageType.Info);
        }
        else if (eventType.enumValueIndex == 4)
        {
            // InspectorUtils.Description(ThemeColor.Col5, "Calls \"SetSmooth\" using the slider's absolute value on HUD Settings.");
            EditorGUILayout.HelpBox("Calls \"SetSmooth\" using the slider's absolute value on HUD Settings.", MessageType.Info);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif