using System;
using System.Collections.Generic;
using UnityEngine.Events;
using Utility.Scripts;
using Utility.Scripts.Extensions;

public class EscapeManager : Singleton<EscapeManager>
{
    private LinkedList<IEscapable> _escapables = new();

    public UnityEvent OnEscapeStackAvailable = new();
    
    private void Start()
    {
        InputTranslationManager.Instance.OnEscapeDown.AddListener(HandleEscape);
    }

    private void HandleEscape()
    {
        if (_escapables.TryPopLast(out var escapable))
        {
            escapable.Escape();
            return;
        }

        OnEscapeStackAvailable.Invoke();
    }

    public void RegisterEscapable(IEscapable escapable)
    {
        _escapables.Remove(escapable);
        _escapables.AddLast(escapable);
    }

    public void DeregisterEscapable(IEscapable escapable)
    {
        _escapables.Remove(escapable);
    }

    private void OnDestroy()
    {
        if (InputTranslationManager.Instance != null) InputTranslationManager.Instance.OnEscapeDown.RemoveListener(HandleEscape);
    }
}