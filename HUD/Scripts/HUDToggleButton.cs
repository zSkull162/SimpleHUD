
using UdonSharp;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class HUDToggleButton : UdonSharpBehaviour
{
    [SerializeField] private HUDSettings hudSettings;
    [SerializeField] private GameObject target;
    private Image image;
    private Color onColor;
    private Color offColor;

    void Start()
    {
        // I'm using GetProgramVariable here, because the toggle colors are private variables, and GetProgramVariable bypasses that protection level.
        onColor = (Color)hudSettings.GetProgramVariable("togglesOnColor");
        offColor = (Color)hudSettings.GetProgramVariable("togglesOffColor");
        image = this.GetComponent<Image>();

        SetColor();
    }

    public void OnClick()
    {
        bool state = target.activeSelf;
        target.SetActive(!state);

        SetColor();
    }

    private void SetColor()
    {
        if (target.activeSelf) image.color = onColor;
        else image.color = offColor;
    }
}