using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColorChanger : ToggleChanger
{
    [SerializeField] private Image targetImage;
    [SerializeField] private ColorInfo colors;

    protected override void HandleToggle(bool value)
    {
        targetImage.color = value ? colors.Colors[1] : colors.Colors[0];
    }
}