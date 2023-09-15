using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class EnergyNode : MonoBehaviour
{
    [SerializeField] private CurrentContainer currentContainer;
    [SerializeField] private ENodeType nodeType;
    public ENodeType NodeType => nodeType;

    public bool TryConnect(EnergyNode other)
    {
        if (!CanConnect(other, out var sender, out var receiver)) return false;
        
        InitiateCurrent(sender, receiver, other);
        return true;
    }
    
    private bool CanConnect(EnergyNode other, out CurrentContainer sender, out CurrentContainer receiver)
    {
        sender = nodeType == ENodeType.Generator ? currentContainer : other.currentContainer;
        receiver = nodeType == ENodeType.Spender ? currentContainer : other.currentContainer;
        return other.nodeType != nodeType;
    }

    public void ShutDownNodeConnection(EnergyNode other)
    {
        foreach (var current in currentContainer.Currents.ToList())
        {
            if ((current.Sender == currentContainer && current.Receiver == other.currentContainer) ||
                (current.Sender == other.currentContainer && current.Receiver == currentContainer))
            {
                current.ShutDown();
            }
        }
    }
    
    public void InitiateCurrent(CurrentContainer from, CurrentContainer to, EnergyNode other)
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