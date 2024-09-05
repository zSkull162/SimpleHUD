#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HUDPositioner)), CanEditMultipleObjects]
public class HUDPositionerEditor : Editor
{
    SerializedProperty motionSmoothing;
    private void OnEnable()
    {
        motionSmoothing = serializedObject.FindProperty("motionSmoothing");
    }

    public override void OnInspectorGUI()
    {
        GUIStyle helpBox = new GUIStyle(EditorStyles.helpBox);
        GUIStyle richText = new GUIStyle(GUI.skin.label);
        richText.richText = true;
        GUIStyle description = new GUIStyle(GUI.skin.label);
        description.richText = true;
        description.wordWrap = true;

        InspectorUtils.TitleLabel(ThemeColor.Col1, "HUD Positioner", true);

        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col1, "Info");
        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.Description("This script will, <i>when in-game</i>, position the HUD in front of the player's view. However, there's also a smoothing option, which makes the HUD lag behind a little bit instead of directly following the player's head rotation.");
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();

        serializedObject.Update();
        EditorGUILayout.BeginVertical(helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col2, "Settings");
        EditorGUILayout.BeginVertical(helpBox);

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(motionSmoothing);
        if (motionSmoothing.floatValue != 18f)
        {
            if (GUILayout.Button("Reset to default value", GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth / 3.25f)))
            {
                motionSmoothing.floatValue = 18f;
            }
        }
        EditorGUILayout.EndHorizontal();
        if (motionSmoothing.floatValue >= 20f)
        {
            EditorGUILayout.LabelField("<color=red><b>Disabled</b></color>", richText);
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif