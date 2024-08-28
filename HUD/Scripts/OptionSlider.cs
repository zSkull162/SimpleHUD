
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

public enum HUDOption
{
    SetXPos,
    SetYPos,
    SetDistance,
    SetScale,
    SetSmooth
}

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class OptionSlider : UdonSharpBehaviour
{
    [SerializeField] private HUDSettings hudSettings;
    [SerializeField] private HUDOption eventType;
    private UnityEngine.UI.Slider slider;

    private void Start()
    {
        slider = this.GetComponent<UnityEngine.UI.Slider>();
        OnValueChanged();
    }

    public void OnValueChanged()
    {
        if (eventType == HUDOption.SetXPos)
        {
            hudSettings.SetPosX(slider.value);
        }
        else if (eventType == HUDOption.SetYPos)
        {
            hudSettings.SetPosY(slider.value);
        }
        else if (eventType == HUDOption.SetDistance)
        {
            hudSettings.SetDistance(slider.value);
        }
        else if (eventType == HUDOption.SetSmooth)
        {
            hudSettings.SetSmoothing(Mathf.Abs(slider.value));
        }
        else
        {
            hudSettings.SetScale(slider.value);
        }
    }
}
