#if UNITY_EDITOR && !COMPILER_UDONSHARP
using UnityEngine;
using UnityEditor;

public class ThemePicker : MonoBehaviour
{
    [Tooltip("The scriptable object that stores your selected theme, so it won't reset when you close unity or scripts re-compile.")]
    public ThemeScriptableObject themeContainer;
    [Tooltip("The scriptable object which will save a custom theme, or load a custom theme from.")]
    public ThemeScriptableObject customThemeContainer;

    // Base theme
    public string[] theme1 = { "#008080", "#38c194", "#31bdb6", "#2c99bc", "#3775bf" };

    // Watermelon
    public string[] theme2 = { "#008080", "#38c194", "#c2384c", "#c84183", "#cf59af" };

    // Sunset
    public string[] theme3 = { "#1f61a3", "#9b419c", "#f5756a", "#ffa254", "#fee79b" };

    // Bubblegum
    public string[] theme4 = { "#F5E3E0", "#E2A2AC", "#D2709C", "#A05A75", "#7E63A5" };

    // Solar
    public string[] theme5 = { "#994A46", "#C43D36", "#D8572A", "#DB7C26", "#F7B538" };

    // Custom theme options
    public Color32 custom1 = new Color32(0, 128, 128, 255);
    public Color32 custom2 = new Color32(56, 193, 148, 255);
    public Color32 custom3 = new Color32(49, 189, 182, 255);
    public Color32 custom4 = new Color32(44, 153, 188, 255);
    public Color32 custom5 = new Color32(55, 117, 191, 255);

    // Bools for the inspector
    public bool isThemeCustom;
    public bool useCustom;
    public bool preview;
}

[CustomEditor(typeof(ThemePicker))]
public class ThemeOptionsEditor : Editor
{
    ThemePicker script;
    #region Get Serialized Properties
    SerializedProperty custom1;
    SerializedProperty custom2;
    SerializedProperty custom3;
    SerializedProperty custom4;
    SerializedProperty custom5;
    SerializedProperty themeContainer;
    SerializedProperty customThemeContainer;
    Color baseColor;

    private void OnEnable()
    {
        custom1 = serializedObject.FindProperty("custom1");
        custom2 = serializedObject.FindProperty("custom2");
        custom3 = serializedObject.FindProperty("custom3");
        custom4 = serializedObject.FindProperty("custom4");
        custom5 = serializedObject.FindProperty("custom5");
        themeContainer = serializedObject.FindProperty("themeContainer");
        customThemeContainer = serializedObject.FindProperty("customThemeContainer");
        baseColor = GUI.backgroundColor;
    }
    #endregion

