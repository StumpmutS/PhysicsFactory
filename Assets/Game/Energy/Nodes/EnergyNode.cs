using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class EnergyNode : MonoBehaviour
{
    private HashSet<EnergyCurrent> _currents = new();

    public void InitiateCurrent(EnergyGenerator from, EnergyContainer to, EnergyNode other)
    {
        if (_currents.Any(current => current.Sender == from && current.Receiver == to)) return;
        var current = new EnergyCurrent(from, to);
        _currents.Add(current);
        other.RegisterCurrent(current);
    }

    private void RegisterCurrent(EnergyCurrent current)
    {
        _currents.Add(current);
    }
}