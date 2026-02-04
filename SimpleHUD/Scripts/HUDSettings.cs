
using System;
using UdonSharp;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK3.Persistence;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.SimpleHUD
{
    [AttributeUsage(AttributeTargets.Method)]
    public class DescriptionAttribute : Attribute
    {
        public string Description;
        public DescriptionAttribute(string description) => Description = description;
    }

    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class HUDSettings : UdonSharpBehaviour
    {
        #region Variables
        [SerializeField] private HUDPositioner hudScript;
        [SerializeField] private Animator hudAnimator;
        [SerializeField] private Animator settingsAnimator;

        [Tooltip("The Center Fill Slider component on the Position X slider, used to update it's state.")]
        [SerializeField] private CenterFillSlider centerFillX;
        [SerializeField] private Slider posXSlider;
        [SerializeField] private Text posXText;
        [SerializeField, Range(0, 1)] private float posXDefault = 0.5f;

        [Tooltip("The Center Fill Slider component on the Position Y slider, used to update it's state.")]
        [SerializeField] private CenterFillSlider centerFillY;
        [SerializeField] private Slider posYSlider;
        [SerializeField] private Text posYText;
        [SerializeField, Range(0, 1)] private float posYDefault = 0.5f;

        [SerializeField] private Slider scaleSlider;
        [SerializeField] private Text scaleText;
        [SerializeField, Range(0, 1)] private float scaleDefault = 0.25f;

        [SerializeField] private Slider distanceSlider;
        [SerializeField] private Text distanceText;
        [SerializeField, Range(0, 1)] private float distanceDefault = 0.25f;

        [SerializeField] private Slider smoothingSlider;
        [SerializeField] private Text smoothingText;
        [SerializeField, Range(0, 1)] private float smoothingDefaultVR = 0.2f;
        [SerializeField, Range(0, 1)] private float smoothingDefaultDesktop;

        [SerializeField] private Slider fovXSlider;
        [SerializeField] private Text fovXText;
        [SerializeField, Range(0, 1)] private float fovXDefault = 0.5f;

        [SerializeField] private Slider fovYSlider;
        [SerializeField] private Text fovYText;
        [SerializeField, Range(0, 1)] private float fovYDefault = 0.5f;

        /*
        [SerializeField] private Color togglesOnColor = Color.white;
        [SerializeField] private Color togglesOffColor = Color.gray;
        public Color OnColor => togglesOnColor;
        public Color OffColor => togglesOffColor; */

        [Tooltip("The amount of decimal places to display for the value of sliders.")]
        [SerializeField, Range(1, 4)] private int textDecimalPlaces = 2;
        [Tooltip("Whether or not to use Player Data to save the settings.")]
        [SerializeField] private bool usePersistence = true;

        private const string ToggleKey = "zSkull162_simpleHUD_isOn";
        private const string PosXKey = "zSkull162_simpleHUD_posX";
        private const string PosYKey = "zSkull162_simpleHUD_posY";
        private const string ScaleKey = "zSkull162_simpleHUD_scale";
        private const string DistKey = "zSkull162_simpleHUD_distance";
        private const string SmoothKey = "zSkull162_simpleHUD_smoothing";
        private const string FovXKey = "zSkull162_simpleHUD_fovX";
        private const string FovYKey = "zSkull162_simpleHUD_fovY";
        private bool isInVR;
        #endregion

        #region Editor/Automation
        [HideInInspector] public bool matchSliders;
        [HideInInspector] public bool slidersVisible;
        [HideInInspector] public bool helpVisible;
#if UNITY_EDITOR
        // During playmode, set the default values to the slider values, to make it easier to customize
        private void Update() {
            if (!matchSliders) return;
            if (posXSlider != null) posXDefault = posXSlider.value / posXSlider.maxValue;
            if (posYSlider != null) posYDefault = posYSlider.value / posYSlider.maxValue;
            if (scaleSlider != null) scaleDefault = scaleSlider.value / scaleSlider.maxValue;
            if (distanceSlider != null) distanceDefault = distanceSlider.value / distanceSlider.maxValue;
            if (smoothingSlider != null) smoothingDefaultVR = smoothingSlider.value / smoothingSlider.maxValue;
            if (fovXSlider != null) fovXDefault = fovXSlider.value / fovXSlider.maxValue;
            if (fovYSlider != null) fovYDefault = fovYSlider.value / fovYSlider.maxValue;
        }
#if !COMPILER_UDONSHARP
        private void OnValidate() {
            if (!Application.isPlaying) matchSliders = false;
            UpdateSliders();

            if (hudScript != null && hudAnimator == null) {
                Animator hudAnim = hudScript.GetComponentInChildren<Animator>(true);
                if (hudAnim != null) hudAnimator = hudAnim;
            }
            else {
                HUDPositioner script = hudAnimator.transform.parent.GetComponent<HUDPositioner>();
                if (script != null) hudScript = script;
            }

            if (settingsAnimator == null) {
                Animator settingsAnim = this.GetComponent<Animator>();
                if (settingsAnim != null) settingsAnimator = settingsAnim;
            }

            if (centerFillX == null && posXSlider != null) {
                CenterFillSlider centerX = posXSlider.GetComponent<CenterFillSlider>();
                if (centerX != null) centerFillX = centerX;
            }

            if (centerFillY == null && posYSlider != null) {
                CenterFillSlider centerY = posYSlider.GetComponent<CenterFillSlider>();
                if (centerY != null) centerFillY = centerY;
            }
        }

        // _HiddenMethod means it will not show up in the Public Methods list in the inspector
        [Description("_HiddenMethod")]
        public void FindHUDScript() {
            HUDPositioner[] positioners = FindObjectsByType<HUDPositioner>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            if (positioners.Length > 0) hudScript = positioners[0];
        }

        [Description("_HiddenMethod")]
        public void UpdateSliders() {
            if (posXSlider != null) {
                posXSlider.value = posXDefault;
                if (centerFillX != null) centerFillX.OnValueChanged();
                if (PrefabUtility.IsPartOfAnyPrefab(posXSlider)) PrefabUtility.RecordPrefabInstancePropertyModifications(posXSlider);
            }

            if (posYSlider != null) {
                posYSlider.value = posYDefault;
                if (centerFillY != null) centerFillY.OnValueChanged();
                if (PrefabUtility.IsPartOfAnyPrefab(posYSlider)) PrefabUtility.RecordPrefabInstancePropertyModifications(posYSlider);
            }

            if (scaleSlider != null) {
                scaleSlider.value = scaleDefault;
                if (PrefabUtility.IsPartOfAnyPrefab(scaleSlider)) PrefabUtility.RecordPrefabInstancePropertyModifications(scaleSlider);
            }

            if (distanceSlider != null) {
                distanceSlider.value = distanceDefault;
                if (PrefabUtility.IsPartOfAnyPrefab(distanceSlider)) PrefabUtility.RecordPrefabInstancePropertyModifications(distanceSlider);
            }

            if (smoothingSlider != null) {
                smoothingSlider.value = smoothingDefaultVR;
                if (PrefabUtility.IsPartOfAnyPrefab(smoothingSlider)) PrefabUtility.RecordPrefabInstancePropertyModifications(smoothingSlider);
            }

            if (fovXSlider != null) {
                fovXSlider.value = fovXDefault;
                if (PrefabUtility.IsPartOfAnyPrefab(fovXSlider)) PrefabUtility.RecordPrefabInstancePropertyModifications(fovXSlider);
            }

            if (fovYSlider != null) {
                fovYSlider.value = fovYDefault;
                if (PrefabUtility.IsPartOfAnyPrefab(fovYSlider)) PrefabUtility.RecordPrefabInstancePropertyModifications(fovYSlider);
            }
        }
#endif
#endif
#endregion

        private void Start() {
            isInVR = Networking.LocalPlayer.IsUserInVR();
        }

        public override void OnPlayerRestored(VRCPlayerApi player) {
            if (!player.isLocal) return;
            if (!usePersistence) {
                Initalize();
                return;
            }
            if (!isInVR) distanceSlider.interactable = false;

            LoadValue(posXSlider, "posX", PosXKey, posXDefault, posXText);
            LoadValue(posYSlider, "posY", PosYKey, posYDefault, posYText);
            if (centerFillX != null) centerFillX.OnValueChanged();
            if (centerFillY != null) centerFillY.OnValueChanged();

            LoadValue(scaleSlider, "scale", ScaleKey, scaleDefault, scaleText);
            LoadValue(fovXSlider, "fovX", FovXKey, fovXDefault, fovXText);
            LoadValue(fovYSlider, "fovY", FovYKey, fovYDefault, fovYText);

            if (PlayerData.TryGetFloat(Networking.LocalPlayer, SmoothKey, out float savedSmoothing)) {
                smoothingSlider.SetValueWithoutNotify(savedSmoothing * smoothingSlider.maxValue);
                if (hudScript != null) hudScript.MotionSmoothing = savedSmoothing;
                SetText(smoothingText, savedSmoothing);
            }
            else ResetSmoothing();

            float defaultValue = 0f;
            if (isInVR) defaultValue = distanceDefault;
            LoadValue(distanceSlider, "distance", DistKey, defaultValue, distanceText);

            if (PlayerData.TryGetBool(Networking.LocalPlayer, ToggleKey, out bool savedState)) {
                settingsAnimator.SetBool("isOn", savedState);
            }
            else settingsAnimator.SetBool("isOn", hudAnimator.gameObject.activeSelf);
        }

        private void Initalize() {
            ResetPosX();
            ResetPosY();
            ResetScale();
            ResetDistance();
            ResetSmoothing();
            ResetFovX();
            ResetFovY();
        }

        private void UpdateAnimator() {
            UpdatePosX();
            UpdatePosY();
            UpdateScale();
            UpdateDistance();
            UpdateFovX();
            UpdateFovY();
        }

        [Description("Toggles the active state of the HUD.")]
        public void ToggleHUD() {
            if (hudAnimator == null) {
                zLogger.LogError(name, "[ToggleHUD] HUD Object is null");
                return;
            }
            if (settingsAnimator == null) {
                zLogger.LogError(name, "[SetValue] HUD Animator is null.");
                return;
            }
            bool state = hudAnimator.gameObject.activeSelf;

            hudAnimator.gameObject.SetActive(!state);
            settingsAnimator.SetBool("isOn", !state);
            if (usePersistence) PlayerData.SetBool(ToggleKey, !state);

            UpdateAnimator();
        }

        #region Update methods
        [Description("Sets the HUD animator's posX parameter to the value of the Pos X Slider.")]
        public void UpdatePosX() {
            if (posXSlider == null) return;
            float value = posXSlider.value / posXSlider.maxValue;

            SetValue("posX", PosXKey, value, posXText);
        }
        [Description("Sets the HUD animator's posY parameter to the value of the Pos Y Slider.")]
        public void UpdatePosY() {
            if (posYSlider == null) return;
            float value = posYSlider.value / posYSlider.maxValue;

            SetValue("posY", PosYKey, value, posYText);
        }
        [Description("Sets the HUD animator's scale parameter to the value of the Scale Slider.")]
        public void UpdateScale() {
            if (scaleSlider == null) return;
            float value = scaleSlider.value / scaleSlider.maxValue;

            SetValue("scale", ScaleKey, value, scaleText);
        }
        [Description("Sets the HUD animator's distance parameter to the value of the Distance Slider. Only works in VR.")]
        public void UpdateDistance() {
            if (distanceSlider == null) return;
            float value = 0f; // Only modify the distance if the player is in VR, because distance does not visually change anything in desktop mode
            if (isInVR) value = distanceSlider.value / distanceSlider.maxValue;

            SetValue("distance", DistKey, value, distanceText);
        }
        [Description("Sets the HUD Script's motion smoothing value to the value of the Smoothing Slider.")]
        public void UpdateSmoothing() {
            if (smoothingSlider == null || hudScript == null) return;
            float value = smoothingSlider.value / smoothingSlider.maxValue;

            hudScript.MotionSmoothing = value;
            if (usePersistence) PlayerData.SetFloat(SmoothKey, value);
            SetText(smoothingText, value);
        }
        [Description("Sets the HUD animator's fovX parameter to the value of the Fov X Slider.")]
        public void UpdateFovX() {
            if (fovXSlider == null) return;
            float value = fovXSlider.value / fovXSlider.maxValue;

            SetValue("fovX", FovXKey, value, fovXText);
        }
        [Description("Sets the HUD animator's fovY parameter to the value of the Fov Y Slider.")]
        public void UpdateFovY() {
            if (fovYSlider == null) return;
            float value = fovYSlider.value / fovYSlider.maxValue;

            SetValue("fovY", FovYKey, value, fovYText);
        }
        #endregion

        #region Reset methods
        [Description("Resets the HUD animator's posX parameter to the Pos X Default.")]
        public void ResetPosX() {
            ResetValue(posXSlider, "posX", PosXKey, posXDefault, posXText);
            if (centerFillX != null) centerFillX.OnValueChanged();
        }
        [Description("Resets the HUD animator's posY parameter to the Pos Y Default.")]
        public void ResetPosY() {
            ResetValue(posYSlider, "posY", PosYKey, posYDefault, posYText);
            if (centerFillY != null) centerFillY.OnValueChanged();
        }
        [Description("Resets the HUD animator's scale parameter to the Scale Default.")]
        public void ResetScale() => ResetValue(scaleSlider, "scale", ScaleKey, scaleDefault, scaleText);
        [Description("Resets the HUD animator's fovX parameter to the Fov X Default.")]
        public void ResetFovX() => ResetValue(fovXSlider, "fovX", FovXKey, fovXDefault, fovXText);
        [Description("Resets the HUD animator's fovY parameter to the Fov Y Default.")]
        public void ResetFovY() => ResetValue(fovYSlider, "fovY", FovYKey, fovYDefault, fovYText);

        [Description("Resets the HUD animator's distance parameter to the Distance Default.")]
        public void ResetDistance() {
            float defaultValue = 0f;
            if (isInVR) defaultValue = distanceDefault;

            ResetValue(distanceSlider, "distance", DistKey, defaultValue, distanceText);
        }
        [Description("Resets the HUD Script's motion smoothing value to the Smoothing Default Desktop or VR, depending on the player's platform.")]
        public void ResetSmoothing() {
            if (smoothingSlider == null || hudScript == null) return;
            float defaultValue = smoothingDefaultDesktop;
            if (isInVR) defaultValue = smoothingDefaultVR;

            smoothingSlider.SetValueWithoutNotify(defaultValue * smoothingSlider.maxValue);
            hudScript.MotionSmoothing = defaultValue;
            if (usePersistence) PlayerData.SetFloat(SmoothKey, defaultValue);
            SetText(smoothingText, defaultValue);
        }
        #endregion

        #region Helper methods
        private void LoadValue(Slider slider, string param, string key, float defaultValue, Text text) {
            if (hudAnimator == null) {
                zLogger.LogError(name, "[LoadValue] HUD Animator is null.");
                return;
            }
            if (slider == null) {
                zLogger.LogError(name, "[LoadValue] Slider was null.");
                return;
            }
            if (text == null) {
                zLogger.LogError(name, "[LoadValue] Text was null.");
                return;
            }

            if (PlayerData.TryGetFloat(Networking.LocalPlayer, key, out float savedValue)) {
                slider.SetValueWithoutNotify(savedValue * slider.maxValue);
                hudAnimator.SetFloat(param, savedValue);
                SetText(text, savedValue);
            }
            else ResetValue(slider, param, key, defaultValue, text);
        }

        private void SetValue(string param, string key, float value, Text text) {
            if (hudAnimator == null) {
                zLogger.LogError(name, "[SetValue] HUD Animator is null.");
                return;
            }
            hudAnimator.SetFloat(param, value);
            if (usePersistence) PlayerData.SetFloat(key, value);
            SetText(text, value);
        }

        private void ResetValue(Slider slider, string param, string key, float value, Text text) {
            if (hudAnimator == null) {
                zLogger.LogError(name, "[ResetValue] HUD Animator is null.");
                return;
            }
            if (slider == null) {
                zLogger.LogError(name, "[ResetValue] Slider was null.");
                return;
            }
            slider.SetValueWithoutNotify(value * slider.maxValue);
            hudAnimator.SetFloat(param, value);
            if (usePersistence) PlayerData.SetFloat(key, value);
            SetText(text, value);
        }

        private void SetText(Text text, float value) {
            if (text == null) {
                zLogger.LogError(name, "[SetText] Text was null.");
                return;
            }
            text.text = value.ToString($"F{textDecimalPlaces}");
        }
        #endregion
    }
}