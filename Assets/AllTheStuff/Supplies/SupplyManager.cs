using System;
using UnityEngine;
using Utility.Scripts;

public class SupplyManager : Singleton<SupplyManager>
{
    [SerializeField] private int startingSupply;

    public int CurrentSupplyCount
    {
        get => _currentSupplyCount;
        private set
        {
            _currentSupplyCount = value;
            OnSupplyChanged.Invoke(value);
        }
    }

    private int _currentSupplyCount;
    
    public event Action<int> OnSupplyChanged = delegate {  };

    protected override void Awake()
    {
        base.Awake();
        CurrentSupplyCount = startingSupply;
    }

    public void DepositDolboid(Dolboid dolboid)
    {
        CurrentSupplyCount += SupplyCalculator.CalculatePrice(dolboid);
    }

    public void SpendSupply(int amount)
    {
        CurrentSupplyCount -= amount;
    }
}