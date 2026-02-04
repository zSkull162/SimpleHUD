#if UNITY_EDITOR && !COMPILER_UDONSHARP
using System.Linq;
using UnityEditor;
using UnityEngine;

public enum ThemeColor
{
    Col1,
    Col2,
    Col3,
    Col4,
    Col5
}

public static class InspectorUtils
{
    // public static string[] colorList = { "teal", "#38c194", "#31bdb6", "#2c99bc", "#3775bf" };    (old code from before the theme picker)
    private static ThemeScriptableObject themeContainer;

    public static ThemeScriptableObject GetThemeSO()
    {
        // Thanks to LiveDimension for providing this Scriptable Object loading code,
        // and for some help getting it all to work correctly.

        if (themeContainer == null) {
            var guid = AssetDatabase.FindAssets($"ThemeContainer t:{nameof(ThemeScriptableObject)}").First();
            ThemeScriptableObject themeSO = AssetDatabase.LoadAssetAtPath<ThemeScriptableObject>(AssetDatabase.GUIDToAssetPath(guid));

            themeContainer = themeSO;
            return themeSO;
        }
        else return themeContainer;
    }

    /// <summary>
    /// Gets a color from the current theme.
    /// </summary>
    /// <param name="color"></param>
    /// <returns>A Hexidecimal color string from the inputted ThemeColor</returns>
    public static string Color(ThemeColor color)
    {
        if (themeContainer == null) themeContainer = GetThemeSO();
        string[] colorList = themeContainer.storedTheme;
        string target = "";

        if (colorList == null || colorList.Length == 0) colorList = new string[] { "teal", "#38c194", "#31bdb6", "#2c99bc", "#3775bf" };

        switch (color)
        {
            case ThemeColor.Col1:
                target = colorList[0];
                break;
            case ThemeColor.Col2:
                target = colorList[1];
                break;
            case ThemeColor.Col3:
                target = colorList[2];
                break;
            case ThemeColor.Col4:
                target = colorList[3];
                break;
            case ThemeColor.Col5:
                target = colorList[4];
                break;
        }

        if (target == "") {
            Debug.Log($"<color=red>Target was empty.</color>");
            return null;
        }
        return target;
    }

    /// <summary>
    /// Sets GUI.backgroundColor to be the theme color of choice.
    /// </summary>
    /// <param name="color"></param>
    /// <returns>The theme color as RGB</returns>
    public static Color SetColor(ThemeColor color) {
        Color newCol = RGBColor(color) * 1.25f;
        GUI.backgroundColor = newCol;
        return newCol;
    }

    /// <summary>
    /// Sets GUI.backgroundColor to be the color of choice.
    /// </summary>
    /// <param name="color"></param>
    public static void SetColor(Color color) {
        GUI.backgroundColor = color;
    }

    /// <summary>
    /// Sets GUI.backgroundColor to be the default color, white.
    /// Shorthand for writing GUI.backgroundColor = Color.white
    /// </summary>
    /// <param name="color"></param>
    public static void SetColor() {
        GUI.backgroundColor = UnityEngine.Color.white;
    }

    /// <summary>
    /// Gets a color from the current theme as RGB values.
    /// </summary>
    /// <param name="color"></param>
    /// <returns>a UnityEngine.Color from the inputted ThemeColor</returns>
    public static Color RGBColor(ThemeColor color)
    {
        if (ColorUtility.TryParseHtmlString(Color(color), out Color target)) return target;
        else {
            Debug.Log($"<color=red>Parse failed. Returning <b>white</b></color>");
            return UnityEngine.Color.white;
        }
    }

    /// <summary>
    /// Gets a random color from the current theme
    /// </summary>
    /// <returns>A ThemeColor</returns>
    public static ThemeColor RandomColor() {
        if (themeContainer == null) themeContainer = GetThemeSO();
        string[] colorList = themeContainer.storedTheme;
        ThemeColor target = ThemeColor.Col1;

        if (colorList == null || colorList.Length == 0) colorList = new string[] { "teal", "#38c194", "#31bdb6", "#2c99bc", "#3775bf" };

        int rng = Random.Range(0, 4);
        switch (rng)
        {  
            case 0:
                target = ThemeColor.Col1;
                break;
            case 1:
                target = ThemeColor.Col2;
                break;
            case 2:
                target = ThemeColor.Col3;
                break;
            case 3:
                target = ThemeColor.Col4;
                break;
            case 4:
                target = ThemeColor.Col5;
                break;
        }
        return target;
    }

    /// <summary>
    /// Gets a random color from the current theme
    /// </summary>
    /// <returns>A UnityEngine.Color</returns>
    public static Color RandomRGBColor() {
        if (themeContainer == null) themeContainer = GetThemeSO();
        string[] colorList = themeContainer.storedTheme;
        string target = "";

        if (colorList == null || colorList.Length == 0) colorList = new string[] { "teal", "#38c194", "#31bdb6", "#2c99bc", "#3775bf" };

        int rng = Random.Range(0, 4);
        switch (rng)
        {  
            case 0:
                target = colorList[0];
                break;
            case 1:
                target = colorList[1];
                break;
            case 2:
                target = colorList[2];
                break;
            case 3:
                target = colorList[3];
                break;
            case 4:
                target = colorList[4];
                break;
        }
        if (ColorUtility.TryParseHtmlString(target, out Color result)) return result;
        else {
            Debug.Log($"<color=red>Parse failed. Returning <b>white</b></color>");
            return UnityEngine.Color.white;
        }
    }

    /// <summary>
    /// Creates a centered, bold, colored LabelField. Usually used to display the script name.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="title"></param>
    /// <param name="useBorder"></param>
    public static void TitleLabel(ThemeColor color, string title, bool useBorder)
    {
        GUIStyle richTextCentered = new GUIStyle(GUI.skin.label);
        richTextCentered.richText = true;
        richTextCentered.alignment = TextAnchor.UpperCenter;

        if (useBorder) {
            EditorGUILayout.LabelField($"<size=14><b><color={Color(color)}>----------------- {title} -----------------</color></b></size>", richTextCentered);
            EditorGUILayout.Space(1);
        }
        else {
            EditorGUILayout.LabelField($"<size=14><b><color={Color(color)}>{title}</color></b></size>", richTextCentered);
            EditorGUILayout.Space(1);
        }
    }

    /// <summary>
    /// Creates a bold, colored LabelField anchored to the left.
    /// </summary>
    /// <param name="color"></param>
    /// <param name="title"></param>
    public static void SectionLabel(ThemeColor color, string title)
    {
        GUIStyle richText = new GUIStyle(GUI.skin.label);
        richText.richText = true;
        EditorGUILayout.LabelField($"<size=13><b><color={Color(color)}>{title}</color></b></size>", richText);
    }

    /// <summary>
    /// A custom method for drawing arrays in the inspector.
    /// </summary>
    /// <param name="array"></param>
    public static void ArrayField(SerializedProperty array)
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        EditorGUILayout.BeginHorizontal();

        int elementIndex = 0;
        if (array.arraySize > 0) elementIndex = array.arraySize - 1;

        if (GUILayout.Button("+", EditorStyles.miniButtonLeft, GUILayout.Width(30))) array.InsertArrayElementAtIndex(elementIndex);

        EditorGUI.BeginDisabledGroup(array.arraySize < 1);
        if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(30))) array.DeleteArrayElementAtIndex(elementIndex);
        EditorGUI.EndDisabledGroup();

        GUILayout.Label(array.displayName);

        GUILayout.FlexibleSpace();
        array.arraySize = EditorGUILayout.IntField(array.arraySize, GUILayout.Width(48));
        EditorGUILayout.EndHorizontal();

        if (array.arraySize > 0) {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            for (int i = 0; i < array.arraySize; ++i)
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.PropertyField(array.GetArrayElementAtIndex(i), GUILayout.Height(EditorGUIUtility.singleLineHeight));
                if (GUILayout.Button("x", EditorStyles.miniButton, GUILayout.Width(24))) array.DeleteArrayElementAtIndex(i);

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// A custom method for drawing arrays in the inspector, with a foldout for toggling visibility.
    /// </summary>
    /// <param name="array"></param>
    /// <param name="visibility"></param>
    public static bool ArrayField(SerializedProperty array, bool visibility)
    {
        GUIStyle foldout = new GUIStyle(EditorStyles.foldout);
        foldout.fontStyle = FontStyle.Bold;
       
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        if (visibility)
        {
            visibility = GUILayout.Toggle(visibility, array.displayName, foldout, GUILayout.Height(EditorGUIUtility.singleLineHeight + 2));
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            
            EditorGUILayout.BeginHorizontal();
            int elementIndex = 0;
            if (array.arraySize > 0) elementIndex = array.arraySize - 1;

            if (GUILayout.Button("+", EditorStyles.miniButtonLeft, GUILayout.Width(30))) array.InsertArrayElementAtIndex(elementIndex);

            EditorGUI.BeginDisabledGroup(array.arraySize < 1);
            if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(30))) array.DeleteArrayElementAtIndex(elementIndex);
            EditorGUI.EndDisabledGroup();

            GUILayout.FlexibleSpace();
            array.arraySize = EditorGUILayout.IntField(array.arraySize, GUILayout.Width(48));
            EditorGUILayout.EndHorizontal();

            if (array.arraySize > 0) {
                GUILayout.Space(3);
                for (int i = 0; i < array.arraySize; ++i)
                {
                    EditorGUILayout.BeginHorizontal();

                    EditorGUILayout.PropertyField(array.GetArrayElementAtIndex(i), GUILayout.Height(EditorGUIUtility.singleLineHeight));
                    if (GUILayout.Button("x", EditorStyles.miniButton, GUILayout.Width(24))) array.DeleteArrayElementAtIndex(i);

                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }
        else {
            EditorGUILayout.BeginHorizontal();
            foldout.fontStyle = FontStyle.Normal;
            visibility = GUILayout.Toggle(visibility, array.displayName, foldout, GUILayout.Height(EditorGUIUtility.singleLineHeight + 2));

            GUILayout.FlexibleSpace();
            array.arraySize = EditorGUILayout.IntField(array.arraySize, GUILayout.Width(48), GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        return visibility;
    }

    /// <summary>
    /// A custom method for drawing two synced arrays in the inspector, with a foldout for toggling visibility.
    /// Array 1 will be on the left side of the elements, and Array 2 will be on the right. Array Name is the name of the foldout that appears in the inspector.
    /// Value Name is the text that appears beside the field of the first array.
    /// </summary>
    /// <param name="array1"></param>
    /// <param name="array2"></param>
    /// <param name="arrayName"></param>
    /// <param name="valueName"></param>
    /// <param name="visibility"></param>
    public static bool ArrayField(SerializedProperty array1, SerializedProperty array2, string arrayName, string valueName, bool visibility) {
        GUIStyle foldout = new GUIStyle(EditorStyles.foldout);
        foldout.fontStyle = FontStyle.Bold;

        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        if (visibility) {
            visibility = GUILayout.Toggle(visibility, arrayName, foldout, GUILayout.Height(EditorGUIUtility.singleLineHeight + 2));
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            EditorGUILayout.BeginHorizontal();
            int count = Mathf.Min(array1.arraySize, array2.arraySize);

            if (GUILayout.Button("+", EditorStyles.miniButtonLeft, GUILayout.Width(30))) {
                array1.InsertArrayElementAtIndex(count);
                array2.InsertArrayElementAtIndex(count);
            }

            int last = count - 1;
            EditorGUI.BeginDisabledGroup(last < 0);
            if (GUILayout.Button("-", EditorStyles.miniButtonRight, GUILayout.Width(30))) {
                array1.DeleteArrayElementAtIndex(last);
                array2.DeleteArrayElementAtIndex(last);
            }
            EditorGUI.EndDisabledGroup();

            GUILayout.FlexibleSpace();
            int newSize = EditorGUILayout.IntField(array1.arraySize, GUILayout.Width(48));
            if (newSize != array1.arraySize) {
                array1.arraySize = newSize;
                array2.arraySize = newSize;
            }
            EditorGUILayout.EndHorizontal();

            if (count > 0) {
                GUILayout.Space(3);
                for (int i = 0; i < count; ++i) {
                    EditorGUILayout.BeginHorizontal();

                    if (GUILayout.Button("x", EditorStyles.miniButton, GUILayout.Width(24))) {
                        array1.DeleteArrayElementAtIndex(i);
                        array2.DeleteArrayElementAtIndex(i);
                        break;
                    }

                    GUILayout.Space(8);
                    using (new EditorGUILayout.HorizontalScope()) {
                        GUIContent content1 = new GUIContent($"Element {i}:");
                        Vector2 labelSize1 = EditorStyles.label.CalcSize(content1);

                        EditorGUILayout.LabelField(content1, GUILayout.Width(labelSize1.x));
                        GUILayout.Space(48);
                        EditorGUILayout.PropertyField(array1.GetArrayElementAtIndex(i), GUIContent.none);
                    }
                    EditorGUILayout.Space();
                    using (new EditorGUILayout.HorizontalScope()) {
                        GUIContent content2 = new GUIContent(valueName);
                        Vector2 labelSize2 = EditorStyles.label.CalcSize(content2);

                        EditorGUILayout.LabelField(content2, GUILayout.Width(labelSize2.x));
                        GUILayout.Space(48);
                        EditorGUILayout.PropertyField(array2.GetArrayElementAtIndex(i), GUIContent.none);
                    }

                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }
        else {
            EditorGUILayout.BeginHorizontal();
            foldout.fontStyle = FontStyle.Normal;
            visibility = GUILayout.Toggle(visibility, arrayName, foldout, GUILayout.Height(EditorGUIUtility.singleLineHeight + 2));

            GUILayout.FlexibleSpace();
            int newSize = EditorGUILayout.IntField(array1.arraySize, GUILayout.Width(48));
            if (newSize != array1.arraySize) {
                array1.arraySize = newSize;
                array2.arraySize = newSize;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        return visibility;
    }

    /// <summary>
    /// A custom method for drawing arrays in the inspector.
    /// This version of the method is Read Only, therefore no changes can be made to the contents of the array.
    /// </summary>
    /// <param name="array"></param>
    /// <param name="visibility"></param>
    /// <returns></returns>
    public static void ArrayFieldReadonly(SerializedProperty array)
    {
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label(array.displayName);

        GUILayout.FlexibleSpace();
        EditorGUILayout.IntField(array.arraySize, GUILayout.Width(48));
        EditorGUILayout.EndHorizontal();

        if (array.arraySize > 0) {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUI.BeginDisabledGroup(true);
            for (int i = 0; i < array.arraySize; ++i)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PropertyField(array.GetArrayElementAtIndex(i), GUILayout.Height(EditorGUIUtility.singleLineHeight));
                EditorGUILayout.EndHorizontal();
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.EndVertical();
        }
        EditorGUILayout.EndVertical();
    }

    /// <summary>
    /// A custom method for drawing arrays in the inspector, with a foldout for toggling visibility.
    /// This version of the method is Read Only, therefore no changes can be made to the contents of the array.
    /// </summary>
    /// <param name="array"></param>
    /// <param name="visibility"></param>
    /// <returns></returns>
    public static bool ArrayFieldReadonly(SerializedProperty array, bool visibility)
    {
        GUIStyle foldout = new GUIStyle(EditorStyles.foldout);
        foldout.fontStyle = FontStyle.Bold;
       
        EditorGUILayout.BeginVertical(EditorStyles.helpBox);
        if (visibility)
        {
            EditorGUILayout.BeginHorizontal();
            visibility = GUILayout.Toggle(visibility, array.displayName, foldout, GUILayout.Height(EditorGUIUtility.singleLineHeight + 2));

            GUILayout.FlexibleSpace();
            EditorGUILayout.IntField(array.arraySize, GUILayout.Width(48));
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            if (array.arraySize > 0) {
                EditorGUI.BeginDisabledGroup(true);
                for (int i = 0; i < array.arraySize; ++i) {
                    EditorGUILayout.PropertyField(array.GetArrayElementAtIndex(i), GUILayout.Height(EditorGUIUtility.singleLineHeight));
                }
                EditorGUI.EndDisabledGroup();
            }
            EditorGUILayout.EndVertical();
        }
        else {
            EditorGUILayout.BeginHorizontal();
            foldout.fontStyle = FontStyle.Normal;
            visibility = GUILayout.Toggle(visibility, array.displayName, foldout, GUILayout.Height(EditorGUIUtility.singleLineHeight + 2));

            GUILayout.FlexibleSpace();
            EditorGUILayout.IntField(array.arraySize, GUILayout.Width(48), GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndVertical();

        return visibility;
    }
}
#endif