
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

    void Start() => ApplySettings();

    private void ApplySettings()
    {
        // Smoothing should never be below zero, so make sure it isn't
        if (defaultSmoothing < 0) defaultSmoothing = 0;

        hudAnimator.SetFloat("posX", defaultPosX);
        hudAnimator.SetFloat("posY", defaultPosY);
        hudAnimator.SetFloat("scale", defaultScale);
        hudAnimator.SetFloat("distance", defaultDistance);
        hudObject.MotionSmoothing = defaultSmoothing;
        zLogger.Log(name, $"Default settings applied! posX = {hudAnimator.GetFloat("posX")} | posY = {hudAnimator.GetFloat("posY")} | scale = {hudAnimator.GetFloat("scale")} | distance = {hudAnimator.GetFloat("distance")}", LogColor.Lime, false);
    }
}