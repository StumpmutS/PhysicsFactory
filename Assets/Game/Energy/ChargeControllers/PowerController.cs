using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class PowerController : MonoBehaviour, IEnergySpender
{
    [SerializeField] private EnergySpenderInfo spenderInfo;
    public EnergySpenderInfo SpenderInfo => spenderInfo;

    [FormerlySerializedAs("OnChargeChanged")] public UnityEvent<float> OnPowerChanged;

    public void SetEnergyLevel(float amount)
    {
        OnPowerChanged.Invoke(CalculatePower(amount));
    }

    protected abstract float CalculatePower(float charge);
}