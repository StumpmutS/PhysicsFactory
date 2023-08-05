using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;

public class EnergyNode : MonoBehaviour
{
    [SerializeField] private CurrentContainer currentContainer;
    [SerializeField] private bool generator;

    public bool CanConnect(EnergyNode other, out CurrentContainer sender, out CurrentContainer receiver)
    {
        sender = generator ? currentContainer : other.currentContainer;
        receiver = generator ? other.currentContainer : currentContainer;
        return other.generator != generator;
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