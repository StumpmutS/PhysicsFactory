using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class ChargeController : MonoBehaviour, IChargeable
{
    [SerializeField] private DataService<ContextData> contextService;

    private ChargePacket _chargePacket;
    public ChargePacket ChargePacket
    {
        get => _chargePacket;
        set
        {
            if (_chargePacket != null) _chargePacket.OnChargeUpdated -= HandleChargeUpdated;
            _chargePacket = value;
            _chargePacket.OnChargeUpdated += HandleChargeUpdated;
        }
    }

    public ContextData Context => contextService.RequestData();

    [FormerlySerializedAs("OnPowerChanged")] public UnityEvent<float> OnChargeChanged = new();

    private void HandleChargeUpdated(ChargePacket packet)
    {
        OnChargeChanged.Invoke(CalculateCharge(packet.CurrentCharge.AsFloat()));
    }

    protected abstract float CalculateCharge(float charge);

    private void OnDestroy()
    {
        if (_chargePacket != null) _chargePacket.OnChargeUpdated -= HandleChargeUpdated;
    }
}