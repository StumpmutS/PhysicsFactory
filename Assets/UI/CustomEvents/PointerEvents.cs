using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PointerEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private bool _entered;
    private bool _pointerDown;
    
    public UnityEvent OnPointerEnterDown = new();
    public UnityEvent OnPointerEnterUp = new();
    public UnityEvent OnPointerExitDown = new();
    public UnityEvent OnPointerExitUp = new();
    public UnityEvent OnPointerDownEntered = new();
    public UnityEvent OnPointerDownExited = new();
    public UnityEvent OnPointerUpEntered = new();
    public UnityEvent OnPointerUpExited = new();

    public void OnPointerEnter(PointerEventData eventData)
    {
        _entered = true;
        if (_pointerDown) OnPointerEnterDown.Invoke();
        else OnPointerEnterUp.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _entered = false;
        if (_pointerDown) OnPointerExitDown.Invoke();
        else OnPointerExitUp.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _pointerDown = true;
        if (_entered) OnPointerDownEntered.Invoke();
        else OnPointerDownExited.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _pointerDown = false;
        if (_entered) OnPointerUpEntered.Invoke();
        else OnPointerUpExited.Invoke();
    }

    private void OnDisable()
    {
        _entered = false;
        _pointerDown = false;
    }
}