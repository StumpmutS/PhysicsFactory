using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class CurrentContainer : MonoBehaviour
{
    [SerializeField] private IdentifiableObject identifiableObject;
    
    public HashSet<EnergyCurrent> Currents { get; private set; } = new();

    public UnityEvent OnCurrentsChanged = new();
    
    public void AddCurrent(EnergyCurrent current)
    {
        if (!Currents.Add(current)) return;
        
        current.OnChargeChanged += HandleChargeChanged;
        current.OnShutDown += RemoveCurrent;
        OnCurrentsChanged.Invoke();
    }
    
    private void RemoveCurrent(EnergyCurrent current)
    {
        if (!Currents.Remove(current)) return;
        
        current.OnChargeChanged -= HandleChargeChanged;
        current.OnShutDown -= RemoveCurrent;
        OnCurrentsChanged.Invoke();
    }

    private void HandleChargeChanged()
    {
        OnCurrentsChanged.Invoke();
    }

    private void ShutDownCurrents()
    {
        var currentsCopy = Currents.ToList();
        foreach (var current in currentsCopy)
        {
            current.ShutDown();
        }
    }

    public void HandleSale()
    {
        ShutDownCurrents();
    }
}