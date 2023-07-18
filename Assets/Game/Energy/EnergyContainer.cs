using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnergyContainer : MonoBehaviour
{
    [SerializeField] private List<EnergyConverter> converters;

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
    
    public HashSet<EnergyCurrent> Currents { get; private set; } = new();

    public UnityEvent<float> OnChargeChanged;

    public void AddCurrent(EnergyCurrent current)
    {
        Currents.Add(current);
        current.OnChargeChanged += UpdateTotalCharge;
        UpdateTotalCharge();
    }

    private void UpdateTotalCharge()
    {
        var rawCharge = Currents.Sum(c => c.Charge);
        foreach (var converter in converters)
        {
            rawCharge = converter.ConvertEnergy(rawCharge);
        }
        TotalCharge = rawCharge;
    }
}