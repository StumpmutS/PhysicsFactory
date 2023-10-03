using UnityEngine;

public class ToggleSpringColorChanger : ToggleChanger
{
    [SerializeField] private ColorSpringListener colorSpring;
    [SerializeField] private ColorInfo colors;

    protected override void HandleToggle(bool value)
    {
        colorSpring.SetOrig(value ? colors.Colors[1] : colors.Colors[0]);
    }
}