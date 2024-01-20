using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class ContextPanelManager : Singleton<ContextPanelManager>
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private ContextPanel contextPanelPrefab;
    [SerializeField] private RectTransform floatingPanelContainer;
    [SerializeField] private RectTransform lockedPanelContainer;

    private object _lockedCaller;
    private ContextPanel _lockedPanel;
    private Dictionary<object, ContextPanel> _displayedPanels = new();
    
    public ContextPanel DisplayFloatingPanel(object caller, ContextData data)
    {
        TryRemoveFloatingDisplay(caller);
        var panel = DisplayPanel(floatingPanelContainer, data);
        ScreenFormatter.PositionRectAboutPoint(floatingPanelContainer, Input.mousePosition, canvas);
        _displayedPanels[caller] = panel;
        return panel;
    }

    public void TryRemoveFloatingDisplay(object caller)
    {
        if (!_displayedPanels.TryGetValue(caller, out var panel)) return;
        
        DestroyImmediate(panel.gameObject);
        _displayedPanels.Remove(caller);
    }

    public void DisplayLockedPanel(object caller, ContextData data)
    {
        if (_lockedCaller != null) RemoveLockedDisplay(_lockedCaller);
        
        var panel = DisplayPanel(lockedPanelContainer, data);
        _lockedCaller = caller;
        _lockedPanel = panel;
    }

    public void RemoveLockedDisplay(object caller)
    {
        if (caller != _lockedCaller) return;
        
        Destroy(_lockedPanel.gameObject);
        _lockedCaller = null;
        _lockedPanel = null;
    }

    private ContextPanel DisplayPanel(RectTransform container, ContextData context)
    {
        var panel = Instantiate(contextPanelPrefab, container, false);
        panel.Display(context);
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(container);
        LayoutRebuilder.ForceRebuildLayoutImmediate(panel.transform as RectTransform);
        LayoutRebuilder.ForceRebuildLayoutImmediate(container);

        return panel;
    }
}