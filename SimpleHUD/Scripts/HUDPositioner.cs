
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace zSkull162.SimpleHUD
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class HUDPositioner : UdonSharpBehaviour
    {
        [Tooltip("How smoothly the HUD will follow the player's view. A value of zero will mean no smoothing at all.")]
        [SerializeField, Range(0, 1)] private float motionSmoothing = 0.2f;

        readonly float maxSmoothing = 20f;
        private float smoothingInternal;
        private VRCPlayerApi localPlayer;

        public float MotionSmoothing {
            set {
                value = Mathf.Clamp01(value);
                // Map the user-facing smoothing value to the internal smoothing value, so that it's a nice 0-1 range for users and a 0-20 range for the math function
                smoothingInternal = Mathf.Lerp(maxSmoothing, 0f, value);
                motionSmoothing = value;
            }
            get => motionSmoothing;
        }

        private void Start() {
            localPlayer = Networking.LocalPlayer;
        }

        public override void PostLateUpdate() {
            var head = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);

            this.transform.position = head.position;

            if (smoothingInternal >= maxSmoothing) { // If the smoothing value is at it's max value, ignore the math and directly set the rotation
                this.transform.rotation = head.rotation;
                return;
            }
            // Thanks to Vavassor for sharing this exponential smoothing code
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, head.rotation, 1.0f - Mathf.Exp(-smoothingInternal * Time.deltaTime));
        }
    }
}