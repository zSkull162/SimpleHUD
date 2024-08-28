
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

        if (Networking.LocalPlayer.IsUserInVR())
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
        }
    }
}
