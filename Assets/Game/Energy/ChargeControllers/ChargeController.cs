using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class ChargeController : MonoBehaviour, IEnergySpender
{
    [SerializeField] private DataService<ContextData> contextService;
    public ContextData Context => contextService.RequestData();

    [FormerlySerializedAs("OnPowerChanged")] public UnityEvent<float> OnChargeChanged = new();

    public void SetEnergyLevel(float amount)
    {
        OnChargeChanged.Invoke(CalculateCharge(amount));
    }

    protected abstract float CalculateCharge(float charge);
}