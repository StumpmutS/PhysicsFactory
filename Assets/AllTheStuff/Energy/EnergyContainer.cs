using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class EnergyContainer : MonoBehaviour
{
    private int _totalCharge;
    public int TotalCharge
    {
        get => _totalCharge;
        private set
        {
            if (value == _totalCharge) return;

            _totalCharge = value;
            OnChargeChanged.Invoke(_totalCharge);
        }
    }
    
    private HashSet<EnergyCurrent> _currents = new();

    public UnityEvent<int> OnChargeChanged;

    public void AddCurrent(EnergyCurrent current)
    {
        _currents.Add(current);
        current.OnChargeChanged += UpdateTotalCharge;
        UpdateTotalCharge();
    }

    private void UpdateTotalCharge()
    {
        TotalCharge = _currents.Sum(c => c.Charge);
    }
}
