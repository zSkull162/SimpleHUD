#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.SimpleHUD;

[CustomEditor(typeof(HUDPositioner)), CanEditMultipleObjects]
public class HUDPositionerEditor : Editor
{
    SerializedProperty motionSmoothing;
    HUDSettings controller;

    private void OnEnable() {
        motionSmoothing = serializedObject.FindProperty("motionSmoothing");

        // Find whether or not a HUD Settings controller is in the scene, because the inspector changes if there is or not.
        HUDSettings[] controllers = FindObjectsByType<HUDSettings>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        if (controllers != null && controllers.Length > 0) controller = controllers[0];
    }

    public override void OnInspectorGUI() {
        UdonSharpEditor.UdonSharpGUI.DrawProgramSource(target);
        EditorGUILayout.Space();

        string desc = "Positions the HUD at the player's head, with motion smoothing.";
        if (controller != null) desc += " The default motion smoothing value can be set from the HUD settings controller.";

        if (controller != null) {
            InspectorUtils.SetColor(ThemeColor.Col1);
            EditorGUILayout.HelpBox(desc, MessageType.Info);
            InspectorUtils.SetColor();
            return;
        }

        InspectorUtils.SetColor(ThemeColor.Col1);
        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
            InspectorUtils.SetColor();

            serializedObject.Update();
            EditorGUILayout.PropertyField(motionSmoothing);
            serializedObject.ApplyModifiedProperties();

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox(desc, MessageType.Info);
        }
    }
}
#endif