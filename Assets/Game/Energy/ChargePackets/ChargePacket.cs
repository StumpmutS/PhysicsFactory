using System;
using Utility.Scripts;

public class ChargePacket : IFloatGroupKey
{
    public float AvailableCharge => EntryData.Available;
    public SignedFloat CurrentCharge => EntryData.FloatGroupValue;
    
    private FloatGroupEntryData _currentData;
    public FloatGroupEntryData EntryData
    {
        get => _currentData;
        set
        {
            if (value.Equals(_currentData)) return;

            _currentData = value;
            OnChargeUpdated.Invoke(this);
        }
    }
    
    public event Action<ChargePacket> OnChargeUpdated = delegate { };
    public event Action<ChargePacket, SignedFloat> OnChargeRequested = delegate { };
    public event Action<ChargePacket> OnPacketReleased = delegate { };
    
    public void UpdateRequestedCharge(SignedFloat value)
    {
        OnChargeRequested.Invoke(this, value);
    }

    public void ReleasePacket()
    {
        OnPacketReleased.Invoke(this);
    }
}
