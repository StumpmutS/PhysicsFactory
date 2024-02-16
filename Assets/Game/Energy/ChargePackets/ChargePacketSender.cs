using UnityEngine;
using Utility.Scripts;

public class ChargePacketSender : MonoBehaviour
{
    protected FloatGroup<ChargePacket> _packets = new();

    public float CurrentCharge => _packets.CurrentTotal;
    public float MaxCharge => _packets.MaxTotal;
    public float AvailableCharge => MaxCharge - CurrentCharge;

    public void SetMaxCharge(float value)
    {
        _packets.MaxTotal = value;
    }

    public ChargePacket RequestChargePacket()
    {
        var packet = new ChargePacket();
        packet.OnChargeRequested += HandlePacketChargeRequest;
        packet.OnPacketReleased += HandlePacketRelease;
        _packets.SetValue(packet, new SignedFloat(0f, true));
        return packet;
    }

    protected virtual void HandlePacketChargeRequest(ChargePacket packet, SignedFloat value)
    {
        _packets.SetValue(packet, value);
    }
    
    private void HandlePacketRelease(ChargePacket packet)
    {
        packet.OnChargeRequested -= HandlePacketChargeRequest;
        packet.OnPacketReleased -= HandlePacketRelease;
        _packets.Remove(packet);
    }
}
