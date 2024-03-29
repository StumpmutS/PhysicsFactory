﻿using UnityEngine;

public class ToggleSpringColorChanger : ToggleChanger
{
    [SerializeField] private ColorSpringListener colorSpring;
    [SerializeField] private ColorSO colors;

    protected override void ChangeValue(bool value)
    {
        colorSpring.SetOrig(value ? colors.Colors[1] : colors.Colors[0]);
    }
}