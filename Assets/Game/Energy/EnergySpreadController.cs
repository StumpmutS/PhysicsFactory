using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Utility.Scripts;

public class EnergySpreadController : MonoBehaviour, ISaveable<BuildingSaveData>, ILoadable<EnergySpreadSaveData>
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

    public void Save(BuildingSaveData data, AssetRefCollection assetRefCollection)
    {
        data.EnergySpreadSaveData ??= new EnergySpreadSaveData();
        data.EnergySpreadSaveData.MaxTotal = Spenders.MaxTotal;
        data.EnergySpreadSaveData.Spread ??= new SerializableDictionary<string, SerializableSignedFloat>();
        foreach (var kvp in Spenders.Floats)
        {
            data.EnergySpreadSaveData.Spread[kvp.Key.SpenderInfo.Label] = new SerializableSignedFloat(kvp.Value);
        }
        data.EnergySpreadSaveData.Spread.OnBeforeSerialize();
    }

    public LoadingInfo Load(EnergySpreadSaveData data, AssetRefCollection assetRefCollection)
    {
        Spenders.MaxTotal = data.MaxTotal;
        var floatsCopy = Spenders.Floats.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        foreach (var kvp in floatsCopy)
        {
            var label = kvp.Key.SpenderInfo.Label;
            if (data.Spread.TryGetValue(label, out var value))
            {
                Spenders.SetValue(kvp.Key, value.ToSignedFloat());
            }
        }

        return LoadingInfo.Completed(data, ELoadCompletionStatus.Succeeded);
    }
}