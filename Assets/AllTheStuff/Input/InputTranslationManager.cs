using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Utility.Scripts;

public class InputTranslationManager : Singleton<InputTranslationManager>
{
    public UnityEvent<Vector2> OnMove;
    public UnityEvent<Vector2> OnLook;
    public UnityEvent<bool> OnInteract;
    public UnityEvent<float> OnScroll;
    public UnityEvent<bool> OnInspect;

    public void Move(InputAction.CallbackContext ctx)
    {
        OnMove.Invoke(ctx.ReadValue<Vector2>());
    }
    
    public void Look(InputAction.CallbackContext ctx)
    {
        OnLook.Invoke(ctx.ReadValue<Vector2>());
    }
    
    public void Interact(InputAction.CallbackContext ctx)
    {
        OnInteract.Invoke(ctx.ReadValueAsButton());
    }
    
    public void Scroll(InputAction.CallbackContext ctx)
    {
        OnScroll.Invoke(ctx.ReadValue<float>());
    }

    public void Inspect(InputAction.CallbackContext ctx)
    {
        OnInspect.Invoke(ctx.ReadValueAsButton());
    }
}