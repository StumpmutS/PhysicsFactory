using UnityEngine;

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
        
        SetupSelectionDisplay(component);
        selectable.OnDeselect.AddListener(HandleDeselection);
    }

    private void HandleEngagement(Selectable selectable)
    {
        if (!selectable.MainObject.TryGetComponent<T>(out var component)) return;
        
        SetupEngagementDisplay(component);
        selectable.OnDisengage.AddListener(HandleDisengagement);
    }

    protected virtual void SetupSelectionDisplay(T component) { }

    protected virtual void SetupEngagementDisplay(T component) { }

    private void HandleDeselection(Selectable selectable)
    {
        selectable.OnDeselect.RemoveListener(HandleDeselection);
        RemoveSelectionDisplay();
    }

    private void HandleDisengagement(Selectable selectable)
    {
        selectable.OnDisengage.RemoveListener(HandleDisengagement);
        RemoveEngagementDisplay();
    }

    protected virtual void RemoveSelectionDisplay() { }

    protected virtual void RemoveEngagementDisplay() { }
}