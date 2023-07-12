using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField] private int energyGenerated;

    private HashSet<EnergyCurrent> _currents = new();

    public void RequestEnergy(EnergyCurrent current)
    {
        _currents.Add(current);
        UpdateCurrentCharges();
    }

    private void UpdateCurrentCharges()
    {
        foreach (var current in _currents)
        {
            current.SetCharge(Mathf.FloorToInt((float)energyGenerated / _currents.Count));
        }
    }
}