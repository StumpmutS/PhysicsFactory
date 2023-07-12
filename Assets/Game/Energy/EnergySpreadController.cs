using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utility.Scripts;

public class EnergySpreadController : MonoBehaviour
{
    [SerializeField] private EnergyContainer container;
    [SerializeField] private List<Component> startingSpenders;

    public IntegerGroup<IEnergySpender> Spenders { get; } = new();

    private void Awake()
    {
        Spenders.MaxTotal = container.TotalCharge;
        
        foreach (var component in startingSpenders)
        {
            if (component is IEnergySpender spender)
            {
                RegisterSpender(spender);
            }
        }

        Spenders.OnIntegersChanged += HandleSpendersChanged;
        container.OnChargeChanged.AddListener(HandleChargeChanged);
    }

    public void RegisterSpender(IEnergySpender spender)
    {
        Spenders.SetValue(spender, 0);
    }

    private void HandleChargeChanged(int value)
    {
        Spenders.MaxTotal = value;
    }

    private void HandleSpendersChanged()
    {
        foreach (var kvp in Spenders.Integers)
        {
            kvp.Key.SetEnergyLevel(kvp.Value.AsInt());
        }
    }

    private void OnDestroy()
    {
        if (container != null) container.OnChargeChanged.RemoveListener(HandleChargeChanged);
        if (Spenders != null) Spenders.OnIntegersChanged -= HandleSpendersChanged;
    }
}