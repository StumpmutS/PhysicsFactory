using System.Collections.Generic;
using UnityEngine;

public class SenderNode : EnergyNode
{
    [SerializeField] private ChargePacketSender sender;
    public ChargePacketSender Sender => sender;
    [SerializeField] private EnergyNodeFinder nodeFinder;
    
    public override bool TryGetConnectionPair(EnergyNode other, out ChargePacketSender sender, out IChargePacketReceiver receiver)
    {
        sender = this.sender;
        receiver = null;
        if (other is not ReceiverNode receiverNode) return false;

        receiver = receiverNode.PacketReceiver;

        return true;
    }

    public override bool CanConnect(EnergyNode other, out ChargePacketSender sender, out IChargePacketReceiver receiver)
    {
        return TryGetConnectionPair(other, out sender, out receiver) && nodeFinder.Nodes.Contains(other);
    }
}
