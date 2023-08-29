using UnityEngine;
using UnityEngine.Events;

public abstract class SelectableDisplay<T> : MonoBehaviour where T : Component
{
    private void Start()
    {
        SelectionEvents.Instance.OnSelected.AddListener(HandleSelection);
        SelectionEvents.Instance.OnEngaged.AddListener(HandleEngagement);
    }
    
    private void HandleSelection(Selectable selectable)
    {
        if (!selectable.MainObject.TryGetComponent<T>(out var component)) return;
        SetupSelectionDisplay(selectable, component);
        selectable.OnDeselect.AddListener(HandleDeselection);
    }

    private void HandleEngagement(Selectable selectable)
    {
        if (!selectable.MainObject.TryGetComponent<T>(out var component)) return;
        
        SetupEngagementDisplay(selectable, component);
        selectable.OnDisengage.AddListener(HandleDisengagement);
    }

    protected virtual void SetupSelectionDisplay(Selectable selectable, T component) { }

    protected virtual void SetupEngagementDisplay(Selectable selectable, T component) { }

    private void HandleDeselection(Selectable selectable)
    {
        selectable.OnDeselect.RemoveListener(HandleDeselection);
        RemoveSelectionDisplay(selectable);
    }

    private void HandleDisengagement(Selectable selectable)
    {
        selectable.OnDisengage.RemoveListener(HandleDisengagement);
        RemoveEngagementDisplay(selectable);
    }

    protected virtual void RemoveSelectionDisplay(Selectable selectable) { }

    protected virtual void RemoveEngagementDisplay(Selectable selectable) { }
}