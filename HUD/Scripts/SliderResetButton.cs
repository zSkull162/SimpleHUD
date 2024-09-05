
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class SliderResetButton : UdonSharpBehaviour
{
    [SerializeField] private HUDSettings hudSettings;
    [SerializeField] private Slider slider;
    private float value;

    void Start()
    {
        value = slider.value;
    }

    public void OnClick()
    {
        slider.value = value;
        hudSettings.UpdateValues();
    }
}
