using System.Collections.Generic;
using UnityEngine.Events;
using Utility.Scripts;

public class SelectionEvents : Singleton<SelectionEvents>
{
    private HashSet<Selectable> _selectables = new();

    public UnityEvent<Selectable> OnHovered;
    public UnityEvent<Selectable> OnHoverStopped;
    public UnityEvent<Selectable> OnSelected;
    public UnityEvent<Selectable> OnDeselected;
    public UnityEvent<Selectable> OnEngaged;
    public UnityEvent<Selectable> OnDisengaged;

    public void RegisterSelectable(Selectable selectable)
    {
        if (!_selectables.Add(selectable)) return;
        
        selectable.OnHover.AddListener(HandleHover);
        selectable.OnHoverStop.AddListener(HandleHoverStop);
        selectable.OnSelect.AddListener(HandleSelection);
        selectable.OnDeselect.AddListener(HandleDeselection);
        selectable.OnEngage.AddListener(HandleEngagement);
        selectable.OnDisengage.AddListener(HandleDisengagement);
    }

    public void DeregisterSelectable(Selectable selectable)
    {
        if (!_selectables.Remove(selectable)) return;
        selectable.OnHover.RemoveListener(HandleHover);
        selectable.OnHoverStop.RemoveListener(HandleHoverStop);
        selectable.OnSelect.RemoveListener(HandleSelection);
        selectable.OnDeselect.RemoveListener(HandleDeselection);
        selectable.OnEngage.RemoveListener(HandleEngagement);
        selectable.OnDisengage.RemoveListener(HandleDisengagement);
    }

    private void HandleHover(Selectable selectable)
    {
        OnHovered.Invoke(selectable);
    }

    private void HandleHoverStop(Selectable selectable)
    {
        OnHoverStopped.Invoke(selectable);
    }
    
    private void HandleSelection(Selectable selectable)
    {
        OnSelected.Invoke(selectable);
    }

    private void HandleDeselection(Selectable selectable)
    {
        OnDeselected.Invoke(selectable);
    }
    
    private void HandleEngagement(Selectable selectable)
    {
        OnEngaged.Invoke(selectable);
    }

    private void HandleDisengagement(Selectable selectable)
    {
        OnDisengaged.Invoke(selectable);
    }
}