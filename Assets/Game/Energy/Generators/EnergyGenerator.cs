using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class EnergyGenerator : MonoBehaviour
{
    [SerializeField] private float startingChargeGenerated;
    public float ChargeGenerated => _chargeGenerated;

    private float _chargeGenerated;
    private bool _loaded;

    public UnityEvent<float> OnGeneratedChargeChanged = new();

    public void Load(float chargeGenerated)
    {
        _chargeGenerated = chargeGenerated;
        OnGeneratedChargeChanged.Invoke(ChargeGenerated);
        _loaded = true;
    }
    
    private void Start()
    {
        if (_loaded) return;
        
        Load(startingChargeGenerated);
    }
}
