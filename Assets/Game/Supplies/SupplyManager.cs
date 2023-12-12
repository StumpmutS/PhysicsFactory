﻿using System;
using UnityEngine;
using UnityEngine.Events;
using Utility.Scripts;

public class SupplyManager : Singleton<SupplyManager>, ISaveable, ILoadable
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
    public event Action<ILoadable> OnLoadComplete = delegate {  };

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

    public void Save(SaveData data)
    {
        data.SupplyInfo.Supply = _currentSupplyCount;
    }

    public void Load(SaveData data)
    {
        _currentSupplyCount = data.SupplyInfo.Supply;
        OnLoadComplete.Invoke(this);
    }
}