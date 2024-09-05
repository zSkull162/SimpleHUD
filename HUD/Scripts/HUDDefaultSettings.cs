
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class HUDDefaultSettings : UdonSharpBehaviour
{
    [SerializeField] private Animator hudAnimator;
    [SerializeField] private HUDPositioner hudObject;
    [SerializeField, Range(0, 1)] private float defaultPosX;
    [SerializeField, Range(0, 1)] private float defaultPosY;
    [SerializeField, Range(0, 1)] private float defaultScale;
    [SerializeField, Range(0, 1)] private float defaultDistance;
    [Tooltip("The higher the number, the quicker the movement.\nIf set to 20, motion smoothing will be disabled.")]
    [SerializeField, Range(0, 20)] private float defaultSmoothing = 18f;

    void Start()
    {
        // Reset default smoothing if it's below zero (negative)
        if (defaultSmoothing < 0) defaultSmoothing = 0;

        // Set the HUD's settings to the valeus
        hudAnimator.SetFloat("posX", defaultPosX);
        hudAnimator.SetFloat("posY", defaultPosY);
        hudAnimator.SetFloat("scale", defaultScale);
        hudAnimator.SetFloat("distance", defaultDistance);
        hudObject.MotionSmoothing = defaultSmoothing;
    }
}
