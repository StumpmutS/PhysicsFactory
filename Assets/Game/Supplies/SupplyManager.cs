using System;
using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class SupplyManager : Singleton<SupplyManager>, ISaveable<LevelData>, ILoadable<LevelData>
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
    
    public UnityEvent<float> OnSupplyChanged = new();

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

    public void Save(LevelData data)
    {
        data.supplyData.Supply = _currentSupplyCount;
    }

    public LoadingInfo Load(LevelData data)
    {
        CurrentSupplyCount = data.supplyData.Supply;
        var loadingInfo = new LoadingInfo(() => 100)
        {
            Result = this,
            Status = ELoadCompletionStatus.Succeeded
        };
        return loadingInfo;
    }
}