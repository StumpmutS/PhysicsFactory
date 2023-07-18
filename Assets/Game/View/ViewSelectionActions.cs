using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ViewSelectionActions : MonoBehaviour
{
    [SerializeField] private Viewable viewable;
    [SerializeField] private Selectable selectable;

    public UnityEvent<Selectable> OnHover;
    public UnityEvent<Selectable> OnHoverStop;
    public UnityEvent<Selectable> OnSelect;
    public UnityEvent<Selectable> OnDeselect;
    public UnityEvent<Selectable> OnEngage;
    public UnityEvent<Selectable> OnDisengage;

    private void Awake()
    {
        if (selectable == null) return;
        selectable.OnHover.AddListener(HandleHover);
        selectable.OnHoverStop.AddListener(HandleHoverStop);
        selectable.OnSelect.AddListener(HandleSelect);
        selectable.OnDeselect.AddListener(HandleDeselect);
        selectable.OnEngage.AddListener(HandleEngage);
        selectable.OnDisengage.AddListener(HandleDisengage);
    }

    public void HandleHover(Selectable selectableParameter)
    {
        if (!viewable.Active) return;
        OnHover.Invoke(selectableParameter);
    }

    public void HandleHoverStop(Selectable selectableParameter)
    {
        if (!viewable.Active) return;
        OnHoverStop.Invoke(selectableParameter);
    }

    public void HandleSelect(Selectable selectableParameter)
    {
        if (!viewable.Active) return;
        OnSelect.Invoke(selectableParameter);
    }

    public void HandleDeselect(Selectable selectableParameter)
    {
        if (!viewable.Active) return;
        OnDeselect.Invoke(selectableParameter);
    }

    public void HandleEngage(Selectable selectableParameter)
    {
        if (!viewable.Active) return;
        OnEngage.Invoke(selectableParameter);
    }

    public void HandleDisengage(Selectable selectableParameter)
    {
        if (!viewable.Active) return;
        OnDisengage.Invoke(selectableParameter);
    }

    private void OnDestroy()
    {
        if (selectable == null) return;
        selectable.OnHover.RemoveListener(HandleHover);
        selectable.OnHoverStop.RemoveListener(HandleHoverStop);
        selectable.OnSelect.RemoveListener(HandleSelect);
        selectable.OnDeselect.RemoveListener(HandleDeselect);
        selectable.OnEngage.RemoveListener(HandleEngage);
        selectable.OnDisengage.RemoveListener(HandleDisengage);
    }
}