#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;
using zSkull162.SimpleHUD;
using System.Reflection;
using System;

[CustomEditor(typeof(HUDSettings)), CanEditMultipleObjects]
public class HUDSettingsEditor : Editor
{
    #region Serialized properties
    SerializedProperty hudScript;
    SerializedProperty hudAnimator;
    SerializedProperty settingsAnimator;
    SerializedProperty centerFillX;
    SerializedProperty posXSlider;
    SerializedProperty posXText;
    SerializedProperty posXDefault;
    SerializedProperty centerFillY;
    SerializedProperty posYSlider;
    SerializedProperty posYText;
    SerializedProperty posYDefault;
    SerializedProperty scaleSlider;
    SerializedProperty scaleText;
    SerializedProperty scaleDefault;
    SerializedProperty distanceSlider;
    SerializedProperty distanceText;
    SerializedProperty distanceDefault;
    SerializedProperty smoothingSlider;
    SerializedProperty smoothingText;
    SerializedProperty smoothingDefaultVR;
    SerializedProperty smoothingDefaultDesktop;
    SerializedProperty fovXSlider;
    SerializedProperty fovXText;
    SerializedProperty fovXDefault;
    SerializedProperty fovYSlider;
    SerializedProperty fovYText;
    SerializedProperty fovYDefault;
    SerializedProperty usePersistence;
    SerializedProperty textDecimalPlaces;

    GUIStyle label;
    bool examples;
    MethodInfo[] publicMethods;

    private void OnEnable() {
        hudScript = serializedObject.FindProperty("hudScript");
        hudAnimator = serializedObject.FindProperty("hudAnimator");
        settingsAnimator = serializedObject.FindProperty("settingsAnimator");
        centerFillX = serializedObject.FindProperty("centerFillX");
        posXSlider = serializedObject.FindProperty("posXSlider");
        posXText = serializedObject.FindProperty("posXText");
        posXDefault = serializedObject.FindProperty("posXDefault");
        centerFillY = serializedObject.FindProperty("centerFillY");
        posYSlider = serializedObject.FindProperty("posYSlider");
        posYText = serializedObject.FindProperty("posYText");
        posYDefault = serializedObject.FindProperty("posYDefault");
        scaleSlider = serializedObject.FindProperty("scaleSlider");
        scaleText = serializedObject.FindProperty("scaleText");
        scaleDefault = serializedObject.FindProperty("scaleDefault");
        distanceSlider = serializedObject.FindProperty("distanceSlider");
        distanceText = serializedObject.FindProperty("distanceText");
        distanceDefault = serializedObject.FindProperty("distanceDefault");
        smoothingSlider = serializedObject.FindProperty("smoothingSlider");
        smoothingText = serializedObject.FindProperty("smoothingText");
        smoothingDefaultVR = serializedObject.FindProperty("smoothingDefaultVR");
        smoothingDefaultDesktop = serializedObject.FindProperty("smoothingDefaultDesktop");
        fovXSlider = serializedObject.FindProperty("fovXSlider");
        fovXText = serializedObject.FindProperty("fovXText");
        fovXDefault = serializedObject.FindProperty("fovXDefault");
        fovYSlider = serializedObject.FindProperty("fovYSlider");
        fovYText = serializedObject.FindProperty("fovYText");
        fovYDefault = serializedObject.FindProperty("fovYDefault");
        usePersistence = serializedObject.FindProperty("usePersistence");
        textDecimalPlaces = serializedObject.FindProperty("textDecimalPlaces");

        Type t = target.GetType();
        publicMethods = t.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

        HUDSettings script = (HUDSettings)target;
        script.UpdateSliders();
    }
    #endregion

    public override void OnInspectorGUI() {
        UdonSharpEditor.UdonSharpGUI.DrawProgramSource(target);
        EditorGUILayout.Space();

        HUDSettings script = (HUDSettings)target;
        label = new GUIStyle(EditorStyles.boldLabel) {
            fontSize = 13,
            stretchHeight = true
        };

        serializedObject.Update();

        #region References
        InspectorUtils.SetColor(ThemeColor.Col1);
        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
            InspectorUtils.SetColor();

            InspectorUtils.SectionLabel(ThemeColor.Col1, "References");
            EditorGUILayout.PropertyField(hudScript);
            EditorGUILayout.PropertyField(hudAnimator);
            EditorGUILayout.PropertyField(settingsAnimator);
        }
        #endregion
        EditorGUILayout.Space();

