using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility.Scripts;

public class ContextPanelManager : Singleton<ContextPanelManager>
{
    [SerializeField] private ContextPanel contextPanelPrefab;
    [SerializeField] private RectTransform lockedPanelContainer;

    private object _lockedCaller;
    private ContextPanel _lockedPanel;
    private Dictionary<object, ContextPanel> _displayedPanels = new();

    public void DisplayPanel(object caller, ContextData data)
    {
        var panel = Instantiate(contextPanelPrefab);
        ScreenFormatter.FormatRect(panel.transform as RectTransform, Input.mousePosition);
        panel.Display(data);
        _displayedPanels[caller] = panel;
    }

    public void RemoveDisplay(object caller)
    {
        if (!_displayedPanels.TryGetValue(caller, out var panel)) return;
        
        Destroy(panel.gameObject);
        _displayedPanels.Remove(caller);
    }

    public void DisplayLockedPanel(object caller, ContextData data)
    {
        if (_lockedCaller != null) RemoveLockedDisplay(_lockedCaller);
        
        var panel = Instantiate(contextPanelPrefab, lockedPanelContainer, false);
        panel.Display(data);
        _lockedCaller = caller;
        _lockedPanel = panel;
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(lockedPanelContainer);
        LayoutRebuilder.ForceRebuildLayoutImmediate(panel.transform as RectTransform);
    }

    public void RemoveLockedDisplay(object caller)
    {
        if (caller != _lockedCaller) return;
        
        Destroy(_lockedPanel.gameObject);
        _lockedCaller = null;
        _lockedPanel = null;
    }
}