using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class EnergySpreadController : MonoBehaviour, ISaveable<SaveableObjectSaveData>, ILoadable<EnergySpreadSaveData>
{
    [SerializeField] private ChargePacketDistributor packetDistributor;
    [SerializeField] private List<Component> startingSpenders;

    public float CurrentTotal => Spenders
        .Where(c => c.ChargePacket != null)
        .Sum(c => c.ChargePacket.CurrentCharge.Value);
    public float MaxTotal => CurrentTotal + packetDistributor.AvailableCharge;
    public HashSet<IChargeable> Spenders { get; private set; } = new();

    private bool _spendersInitialized;

    public UnityEvent OnSpendersChanged = new();

    private void Start()
    {
        TryInitSpenders();
    }

    private void TryInitSpenders()
    {
        if (_spendersInitialized) return;
        
        foreach (var component in startingSpenders)
        {
            if (component is IChargeable spender)
            {
                RegisterSpender(spender);
            }
        }
        
        _spendersInitialized = true;
    }

    public void RegisterSpender(IChargeable spender)
    {
        if (Spenders.Contains(spender)) return; // Using contains instead of add is intentional
        
        var packet = packetDistributor.RequestChargePacket();
        spender.ChargePacket = packet;
        spender.ChargePacket.OnChargeUpdated += HandlePacketUpdated;

        Spenders.Add(spender);
        
        OnSpendersChanged.Invoke();
    }

    private void HandlePacketUpdated(ChargePacket packet)
    {
        OnSpendersChanged.Invoke();
    }

    public void DeregisterSpender(IChargeable spender)
    {
        Spenders.Remove(spender);
        spender.ChargePacket.ReleasePacket();
        spender.ChargePacket.OnChargeUpdated -= HandlePacketUpdated;
        OnSpendersChanged.Invoke();
    }

    private void OnDestroy()
    {
        foreach (var spender in Spenders)
        {
            spender.ChargePacket.ReleasePacket();
        }
    }

    public void Save(SaveableObjectSaveData data, AssetRefCollection assetRefCollection)
    {
        data.EnergySpreadSaveData ??= new EnergySpreadSaveData();
        data.EnergySpreadSaveData.Spread ??= new SerializableDictionary<string, SerializableSignedFloat>();
        foreach (var spender in Spenders)
        {
            data.EnergySpreadSaveData.Spread[spender.Context.Label] = new SerializableSignedFloat(spender.ChargePacket.CurrentCharge);
        }
        data.EnergySpreadSaveData.Spread.OnBeforeSerialize();
    }

    public LoadingInfo Load(EnergySpreadSaveData data, AssetRefCollection assetRefCollection)
    {
        TryInitSpenders();
        
        foreach (var spender in Spenders)
        {
            var label = spender.Context.Label;
            if (!data.Spread.TryGetValue(label, out var value))
            {
                var ex = $"Could not find spender with label: {label}";
                return LoadingInfo.Completed(data, ELoadCompletionStatus.Failed, new Exception(ex));
            }
            
            spender.ChargePacket.UpdateRequestedCharge(value.ToSignedFloat());
        }

        return LoadingInfo.Completed(data, ELoadCompletionStatus.Succeeded);
    }
}
