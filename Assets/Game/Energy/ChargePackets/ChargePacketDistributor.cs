using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class ChargePacketDistributor : ChargePacketSender, IChargePacketReceiver
{
    [SerializeField] private List<EnergyConverter> converters;
    
    private HashSet<ChargePacket> _distributablePackets = new();

    public UnityEvent OnChargeUpdate = new();
    
    public void ReceivePacket(ChargePacket packet)
    {
        _distributablePackets.Add(packet);
        packet.OnChargeUpdated += HandleDistributableChargeUpdated;
        packet.OnPacketReleased += HandlePacketReleased;
        HandleDistributableChargeUpdated(packet);
    }

    private void HandleDistributableChargeUpdated(ChargePacket _)
    {
        UpdateMaxCharge();
        OnChargeUpdate.Invoke();
    }

    private void HandlePacketReleased(ChargePacket packet)
    {
        if (!_distributablePackets.Remove(packet)) return;
        
        UnsubscribePacket(packet);
        HandleDistributableChargeUpdated(packet);
    }

    protected override void HandlePacketChargeRequest(ChargePacket packet, SignedFloat value)
    {
        var rawDiff = EnergyConversionHelpers.UnconvertEnergy(converters, value.Value - packet.CurrentCharge.Value);
        foreach (var distributablePacket in _distributablePackets)
        {
            var originalCharge = distributablePacket.CurrentCharge;
            distributablePacket.UpdateRequestedCharge(distributablePacket.CurrentCharge + SignedFloat.FromFloat(rawDiff));
            if (distributablePacket.CurrentCharge.Value >= originalCharge.Value + rawDiff) break;
        }
        base.HandlePacketChargeRequest(packet, value);
        
        OnChargeUpdate.Invoke();
    }

    private void UpdateMaxCharge()
    {
        var rawMax = _distributablePackets.Sum(p => p.CurrentCharge.Value + p.AvailableCharge);
        SetMaxCharge(EnergyConversionHelpers.ConvertEnergy(converters, rawMax));
    }

    private void OnDestroy()
    {
        foreach (var packet in _distributablePackets)
        {
            UnsubscribePacket(packet);
        }
    }

    private void UnsubscribePacket(ChargePacket packet)
    {
        if (packet != null) packet.OnChargeUpdated -= HandleDistributableChargeUpdated;
        if (packet != null) packet.OnPacketReleased -= HandlePacketReleased;
    }
}