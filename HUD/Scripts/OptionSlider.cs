
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

    // Bool for the inspector
    [HideInInspector] public bool infoGroup;

    private void Start()
    {
        slider = this.GetComponent<UnityEngine.UI.Slider>();
        OnValueChanged();
    }

    public void OnValueChanged()
    {
        switch (eventType)
        {
            case HUDOption.SetXPos:
                hudSettings.SetPosX(slider.value);
                break;
            case HUDOption.SetYPos:
                hudSettings.SetPosY(slider.value);
                break;
            case HUDOption.SetDistance:
                hudSettings.SetDistance(slider.value);
                break;
            case HUDOption.SetScale:
                hudSettings.SetScale(slider.value);
                break;
            case HUDOption.SetSmooth:
                hudSettings.SetSmoothing(Mathf.Abs(slider.value));
                break;
        }
    }
}
