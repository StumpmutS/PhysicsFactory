using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OutlineColorController : MonoBehaviour
{
    [SerializeField] private ColorInfo colors;
    [SerializeField] private Outline outline;
    [SerializeField] private OutlineController outlineController;
    [SerializeField] private Selectable selectable;

    private void Awake()
    {
        outlineController.OnOutline.AddListener(HandleOutline);
    }

    private void HandleOutline()
    {
        SetColor(DetermineColor());
    }

    private void SetColor(Color color)
    {
        outline.OutlineColor = color;
    }

    private Color DetermineColor()
    {
        if (selectable.Selected && selectable.Engaged) return colors.Colors[2];
        return selectable.Engaged ? colors.Colors[1] : colors.Colors[0];
    }
}
