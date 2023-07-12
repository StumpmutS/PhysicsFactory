using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OutlineController : MonoBehaviour
{
    [SerializeField] private Selectable selectable;
    [SerializeField] private Outline outline;

    public UnityEvent OnOutline;
    public UnityEvent OnRemoveOutline;

    private void Awake()
    {
        selectable.OnHover.AddListener(HandleHovered);
        selectable.OnHoverStop.AddListener(HandleHoverStop);
        selectable.OnSelect.AddListener(HandleSelected);
        selectable.OnDeselect.AddListener(HandleDeselected);
        selectable.OnEngage.AddListener(HandleEngaged);
        selectable.OnDisengage.AddListener(HandleDisengaged);
    }

    private void HandleHovered(Selectable _)
    {
        Outline();
    }

    private void HandleHoverStop(Selectable _)
    {
        if (!selectable.Selected && !selectable.Engaged) RemoveOutline();
    }
    
    private void HandleSelected(Selectable _)
    {
        Outline();
    }

    private void HandleDeselected(Selectable _)
    {
        if (!selectable.Hovered && !selectable.Engaged) RemoveOutline();
    }
    
    private void HandleEngaged(Selectable _)
    {
        Outline();
    }

    private void HandleDisengaged(Selectable _)
    {
        if (!selectable.Hovered && !selectable.Selected) RemoveOutline();
    }

    private void Outline()
    {
        outline.enabled = true;
        OnOutline.Invoke();
    }

    private void RemoveOutline()
    {
        outline.enabled = false;
        OnRemoveOutline.Invoke();
    }

    private void OnDestroy()
    {
        selectable.OnHover.RemoveListener(HandleHovered);
        selectable.OnHoverStop.RemoveListener(HandleHoverStop);
        selectable.OnSelect.RemoveListener(HandleSelected);
        selectable.OnDeselect.RemoveListener(HandleDeselected);
        selectable.OnEngage.RemoveListener(HandleEngaged);
        selectable.OnDisengage.RemoveListener(HandleDisengaged);
    }
}
