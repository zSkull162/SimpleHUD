#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DesktopModeCheck)), CanEditMultipleObjects]
public class DesktopModeCheckEditor : Editor
{
    public override void OnInspectorGUI()
    {
        InspectorUtils.TitleLabel(ThemeColor.Col2, "Desktop Checker", true);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col3, "Info");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.Description("When the game/world starts, this script will check whether or not the player is in VR. If so, the VR Distance slider will be interactable, and if not, the VR Distance slider will <i>not</i> be interactable.");
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
    }
}
#endif