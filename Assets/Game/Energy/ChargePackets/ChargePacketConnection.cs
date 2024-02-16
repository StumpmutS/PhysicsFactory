using System;

public class ChargePacketConnection
{
    public ChargePacketSender Sender;
    public IChargePacketReceiver Receiver;
    public ChargePacket Packet;

    public ChargePacketConnection(ChargePacketSender sender, IChargePacketReceiver receiver, ChargePacket packet)
    {
        Sender = sender;
        Receiver = receiver;
        Packet = packet;
    }
}