#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HUDDefaultSettings))]
public class HUDDefaultSettingsEditor : Editor
{
    #region Get Serialized Properties
    SerializedProperty hudAnimator;
    SerializedProperty hudObject;
    SerializedProperty defaultPosX;
    SerializedProperty defaultPosY;
    SerializedProperty defaultScale;
    SerializedProperty defaultDistance;
    SerializedProperty defaultSmoothing;
    private void OnEnable()
    {
        hudAnimator = serializedObject.FindProperty("hudAnimator");
        hudObject = serializedObject.FindProperty("hudObject");
        defaultPosX = serializedObject.FindProperty("defaultPosX");
        defaultPosY = serializedObject.FindProperty("defaultPosY");
        defaultScale = serializedObject.FindProperty("defaultScale");
        defaultDistance = serializedObject.FindProperty("defaultDistance");
        defaultSmoothing = serializedObject.FindProperty("defaultSmoothing");
    }
    #endregion

    public override void OnInspectorGUI()
    {
        InspectorUtils.TitleLabel(ThemeColor.Col1, "HUD Default Settings", true);

        serializedObject.Update();
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col2, "System");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(hudAnimator);
        EditorGUILayout.PropertyField(hudObject);
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col3, "Settings");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.PropertyField(defaultPosX);
        EditorGUILayout.PropertyField(defaultPosY);
        EditorGUILayout.PropertyField(defaultScale);
        EditorGUILayout.PropertyField(defaultDistance);
        if (defaultSmoothing.floatValue < 20f)
        {
            EditorGUILayout.PropertyField(defaultSmoothing);
        }
        else
        {
            GUIStyle richText = new GUIStyle(GUI.skin.label);
            richText.richText = true;
            GUILayoutOption maxWidth = GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth / 2f);

            EditorGUILayout.PropertyField(defaultSmoothing);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("<i>Motion smoothing disabled.</i>", richText, maxWidth);
            if (GUILayout.Button("Reset to default value"))
            {
                defaultSmoothing.floatValue = 18f;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif