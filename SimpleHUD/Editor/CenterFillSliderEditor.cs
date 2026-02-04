
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using zSkull162.SimpleHUD;

[CustomEditor(typeof(CenterFillSlider)), CanEditMultipleObjects]
public class CenterFillSliderEditor : Editor
{
    SerializedProperty slider;
    SerializedProperty center;
    Slider sliderComp;

    private void OnEnable() {
        slider = serializedObject.FindProperty("slider");
        center = serializedObject.FindProperty("center");

        Slider comp = (Slider)slider.objectReferenceValue;
        if (comp != null) sliderComp = comp;
    }

    public override void OnInspectorGUI() {
        bool multiEditing = serializedObject.isEditingMultipleObjects;

        UdonSharpEditor.UdonSharpGUI.DrawProgramSource(target);
        EditorGUILayout.Space();

        InspectorUtils.SetColor(ThemeColor.Col2);
        using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox)) {
            InspectorUtils.SetColor();

            serializedObject.Update();
            GUILayout.Space(2);
            EditorGUILayout.PropertyField(slider);
            GUILayout.Space(2);

            if (sliderComp == null || multiEditing) EditorGUILayout.PropertyField(center);
            else {
                center.floatValue = EditorGUILayout.Slider("Center", center.floatValue, sliderComp.minValue, sliderComp.maxValue);
                float midPoint = (sliderComp.maxValue + sliderComp.minValue) / 2f;

                if (center.floatValue != midPoint) {
                    using (new EditorGUILayout.HorizontalScope()) {
                        GUILayout.Space(14);
                        if (GUILayout.Button(" Set to Middle ", GUILayout.ExpandWidth(false))) center.floatValue = midPoint;
                    }
                }
            }

            GUILayout.Space(2);
            serializedObject.ApplyModifiedProperties();
        }
        EditorGUILayout.Space();

        InspectorUtils.SetColor(ThemeColor.Col3);
        EditorGUILayout.HelpBox("This script will make the slider fill from the center, rather than exclusively left to right.", MessageType.Info);
        InspectorUtils.SetColor();
    }
}