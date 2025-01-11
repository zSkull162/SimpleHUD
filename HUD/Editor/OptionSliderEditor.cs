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
        OptionSlider script = (OptionSlider)target;
        Color baseColor = GUI.backgroundColor;
        InspectorUtils.TitleLabel(ThemeColor.Col3, "Option Slider", true);

        serializedObject.Update();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col4, "Variables");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(hudSettings);
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.clear;
        if (GUILayout.Button("?", GUILayout.Width(18))) { script.infoGroup = !script.infoGroup; }
        GUI.backgroundColor = baseColor;
        EditorGUILayout.PropertyField(eventType);
        EditorGUILayout.EndHorizontal();
        if (script.infoGroup)
        {
            switch (eventType.enumValueIndex)
            {
                case 0:
                    EditorGUILayout.HelpBox("Calls \"SetPosX\" on HUD Settings, using the slider's value.", MessageType.Info);
                    break;
                case 1:
                    EditorGUILayout.HelpBox("Calls \"SetPosY\" on HUD Settings, using the slider's value.", MessageType.Info);
                    break;
                case 2:
                    EditorGUILayout.HelpBox("Calls \"SetDistance\" on HUD Settings, using the slider's value.", MessageType.Info);
                    break;
                case 3:
                    EditorGUILayout.HelpBox("Calls \"SetScale\" on HUD Settings, using the slider's value.", MessageType.Info);
                    break;
                case 4:
                    EditorGUILayout.HelpBox("Calls \"SetSmooth\" on HUD Settings, using the slider's absolute value.", MessageType.Info);
                    break;
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif