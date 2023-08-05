using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField] private CurrentContainer currentContainer;
    [SerializeField] private float energyGenerated;
    public float EnergyGenerated => energyGenerated;

    public UnityEvent OnCurrentsChanged;

    private void Awake()
    {
        currentContainer.OnCurrentsChanged.AddListener(UpdateCurrentCharges);
    }

    private void UpdateCurrentCharges()
    {
        foreach (var current in currentContainer.Currents)
        {
            current.SetCharge(energyGenerated / currentContainer.Currents.Count);
        }
        OnCurrentsChanged.Invoke();
    }

    private void OnDestroy()
    {
        if (currentContainer != null) currentContainer.OnCurrentsChanged.RemoveListener(UpdateCurrentCharges);
    }
}