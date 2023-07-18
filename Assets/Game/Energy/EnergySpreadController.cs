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

    public FloatGroup<IEnergySpender> Spenders { get; } = new();

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

        Spenders.OnFloatsChanged += HandleSpendersChanged;
        container.OnChargeChanged.AddListener(HandleChargeChanged);
    }

    public void RegisterSpender(IEnergySpender spender)
    {
        Spenders.SetValue(spender, new SignedFloat(0, true));
    }

    private void HandleChargeChanged(float value)
    {
        Spenders.MaxTotal = value;
    }

    private void HandleSpendersChanged()
    {
        foreach (var kvp in Spenders.Floats)
        {
            kvp.Key.SetEnergyLevel(kvp.Value.AsFloat());
        }
    }

    private void OnDestroy()
    {
        if (container != null) container.OnChargeChanged.RemoveListener(HandleChargeChanged);
        if (Spenders != null) Spenders.OnFloatsChanged -= HandleSpendersChanged;
    }
}