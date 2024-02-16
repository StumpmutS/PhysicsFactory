using System;
using UnityEngine;

public class ReceiverNode : EnergyNode
{
    [SerializeField] private Component packetReceiver;
    
    public IChargePacketReceiver PacketReceiver { get; private set; }
    
    private void Awake()
    {
        if (packetReceiver is not IChargePacketReceiver receiver)
        {
            Debug.LogError($"Component named {packetReceiver.name} was not of type {nameof(IChargePacketReceiver)}");
            return;
        }
        
        PacketReceiver = receiver;
    }

    public override bool TryGetConnectionPair(EnergyNode other, out ChargePacketSender sender, out IChargePacketReceiver receiver)
    {
        sender = null;
        receiver = PacketReceiver;
        if (other is not SenderNode senderNode) return false;
        
        sender = senderNode.Sender;
        
        return true;
    }

    public override bool CanConnect(EnergyNode other, out ChargePacketSender sender, out IChargePacketReceiver receiver)
    {
        sender = null;
        receiver = null;
        return other is SenderNode senderNode && senderNode.CanConnect(this, out sender, out receiver);
    }
}