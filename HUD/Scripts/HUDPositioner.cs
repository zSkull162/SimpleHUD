
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class HUDPositioner : UdonSharpBehaviour
{
    [Tooltip("How smoothly the HUD will follow the player's view. Higher values are more rough, and lower values are more smooth.\n<b>Setting this to 20 will disable smoothing.</b>")]
    [SerializeField] private float motionSmoothing = 18f;
    private VRCPlayerApi localPlayer;

    private void Start()
    {
        if (Networking.LocalPlayer.IsValid()) {
            localPlayer = Networking.LocalPlayer;
        }
    }

    public override void PostLateUpdate()
    {
        var head = localPlayer.GetTrackingData(VRCPlayerApi.TrackingDataType.Head);

        this.transform.position = head.position;
        if (motionSmoothing < 20)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, head.rotation, 1.0f - Mathf.Exp(-motionSmoothing * Time.deltaTime));
            // this.transform.rotation = Quaternion.Lerp(this.transform.rotation, head.rotation, motionSmoothing);
        }
        else
        {
            this.transform.rotation = head.rotation;
        }
    }

    public float MotionSmoothing
    {
        set { motionSmoothing = value; }
        get { return motionSmoothing; }
    }
}
