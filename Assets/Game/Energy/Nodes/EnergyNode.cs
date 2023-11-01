using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class EnergyNode : MonoBehaviour
{
    [SerializeField] private CurrentContainer currentContainer;
    public CurrentContainer CurrentContainer => currentContainer;

    public bool TryConnect(EnergyNode other)
    {
        if (!CanConnect(other, out var sender, out var receiver)) return false;
        
        InitiateCurrent(sender, receiver, other);
        return true;
    }

    public abstract bool CanConnect(EnergyNode other, out CurrentContainer sender, out CurrentContainer receiver);

    public bool TryDisconnect(EnergyNode other)
    {
        bool disconnected = false;
        var currentsCopy = currentContainer.Currents.ToList();
        foreach (var current in currentsCopy)
        {
            if ((current.Sender == currentContainer && current.Receiver == other.currentContainer) ||
                (current.Sender == other.currentContainer && current.Receiver == currentContainer))
            {
                disconnected = true;
                current.ShutDown();
            }
        }

        return disconnected;
    }
    
    private void InitiateCurrent(CurrentContainer from, CurrentContainer to, EnergyNode other)
    {
        if (currentContainer.Currents.Any(current => current.Sender == from && current.Receiver == to)) return;
        var current = new EnergyCurrent(from, to);
        RegisterCurrent(current);
        other.RegisterCurrent(current);
    }

    private void RegisterCurrent(EnergyCurrent current)
    {
        currentContainer.AddCurrent(current);
    }
}