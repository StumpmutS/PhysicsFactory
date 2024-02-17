using UnityEngine;
using UnityEngine.Events;

public class ViewSelectionActions : MonoBehaviour
{
    [SerializeField] private Viewable viewable;
    [SerializeField] private Selectable selectable;

    public UnityEvent<Selectable> OnHover = new();
    public UnityEvent<Selectable> OnHoverStop = new();
    public UnityEvent<Selectable> OnSelect = new();
    public UnityEvent<Selectable> OnDeselect = new();
    public UnityEvent<Selectable> OnEngage = new();
    public UnityEvent<Selectable> OnDisengage = new();

    private void Awake()
    {
        if (selectable != null)
        {
            selectable.OnHover.AddListener(HandleHover);
            selectable.OnHoverStop.AddListener(HandleHoverStop);
            selectable.OnSelect.AddListener(HandleSelect);
            selectable.OnDeselect.AddListener(HandleDeselect);
            selectable.OnEngage.AddListener(HandleEngage);
            selectable.OnDisengage.AddListener(HandleDisengage);
        }

        if (viewable != null)
        {
            viewable.OnActivation.AddListener(HandleViewActivation);
            viewable.OnDeactivation.AddListener(HandleViewDeactivation);
        }
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

    private void HandleViewActivation()
    {
        if (selectable == null) return;
        if (selectable.Selected) OnSelect.Invoke(selectable);
        if (selectable.Engaged) OnEngage.Invoke(selectable);
        if (selectable.Hovered) OnHover.Invoke(selectable);
    }

    private void HandleViewDeactivation()
    {
        if (selectable == null) return;
        if (selectable.Selected) OnDeselect.Invoke(selectable);
        if (selectable.Engaged) OnDisengage.Invoke(selectable);
        if (selectable.Hovered) OnHoverStop.Invoke(selectable);
    }
    
    private void OnDestroy()
    {
        if (selectable != null) selectable.OnHover.RemoveListener(HandleHover);
        if (selectable != null) selectable.OnHoverStop.RemoveListener(HandleHoverStop);
        if (selectable != null) selectable.OnSelect.RemoveListener(HandleSelect);
        if (selectable != null) selectable.OnDeselect.RemoveListener(HandleDeselect);
        if (selectable != null) selectable.OnEngage.RemoveListener(HandleEngage);
        if (selectable != null) selectable.OnDisengage.RemoveListener(HandleDisengage);
        if (viewable != null) viewable.OnActivation.AddListener(HandleViewActivation);
        if (viewable != null) viewable.OnDeactivation.AddListener(HandleViewDeactivation);
    }
}