using UnityEngine;
using UnityEngine.UI;

public class ToggleColorChanger : ToggleChanger
{
    [SerializeField] private Image targetImage;
    [SerializeField] private ColorData colors;

    protected override void ChangeValue(bool value)
    {
        targetImage.color = value ? colors.Colors[1] : colors.Colors[0];
    }
}