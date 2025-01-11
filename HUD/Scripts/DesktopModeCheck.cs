#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEditor;
#endif
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class DesktopModeCheck : UdonSharpBehaviour
{
    private UnityEngine.UI.Image image;

    void Start()
    {
        image = this.GetComponent<UnityEngine.UI.Image>();

        if (image != null) CheckPlatform();
        else zLogger.LogError(name, "UI Image not found!", LogColor.Red);
    }

    private void CheckPlatform() => image.enabled = !Networking.LocalPlayer.IsUserInVR();
}

#if UNITY_EDITOR && !COMPILER_UDONSHARP
[CustomEditor(typeof(DesktopModeCheck)), CanEditMultipleObjects]
public class DesktopModeCheckEditor : Editor
{
    public override void OnInspectorGUI()
    {
        InspectorUtils.TitleLabel(ThemeColor.Col2, "Desktop Checker", true);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col3, "Info");
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.Description("When the game/world starts, this script will check whether or not the player is in VR. If so, the VR Distance slider will be interactable. If not, the slider will be blocked by this UI Image.");
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndVertical();
    }
}
#endif