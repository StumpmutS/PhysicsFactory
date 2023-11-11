using UnityEngine;
using UnityEngine.Events;

public class Selectable : MonoBehaviour
{
    [SerializeField] private GameObject mainObject;
    public GameObject MainObject => mainObject;

    public bool Hovered { get; private set; }
    public bool Selected { get; private set; }
    public bool Engaged { get; private set; }
    
    public UnityEvent<Selectable> OnHover;
    public UnityEvent<Selectable> OnHoverStop;
    public UnityEvent<Selectable> OnSelect;
    public UnityEvent<Selectable> OnDeselect;
    public UnityEvent<Selectable> OnEngage;
    public UnityEvent<Selectable> OnDisengage;

    private void Start()
    {
        SelectionEvents.Instance.RegisterSelectable(this);
    }

    public void Hover()
    {
        Hovered = true;
        OnHover.Invoke(this);
    }

    public void StopHover()
    {
        Hovered = false;
        OnHoverStop.Invoke(this);
    }
    
    public void Select()
    {
        Selected = true;
        OnSelect.Invoke(this);
    }

    public void Deselect()
    {
        Selected = false;
        OnDeselect.Invoke(this);
    }
    
    public void Engage()
    {
        Engaged = true;
        OnEngage.Invoke(this);
    }

    public void Disengage()
    {
        Engaged = false;
        OnDisengage.Invoke(this);
    }

    private void OnDisable()
    {
        DeactivateInteractions();
    }

    private void OnDestroy()
    {
        DeactivateInteractions();
        SelectionEvents.Instance.DeregisterSelectable(this);
    }

    private void DeactivateInteractions()
    {
        StopHover();
        Disengage();
        Deselect();
    }
}