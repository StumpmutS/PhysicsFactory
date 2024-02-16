using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public abstract class EnergyNode : MonoBehaviour, ISaveable<SaveableObjectSaveData>, ILoadable<NodeSaveData>
{
    public HashSet<ChargePacketConnection> PacketConnections { get; private set; } = new();

    public UnityEvent OnConnectionsUpdated = new();
    
    public bool TryConnect(EnergyNode other)
    {
        if (!CanConnect(other, out var sender, out var receiver)) return false;
        
        InitiateConnection(other, sender, receiver);
        return true;
    }

    public abstract bool TryGetConnectionPair(EnergyNode other, out ChargePacketSender sender, out IChargePacketReceiver receiver);
    
    public abstract bool CanConnect(EnergyNode other, out ChargePacketSender sender, out IChargePacketReceiver receiver);

    public bool TryDisconnect(EnergyNode other)
    {
        if (!TryGetConnectionPair(other, out var sender, out var receiver)) return false;

        var connection = PacketConnections.FirstOrDefault(c => c.Sender == sender && c.Receiver == receiver);
        if (connection == null) return false;
        
        connection.Packet.ReleasePacket();
        return true;
    }
    
    private void InitiateConnection(EnergyNode other, ChargePacketSender sender, IChargePacketReceiver receiver)
    {
        if (PacketConnections.Any(c => c.Sender == sender && c.Receiver == receiver)) return;

        var packet = sender.RequestChargePacket();
        var connection = new ChargePacketConnection(sender, receiver, packet);
        EstablishConnection(connection);
        other.EstablishConnection(connection);
        receiver.ReceivePacket(packet);
    }

    private void HandlePacketUpdate(ChargePacket packet) => OnConnectionsUpdated.Invoke();

    private void HandlePacketRelease(ChargePacket packet)
    {
        var connection = PacketConnections.FirstOrDefault(c => c.Packet == packet);
        if (connection == null) return;
        
        DisestablishConnection(connection);
    }

    private void EstablishConnection(ChargePacketConnection connection)
    {
        connection.Packet.OnChargeUpdated += HandlePacketUpdate;
        connection.Packet.OnPacketReleased += HandlePacketRelease;
        PacketConnections.Add(connection);
        OnConnectionsUpdated.Invoke();
    }

    private void DisestablishConnection(ChargePacketConnection connection)
    {
        connection.Packet.OnChargeUpdated -= HandlePacketUpdate;
        connection.Packet.OnPacketReleased -= HandlePacketRelease;
        PacketConnections.Remove(connection);
        OnConnectionsUpdated.Invoke();
    }

    public void Save(SaveableObjectSaveData data, AssetRefCollection assetRefCollection)
    {
        data.NodeSaveData ??= new NodeSaveData();
        data.NodeSaveData.ConnectionSaveData ??= new List<ConnectionSaveData>();
        foreach (var connection in PacketConnections)
        {
            if (!connection.Sender.TryGetComponent<SaveableObject>(out var fromObj) ||
                connection.Receiver is not Component receiverComponent ||
                !receiverComponent.TryGetComponent<SaveableObject>(out var toObj)) continue;
            
            data.NodeSaveData.ConnectionSaveData.Add(new ConnectionSaveData(fromObj.Id, toObj.Id));
        }
    }

    public LoadingInfo Load(NodeSaveData data, AssetRefCollection assetRefCollection)
    {
        foreach (var connectionSaveData in data.ConnectionSaveData)
        {
            if (!SaveableObjectIdManager.Instance.TryGet(connectionSaveData.FromId, out var fromObj) ||
                !fromObj.TryGetComponent<ChargePacketSender>(out var sender) ||
                !sender.TryGetComponent<EnergyNode>(out var senderNode) ||
                !SaveableObjectIdManager.Instance.TryGet(connectionSaveData.ToId, out var toObj) ||
                !toObj.TryGetComponent<IChargePacketReceiver>(out var receiver) ||
                !toObj.TryGetComponent<EnergyNode>(out var receiverNode)) continue;

            var other = this == senderNode ? receiverNode : senderNode;
            InitiateConnection(other, sender, receiver);
        }

        return LoadingInfo.Completed(data, ELoadCompletionStatus.Succeeded);
    }
}