    public override void OnInspectorGUI()
    {
        script = (ThemePicker)target;
        if (script == null) return;

        GUIStyle richText = new GUIStyle(GUI.skin.label);
        richText.richText = true;
        richText.wordWrap = true;
        GUIStyle checkboxStyle = EditorStyles.toggle;
        checkboxStyle.fontStyle = FontStyle.Bold;
        GUIStyle foldoutStyle = EditorStyles.foldout;
        foldoutStyle.fontStyle = FontStyle.Bold;

        serializedObject.Update();

        InspectorUtils.TitleLabel(ThemeColor.Col1, "Editor UI Theme Picker", false);
        EditorGUILayout.Space(2);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col1, "Theme Data");

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.HelpBox(
            "This is important. It store your currently selected theme, so that it won't get reset when scripts compile, or when you exit Unity.</color> " +
            "<color=#8c8c8c><i>This theme picker also couldn't do anything without this, as it writes your selected theme to this ScriptableObject.</i></color>",
            MessageType.Info
        );
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(3);
        EditorGUILayout.PropertyField(themeContainer);
        EditorGUILayout.Space(1);
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(2);

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col2, "Preset Themes");

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        ThemeButton(script.theme1, 1);
        EditorGUILayout.Space(2);
        ThemeButton(script.theme2, 2);
        EditorGUILayout.Space(2);
        ThemeButton(script.theme3, 3);
        EditorGUILayout.Space(2);
        ThemeButton(script.theme4, 4);
        EditorGUILayout.Space(2);
        ThemeButton(script.theme5, 5);
        EditorGUILayout.EndVertical();

        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(2);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col3, "Custom Theme");
        EditorGUI.BeginChangeCheck();
        script.useCustom = GUILayout.Toggle(script.useCustom, "Enable custom theme", checkboxStyle);
        if (EditorGUI.EndChangeCheck())
        {
            if (script.useCustom == false && script.isThemeCustom == true)
            {
                script.themeContainer.storedTheme = script.theme1;
            }
        }
        EditorGUILayout.Space(2);
        if (script.useCustom)
        {
            #region Theme color fields
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            script.custom1 = EditorGUILayout.ColorField(new GUIContent("Color 1"), script.custom1, showEyedropper: true, showAlpha: false, hdr: false);
            script.custom2 = EditorGUILayout.ColorField(new GUIContent("Color 2"), script.custom2, showEyedropper: true, showAlpha: false, hdr: false);
            script.custom3 = EditorGUILayout.ColorField(new GUIContent("Color 3"), script.custom3, showEyedropper: true, showAlpha: false, hdr: false);
            script.custom4 = EditorGUILayout.ColorField(new GUIContent("Color 4"), script.custom4, showEyedropper: true, showAlpha: false, hdr: false);
            script.custom5 = EditorGUILayout.ColorField(new GUIContent("Color 5"), script.custom5, showEyedropper: true, showAlpha: false, hdr: false);
            EditorGUILayout.Space(2);
            if (GUILayout.Button("Apply theme!"))
            {
                string col1 = $"#{ ColorUtility.ToHtmlStringRGB(script.custom1) }";
                string col2 = $"#{ ColorUtility.ToHtmlStringRGB(script.custom2) }";
                string col3 = $"#{ ColorUtility.ToHtmlStringRGB(script.custom3) }";
                string col4 = $"#{ ColorUtility.ToHtmlStringRGB(script.custom4) }";
                string col5 = $"#{ ColorUtility.ToHtmlStringRGB(script.custom5) }";
                string[] customTheme = { col1, col2, col3, col4, col5 };

                script.themeContainer.storedTheme = customTheme;
                EditorUtility.SetDirty(script.themeContainer);
                if (!script.isThemeCustom) script.isThemeCustom = true;
                zLogger.Log("Theme Picker", $"Custom theme applied!", LogColor.Aqua, false);
            }
            #endregion

            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);

            #region Theme save/load
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            InspectorUtils.SectionLabel(ThemeColor.Col3, "Custom Theme save/load");
            EditorGUILayout.HelpBox("To save your custom theme, click the \"Save theme\" button. To load a custom theme, place a Theme Container from the assets into the slot on the left, and click \"Load theme\"", MessageType.Info);
            EditorGUILayout.Space(2);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(customThemeContainer, GUIContent.none, GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth / 3.25f));
            if (GUILayout.Button("Save theme"))
            {
                if (!script.isThemeCustom)
                {
                    zLogger.Log("Theme Picker", "Custom theme not currently applied!", LogColor.Red, false);
                    GUIUtility.ExitGUI();
                    return;
                }
                
                ThemeScriptableObject newThemeSO = ScriptableObject.CreateInstance<ThemeScriptableObject>();
                string path = EditorUtility.SaveFilePanelInProject("Save custom theme", "CustomTheme", "asset", "Save custom theme");
                if (path == "" || path == null)
                {
                    AssetDatabase.CreateAsset(newThemeSO, "Assets");
                }
                else
                {
                    AssetDatabase.CreateAsset(newThemeSO, path);
                }

                string col1 = $"#{ColorUtility.ToHtmlStringRGB(script.custom1)}";
                string col2 = $"#{ColorUtility.ToHtmlStringRGB(script.custom2)}";
                string col3 = $"#{ColorUtility.ToHtmlStringRGB(script.custom3)}";
                string col4 = $"#{ColorUtility.ToHtmlStringRGB(script.custom4)}";
                string col5 = $"#{ColorUtility.ToHtmlStringRGB(script.custom5)}";
                string[] customTheme = { col1, col2, col3, col4, col5 };

                newThemeSO.storedTheme = customTheme;
                EditorUtility.SetDirty(newThemeSO);

                zLogger.Log("<b>Theme Picker</b>", $"<b>Theme saved to</b> \"{path}\"", LogColor.Lime, false);
                GUIUtility.ExitGUI();
            }
            if (GUILayout.Button("Load theme"))
            {
                if (customThemeContainer.objectReferenceValue == null)
                {
                    zLogger.Log("Theme Picker", "Theme Container not found!", LogColor.Red, false);
                    GUIUtility.ExitGUI();
                    return;
                }

                script.themeContainer.storedTheme = script.customThemeContainer.storedTheme;
                EditorUtility.SetDirty(script.themeContainer);
                if (!script.isThemeCustom) script.isThemeCustom = true;
                zLogger.Log("Theme Picker", $"Custom theme loaded!", LogColor.LightBlue, true);

                customThemeContainer.objectReferenceValue = null;
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
            #endregion
        }
        EditorGUILayout.EndVertical();

        EditorGUILayout.Space(2);
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        InspectorUtils.SectionLabel(ThemeColor.Col4, "Preview");
        script.preview = GUILayout.Toggle(script.preview, " Show preview", foldoutStyle);
        if (script.preview)
        {
            InspectorUtils.TitleLabel(ThemeColor.Col1, "Preview", true);

            GUI.backgroundColor = InspectorUtils.RGBColor(ThemeColor.Col1) * 1.25f;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.backgroundColor = baseColor;
            InspectorUtils.SectionLabel(ThemeColor.Col1, "General");
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            ExampleField();
            ExampleField();
            ExampleField();
            ExampleField();
            ExampleField();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);
            GUI.backgroundColor = InspectorUtils.RGBColor(ThemeColor.Col2) * 1.25f;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.backgroundColor = baseColor;
            InspectorUtils.SectionLabel(ThemeColor.Col2, "Effects");
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            ExampleField();
            ExampleField();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);
            GUI.backgroundColor = InspectorUtils.RGBColor(ThemeColor.Col3) * 1.25f;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.backgroundColor = baseColor;
            InspectorUtils.SectionLabel(ThemeColor.Col3, "Audio");
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            ExampleField();
            ExampleField();
            ExampleField();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);
            GUI.backgroundColor = InspectorUtils.RGBColor(ThemeColor.Col4) * 1.25f;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.backgroundColor = baseColor;
            InspectorUtils.SectionLabel(ThemeColor.Col4, "Particles");
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            ExampleField();
            ExampleField();
            ExampleField();
            ExampleField();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);
            GUI.backgroundColor = InspectorUtils.RGBColor(ThemeColor.Col5) * 1.25f;
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            GUI.backgroundColor = baseColor;
            InspectorUtils.SectionLabel(ThemeColor.Col5, "Other");
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            ExampleField();
            ExampleField();
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space(2);
        }
        EditorGUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }

    #region Utility methods
    private void ColorDisplay(string[] theme)
    {
        GUIStyle richText = new GUIStyle(GUI.skin.label);
        richText.richText = true;
        richText.stretchWidth = true;
        richText.alignment = TextAnchor.UpperLeft;

        char a = '\u2588';
        string b = $"{a}{a}{a}{a}";
        string col1 = theme[0];
        string col2 = theme[1];
        string col3 = theme[2];
        string col4 = theme[3];
        string col5 = theme[4];

        EditorGUILayout.LabelField($"  <size=13><color={col1}>{b}</color><color={col2}>{b}</color><color={col3}>{b}</color><color={col4}>{b}</color><color={col5}>{b}</color></size>", richText);
    }

    private void ThemeButton(string[] theme, int themeNum)
    {
        GUILayoutOption layoutOption = GUILayout.Width(EditorGUIUtility.currentViewWidth / 2);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button($"Theme {themeNum}", layoutOption))
        {
            script.themeContainer.storedTheme = theme;
            EditorUtility.SetDirty(script.themeContainer);
            if (script.isThemeCustom) script.isThemeCustom = false;
            zLogger.Log("Theme Picker", $"Theme {themeNum} applied!", LogColor.Lime, false);
        }
        ColorDisplay(theme);
        EditorGUILayout.EndHorizontal();
    }

    private void ExampleField()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Example");
        EditorGUILayout.LabelField("", EditorStyles.textField, GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth / 2.5f));
        EditorGUILayout.EndHorizontal();
    }
    #endregion
}
#endif