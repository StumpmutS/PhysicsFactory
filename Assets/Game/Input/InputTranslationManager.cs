using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utility.Scripts;

public class InputTranslationManager : Singleton<InputTranslationManager>
{
    public bool ControlHeld { get; private set; }
    
    public UnityEvent<Vector2> OnMove;
    public UnityEvent<Vector2> OnLook;
    public UnityEvent OnInteractDown;
    public UnityEvent OnInteractUp;
    public UnityEvent OnInteractNonUIDown;
    public UnityEvent OnInteractNonUIUp;
    public UnityEvent OnEngageDown;
    public UnityEvent OnEngageUp;
    public UnityEvent OnEngageNonUIDown;
    public UnityEvent OnEngageNonUIUp;
    public UnityEvent<float> OnScroll;
    public UnityEvent<bool> OnInspect;
    public UnityEvent<Vector2> OnLevelChange;
    public UnityEvent OnResetDown;
    public UnityEvent OnResetUp;
    public UnityEvent OnGridDown;
    public UnityEvent OnGridUp;
    public UnityEvent OnIsolateDown;
    public UnityEvent OnIsolateUp;

    public void Move(InputAction.CallbackContext ctx)
    {
        OnMove.Invoke(ctx.ReadValue<Vector2>());
    }
    
    public void Look(InputAction.CallbackContext ctx)
    {
        OnLook.Invoke(ctx.ReadValue<Vector2>());
    }

    private bool _cachedInteractValue;
    public void Interact(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValueAsButton();
        if (value == _cachedInteractValue) return;
        
        _cachedInteractValue = value;
        
        if (value)
        {
            OnInteractDown.Invoke();
            if (!UIHoveredReference.Instance.OverUI()) OnInteractNonUIDown.Invoke();
        }
        else
        {
            OnInteractUp.Invoke();
            if (!UIHoveredReference.Instance.OverUI()) OnInteractNonUIUp.Invoke();
        }
    }

    private bool _cachedEngageValue;
    public void Engage(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValueAsButton();
        if (value == _cachedEngageValue) return;
        
        _cachedEngageValue = value;
        
        if (value)
        {
            OnEngageDown.Invoke();
            if (!UIHoveredReference.Instance.OverUI()) OnEngageNonUIDown.Invoke();
        }
        else
        {
            OnEngageUp.Invoke();
            if (!UIHoveredReference.Instance.OverUI()) OnEngageNonUIUp.Invoke();
        }
    }
    
    public void Scroll(InputAction.CallbackContext ctx)
    {
        OnScroll.Invoke(ctx.ReadValue<float>());
    }

    public void Inspect(InputAction.CallbackContext ctx)
    {
        OnInspect.Invoke(ctx.ReadValueAsButton());
    }

    private Vector2 _cachedLevelChangeValue;
    public void LevelChange(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValue<Vector2>();
        if (value == _cachedLevelChangeValue) return;

        _cachedLevelChangeValue = value;

        OnLevelChange.Invoke(value);
    }

    private bool _cachedResetValue;
    public void Reset(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValueAsButton();
        if (value == _cachedResetValue) return;
        _cachedResetValue = value;
        
        if (value)
        {
            OnResetDown.Invoke();
        }
        else
        {
            OnResetUp.Invoke();
        }
    }

    private bool _cachedGridValue;
    public void Grid(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValueAsButton();
        if (value == _cachedGridValue) return;
        _cachedGridValue = value;
        
        if (value)
        {
            OnGridDown.Invoke();
        }
        else
        {
            OnGridUp.Invoke();
        }
    }

    private bool _cachedIsolateValue;
    public void Isolate(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValueAsButton();
        if (value == _cachedIsolateValue) return;
        _cachedIsolateValue = value;
        
        if (value)
        {
            OnIsolateDown.Invoke();
        }
        else
        {
            OnIsolateUp.Invoke();
        }
    }

    private bool _cachedControlValue;

    public void Control(InputAction.CallbackContext ctx)
    {
        var value = ctx.ReadValueAsButton();
        if (value == _cachedControlValue) return;
        _cachedControlValue = value;

        ControlHeld = value;
    }
}