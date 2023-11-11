using System;
using UnityEngine;
using Utility.Scripts;

public class SupplyManager : Singleton<SupplyManager>
{
    [SerializeField] private float startingSupply;

    public float CurrentSupplyCount
    {
        get => _currentSupplyCount;
        private set
        {
            _currentSupplyCount = Mathf.Clamp(value, 0, float.MaxValue);
            OnSupplyChanged.Invoke(_currentSupplyCount);
        }
    }

    private float _currentSupplyCount;
    
    public event Action<float> OnSupplyChanged = delegate {  };

    protected override void Awake()
    {
        base.Awake();
        CurrentSupplyCount = startingSupply;
    }

    public void DepositDolboid(Dolboid dolboid, DepositEffects effects)
    {
        CurrentSupplyCount += effects.Multiplier * SupplyCalculator.CalculatePrice(dolboid);
    }

    public void SpendSupply(float amount)
    {
        CurrentSupplyCount -= amount;
    }

    public void AddSupply(float amount)
    {
        CurrentSupplyCount += amount;
    }
}