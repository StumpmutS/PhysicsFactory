using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleColorChanger : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image targetImage;
    [SerializeField] private ColorInfo colors;

    private void Awake()
    {
        toggle.onValueChanged.AddListener(HandleToggle);
    }

    private void Start()
    {
        HandleToggle(toggle.isOn);
    }

    private void HandleToggle(bool value)
    {
        targetImage.color = value ? colors.Colors[1] : colors.Colors[0];
    }
}
