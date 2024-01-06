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

    public void SetSupply(float value) => CurrentSupplyCount = value;

    public void DepositResource(ResourceData data)
    {
        SetSupply(CurrentSupplyCount + SupplyCalculator.CalculatePrice(data));
    }

    public void SpendSupply(float amount)
    {
        SetSupply(CurrentSupplyCount - amount);
    }

    public void AddSupply(float amount)
    {
        SetSupply(CurrentSupplyCount + amount);
    }

    public void Save(LevelData data, AssetRefCollection _)
    {
        data.SupplyData.Supply = _currentSupplyCount;
    }

    public LoadingInfo Load(LevelData data, AssetRefCollection _)
    {
        SetSupply(data.SupplyData.Supply);

        return LoadingInfo.Completed(data.SupplyData, ELoadCompletionStatus.Succeeded);
    }
}