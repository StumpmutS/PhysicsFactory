using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickEvents : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnLeftClick = new();
    public UnityEvent OnRightClick = new();
    public UnityEvent OnControlLeftClick = new();
    public UnityEvent OnControlRightClick = new();
    
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left when InputTranslationManager.Instance.ControlHeld:
                OnControlLeftClick.Invoke();
                break;
            case PointerEventData.InputButton.Left:
                OnLeftClick.Invoke();
                break;
            case PointerEventData.InputButton.Right when InputTranslationManager.Instance.ControlHeld:
                OnControlRightClick.Invoke();
                break;
            case PointerEventData.InputButton.Right:
                OnRightClick.Invoke();
                break;
        }
    }
}