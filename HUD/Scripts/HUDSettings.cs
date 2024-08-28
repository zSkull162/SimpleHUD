
using Newtonsoft.Json.Linq;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None), DefaultExecutionOrder(-1)]
public class HUDSettings : UdonSharpBehaviour
{
    [SerializeField] private GameObject hudObject;
    [SerializeField] private Animator hudAnimator;
    [Tooltip("These have to be ordered exactly in the order of Horizontal slider, Vertical slider, Distance slider, Scale slider, and Smoothing slider.")]
    [SerializeField] private UnityEngine.UI.Slider[] sliders;
    private HUDPositioner hudPositioner;
    private Animator settingsAnimator;

    private void Start()
    {
        hudPositioner = hudObject.GetComponent<HUDPositioner>();
        settingsAnimator = this.GetComponent<Animator>();
    }

    public void UpdateValues()
    {
        hudAnimator.SetFloat("posX", sliders[0].value);
        hudAnimator.SetFloat("posY", sliders[1].value);
        hudAnimator.SetFloat("distance", sliders[2].value);
        hudAnimator.SetFloat("scale", sliders[3].value);
        hudPositioner.MotionSmoothing = Mathf.Abs(sliders[4].value);
    }

    public void SetPosX(float value)
    {
        hudAnimator.SetFloat("posX", value);
    }
    public void SetPosY(float value)
    {
        hudAnimator.SetFloat("posY", value);
    }
    public void SetDistance(float value)
    {
        hudAnimator.SetFloat("distance", value);
    }
    public void SetScale(float value)
    {
        hudAnimator.SetFloat("scale", value);
    }
    public void SetSmoothing(float value)
    {
        hudPositioner.MotionSmoothing = value;
    }
    public void ToggleHUD()
    {
        hudObject.SetActive(!hudObject.activeSelf);

        if (hudObject.activeSelf) settingsAnimator.SetBool("isOn", true);
        else settingsAnimator.SetBool("isOn", false);
    }
}
