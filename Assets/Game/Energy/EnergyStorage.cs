using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnergyStorage : MonoBehaviour
{
    [SerializeField] private CurrentContainer currentContainer;
    [SerializeField] private List<EnergyConverter> converters;

    public float RawCharge => currentContainer.Currents.Sum(c => c.Charge);

    private float _totalCharge;
    public float TotalCharge
    {
        get => _totalCharge;
        private set
        {
            if (value == _totalCharge) return;

            _totalCharge = value;
            OnChargeChanged.Invoke(_totalCharge);
        }
    }

    public UnityEvent<float> OnChargeChanged;

    private void Awake()
    {
        currentContainer.OnCurrentsChanged.AddListener(UpdateTotalCharge);
    }

    private void UpdateTotalCharge()
    {
        TotalCharge = converters.Aggregate(RawCharge, (current, converter) => converter.ConvertEnergy(current));
    }

    private void OnDestroy()
    {
        if (currentContainer != null) currentContainer.OnCurrentsChanged.RemoveListener(UpdateTotalCharge);
    }
}