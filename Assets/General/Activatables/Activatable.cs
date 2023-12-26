using UnityEngine;
using UnityEngine.Events;

public class Activatable : MonoBehaviour
{
    public UnityEvent OnActivation;
    public UnityEvent OnDeactivation;

    private bool _active;
    
    public void TryActivate()
    {
        if (_active) return;
        
        _active = true;
        Activate();
    }

    private void Activate()
    {
        HandleActivation();
        OnActivation.Invoke();
    }

    protected virtual void HandleActivation() { }

    public void RefreshActivation()
    {
        if (!_active) return;
        
        Activate();
    }

    public void TryDeactivate()
    {
        if (!_active) return;
        
        _active = false;
        Deactivate();
    }

    private void Deactivate()
    {
        HandleDeactivation();
        OnDeactivation.Invoke();
    }
    
    protected virtual void HandleDeactivation() { }
}