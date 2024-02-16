public interface IChargeable
{
    public ChargePacket ChargePacket { get; set; }
    public ContextData Context { get; }
}
