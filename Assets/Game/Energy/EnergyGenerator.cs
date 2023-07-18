using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField] private float energyGenerated;

    public HashSet<EnergyCurrent> Currents { get; private set; } = new();

    public void RequestEnergy(EnergyCurrent current)
    {
        Currents.Add(current);
        UpdateCurrentCharges();
    }

    private void UpdateCurrentCharges()
    {
        foreach (var current in Currents)
        {
            current.SetCharge(energyGenerated / Currents.Count);
        }
    }
}