        #region Sliders
        InspectorUtils.SetColor(ThemeColor.Col2);
        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
            InspectorUtils.SetColor();
            script.slidersVisible = GUILayout.Toggle(script.slidersVisible, "Sliders", EditorStyles.foldout, GUILayout.Height(EditorGUIUtility.singleLineHeight + 2f));
            if (script.slidersVisible) {
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
                    SliderField("Horizontal Position", centerFillX, posXSlider, posXText, posXDefault);
                    EditorGUILayout.Space();
                    SliderField("Vertical Position", centerFillY, posYSlider, posYText, posYDefault);
                    EditorGUILayout.Space();
                    SliderField("Horizontal FOV", fovXSlider, fovXText, fovXDefault);
                    EditorGUILayout.Space();
                    SliderField("Vertical FOV", fovYSlider, fovYText, fovYDefault);
                    EditorGUILayout.Space();
                    SliderField("Scale", scaleSlider, scaleText, scaleDefault);
                    EditorGUILayout.Space();
                    SliderField("Distance", distanceSlider, distanceText, distanceDefault);
                    EditorGUILayout.Space();
                    SliderField("Motion Smoothing", smoothingSlider, smoothingText, smoothingDefaultDesktop, smoothingDefaultVR);
                    // Repaint is called here, because the default values are controlled by the sliders during playmode.
                    // Repaint will make those values visually update in the inspector as soon as they're changed, otherwise it would only happen when you hover the inspector.
                    if (Application.isPlaying) Repaint();
                }
            }
        }
        #endregion
        EditorGUILayout.Space();

        #region Options
        InspectorUtils.SetColor(ThemeColor.Col3);
        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
            InspectorUtils.SetColor();

            InspectorUtils.SectionLabel(ThemeColor.Col3, "Options");

            if (Application.isPlaying) {
                GUILayout.Space(2);
                using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
                    InspectorUtils.SectionLabel(ThemeColor.Col4, "Playmode Only");
                    GUILayout.Space(2);
                    script.matchSliders = GUILayout.Toggle(
                        script.matchSliders,
                        new GUIContent(
                            "Match default values to UI sliders",
                            "If enabled, the UI sliders will control the default values. This makes it easier to set the defaults in edit mode."
                        )
                    );
                    GUILayout.Space(2);
                }
            }

            GUILayout.Space(2);
            // EditorGUILayout.PropertyField(usePersistence);
            // A property field will place the checkbox on the right, whereas a GUILayout.Toggle will place it on the left, which i think looks better here.
            usePersistence.boolValue = GUILayout.Toggle(usePersistence.boolValue, "Use Persistence");
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(textDecimalPlaces);
            GUILayout.Space(2);
            using (new EditorGUILayout.HorizontalScope()) {
                GUILayout.Space(14);
                examples = GUILayout.Toggle(examples, "Examples", EditorStyles.foldout);
            }
            if (examples) {
                float eg1 = 1.5f;
                float eg2 = 3.14159f;
                float eg3 = 5.55f;
                string format = $"F{textDecimalPlaces.intValue}";

                EditorGUI.indentLevel += 2;
                EditorGUILayout.LabelField(eg1.ToString(format) + " | " + eg2.ToString(format) + " | " + eg3.ToString(format));
                EditorGUI.indentLevel -= 2;
            }
            GUILayout.Space(2);
        }
        #endregion
        EditorGUILayout.Space();

        #region Methods info
        Rect rect = EditorGUILayout.GetControlRect();
        if (!script.helpVisible) {
            rect.y += 1f;
            if (GUI.Button(rect, EditorGUIUtility.IconContent("_Help"), GUIStyle.none)) {
                script.helpVisible = !script.helpVisible;
            }
            rect.y -= 1f;
            rect.x += 20;
            EditorGUI.LabelField(rect, "Public Methods");
        }
        else {
            rect.y += 1f;
            if (GUI.Button(rect, EditorGUIUtility.IconContent("_Help"), GUIStyle.none)) {
                script.helpVisible = !script.helpVisible;
            }
            rect.y -= 1f;
            rect.x += 20;
            EditorGUI.LabelField(rect, "Public Methods", label);
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
                EditorGUI.indentLevel += 1;
                EditorGUILayout.LabelField("Hover method names for a description, click to copy to clipboard.");
                GUILayout.Space(6);
                foreach (MethodInfo method in publicMethods) {
                    if (method.IsSpecialName) continue; // Don't display getters/setters
                    else if (IsOverride(method)) continue; // Don't display override methods
                    string desc = GetMethodDescription(method);
                    if (desc.Contains("_HiddenMethod")) continue; // Don't display methods with the description "_HiddenMethod"

                    MethodLabel(method.Name, desc);
                }
                EditorGUI.indentLevel -= 1;
            }
        }
        #endregion
        serializedObject.ApplyModifiedProperties();
    }

    #region Helper methods
    void MethodLabel(string name, string tooltip) {
        Rect rect = EditorGUILayout.GetControlRect();

        EditorGUI.LabelField(rect, new GUIContent(name + "()", tooltip), EditorStyles.objectFieldThumb);
        rect.x += 14f;
        rect.width -= 14f;

        GUI.backgroundColor = new Color(0.75f, 0.75f, 0.75f, 0.075f);
        if (GUI.Button(rect, GUIContent.none)) {
            EditorGUIUtility.systemCopyBuffer = name;
        }
        GUI.backgroundColor = Color.white;
    }

    string GetMethodDescription(MethodInfo method) {
        DescriptionAttribute attribute = method.GetCustomAttribute<DescriptionAttribute>();
        return attribute != null ? attribute.Description : "No Description.";
    }

    bool IsOverride(MethodInfo method) => method.GetBaseDefinition().DeclaringType != method.DeclaringType;

    void SliderField(string name, SerializedProperty slider, SerializedProperty text, SerializedProperty defaultValue) {
        EditorGUILayout.LabelField(name, label);
        EditorGUI.indentLevel += 1;
        EditorGUILayout.PropertyField(slider);
        EditorGUILayout.PropertyField(text);
        EditorGUILayout.PropertyField(defaultValue);
        EditorGUI.indentLevel -= 1;
    }

    void SliderField(string name, SerializedProperty slider, SerializedProperty text, SerializedProperty defaultValue1, SerializedProperty defaultValue2) {
        EditorGUILayout.LabelField(name, label);
        EditorGUI.indentLevel += 1;
        EditorGUILayout.PropertyField(slider);
        EditorGUILayout.PropertyField(text);
        EditorGUILayout.PropertyField(defaultValue1);
        EditorGUILayout.PropertyField(defaultValue2);
        EditorGUI.indentLevel -= 1;
    }
    #endregion
}
#endif