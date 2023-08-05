using UnityEngine;
using UnityEngine.Events;

public class GameMenu : MonoBehaviour
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
        OnActivation.Invoke();
    }

    public void TryDeactivate()
    {
        if (!_active) return;
        
        _active = false;
        Deactivate();
    }

    private void Deactivate()
    {
        OnDeactivation.Invoke();
    }
}