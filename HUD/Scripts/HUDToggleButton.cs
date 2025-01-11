
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

    void Start() {
        image = this.GetComponent<Image>();
        SetColor();
    }

    public void OnClick() {
        target.SetActive(!target.activeSelf);
        SetColor();
    }

    private void SetColor() => image.color = target.activeSelf ? hudSettings.OnColor : hudSettings.OffColor;
}