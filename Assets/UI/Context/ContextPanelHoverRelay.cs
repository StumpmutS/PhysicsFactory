using System.Collections;
using UnityEngine;
using Utility.Scripts.Extensions;

public class ContextPanelHoverRelay : ContextPanelRelay
{
    [SerializeField] private UIHoverable uiHoverable;

    private UIHoverable _panelHoverable;

    protected override void Awake()
    {
        base.Awake();
        if (uiHoverable == null) uiHoverable = this.AddOrGetComponent<UIHoverable>();
    }

    protected override void HandleDisplay(ContextPanel panel)
    { 
        _panelHoverable = panel.AddOrGetComponent<UIHoverable>();
        
        uiHoverable.OnHoverStop.AddListener(HandleHoverableHoverStop);
        _panelHoverable.OnHoverStop.AddListener(HandlePanelHoverStop);
    }

    private void HandleHoverableHoverStop()
    {
        if (_panelHoverable.Hovered) return;

        StartCoroutine(CoTryRemoveDisplay());
    }

    private void HandlePanelHoverStop()
    {
        if (uiHoverable.Hovered) return;

        StartCoroutine(CoTryRemoveDisplay());
    }

    private IEnumerator CoTryRemoveDisplay()
    {
        yield return new WaitForEndOfFrame();

        if (uiHoverable.Hovered || _panelHoverable.Hovered) yield break;
        
        RemoveDisplay();
    }
}
