using System;
using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;
using Utility.Scripts.Extensions;

public class LayoutDrivenToggleGroup : MonoBehaviour
{
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private LayoutDisplay layoutDisplay;

    private void Awake()
    {
        layoutDisplay.OnAdd.AddListener(HandleAdd);
    }

    private void HandleAdd(RectTransform rectTransform)
    {
        if (!rectTransform.TryGetComponentInChildren<Toggle>(out var toggle)) return;
        
        toggle.group = toggleGroup; //this does not always set first toggle to true
        if (!toggleGroup.allowSwitchOff && layoutDisplay.Children.Count == 1) toggle.isOn = true;
    }

    private void OnDestroy()
    {
        if (layoutDisplay != null) layoutDisplay.OnAdd.RemoveListener(HandleAdd);
    }
}