using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;

public class EnergySpreadController : MonoBehaviour
{
    [FormerlySerializedAs("container")] [SerializeField] private EnergyStorage storage;
    [SerializeField] private List<Component> startingSpenders;

    public FloatGroup<IEnergySpender> Spenders { get; } = new();

    private void Awake()
    {
        Spenders.MaxTotal = storage.TotalCharge;
        
        foreach (var component in startingSpenders)
        {
            if (component is IEnergySpender spender)
            {
                RegisterSpender(spender);
            }
        }

        Spenders.OnFloatsChanged += HandleSpendersChanged;
        storage.OnChargeChanged.AddListener(HandleChargeChanged);
    }

    public void RegisterSpender(IEnergySpender spender)
    {
        Spenders.SetValue(spender, new SignedFloat(0, true));
    }

    public void DeregisterSpender(IEnergySpender spender)
    {
        Spenders.Remove(spender);
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
        if (storage != null) storage.OnChargeChanged.RemoveListener(HandleChargeChanged);
        if (Spenders != null) Spenders.OnFloatsChanged -= HandleSpendersChanged;
    }
}