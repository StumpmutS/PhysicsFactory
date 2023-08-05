using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CurrentContainer : MonoBehaviour
{
    public HashSet<EnergyCurrent> Currents { get; private set; } = new();

    public UnityEvent OnCurrentsChanged;
    
    public void AddCurrent(EnergyCurrent current)
    {
        if (!Currents.Add(current)) return;
        
        current.OnChargeChanged += HandleChargeChanged;
        OnCurrentsChanged.Invoke();
    }
    
    public void RemoveCurrent(EnergyCurrent current)
    {
        if (!Currents.Remove(current)) return;
        
        current.OnChargeChanged -= HandleChargeChanged;
        OnCurrentsChanged.Invoke();
    }

    private void HandleChargeChanged()
    {
        OnCurrentsChanged.Invoke();
    }

    private void OnDestroy()
    {
        var currentsCopy = Currents.ToList();
        foreach (var current in currentsCopy)
        {
            current.ShutDown();
        }
    }
}