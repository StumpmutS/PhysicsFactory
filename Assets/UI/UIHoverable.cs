using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIHoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _hovered;
    public bool Hovered
    {
        get => _hovered;
        private set
        {
            if (_hovered == value) return;
            
            _hovered = value;
            if (_hovered) OnHovered.Invoke();
            else OnHoverStop.Invoke();
        }
    }

    public UnityEvent OnHovered = new();
    public UnityEvent OnHoverStop = new();

    public void OnPointerEnter(PointerEventData eventData) => Hovered = true;

    public void OnPointerExit(PointerEventData eventData) => Hovered = false;
}