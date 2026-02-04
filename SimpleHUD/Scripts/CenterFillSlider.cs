
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.SimpleHUD
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None), RequireComponent(typeof(Slider))]
    public class CenterFillSlider : UdonSharpBehaviour
    {
        [SerializeField] private Slider slider;
        [Tooltip("The slider value that will be the center to fill from.")]
        [SerializeField] private float center = 0.5f;

        #region Editor/Automation
    #if UNITY_EDITOR && !COMPILER_UDONSHARP
        private void OnValidate() {
            if (slider == null) {
                Slider s = this.GetComponent<Slider>();
                if (s != null && slider != s) slider = s;
            }
            if (slider == null) return;

            if (center < slider.minValue || center > slider.maxValue) {
                center = Mathf.Clamp(center, slider.minValue, slider.maxValue);
            }
        }
    #endif
        #endregion

        public void OnValueChanged() {
            slider.fillRect.anchorMin = new Vector2(Mathf.Clamp(slider.handleRect.anchorMin.x, 0, center), 0);
            slider.fillRect.anchorMax = new Vector2(Mathf.Clamp(slider.handleRect.anchorMin.x, center, 1), 1);
        }
    }
}