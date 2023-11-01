using System;
using System.Collections.Generic;

public static class SelectionDisabler
{
    public static bool Disabled { get; private set; }
    
    private static HashSet<object> _disablers = new();
    
    public static event Action OnDisable = delegate { };
    public static event Action OnEnable = delegate { };

    public static void Enable(object caller)
    {
        _disablers.Remove(caller);
        if (_disablers.Count < 1) Disabled = false;
    }

    public static void Disable(object caller)
    {
        Disabled = true;
        _disablers.Add(caller);
    }
}