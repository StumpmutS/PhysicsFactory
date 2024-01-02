using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Upgradeable : MonoBehaviour, IRefreshable, ISaveable<BuildingSaveData>, ILoadable<UpgradeSaveData>
{
    [SerializeField] private List<UpgradeData> upgrades;
    [SerializeField] private Building building;

    public int Level { get; private set; }
    public int MaxLevel => upgrades.Count;
    public float UpgradePrice => Level >= upgrades.Count
        ? -1
        : SupplyCalculator.CalculatePrice(upgrades[Level].Info.Price, building);
    public float DowngradePrice => Level <= 0
        ? -1
        : SupplyCalculator.CalculatePrice(upgrades[Level - 1].Info.Price, building, upgrades[Level - 1].Info.SaleMultiplier);

    public UnityEvent OnUpgrade = new();
    public UnityEvent OnDowngrade = new();
    public event Action OnRefresh;
    
    public bool TryUpgrade(RestrictionFailureInfo failureInfo)
    {
        if (Level >= upgrades.Count) return false;
        var upgrade = upgrades[Level].Info;
        if (!RestrictionHelper.TryPassRestrictions(upgrade.UpgradeRestrictions, GenerateRestrictionInfo(upgrade), failureInfo)) return false;

        Level++;
        OnUpgrade.Invoke();
        OnRefresh?.Invoke();
        return true;
    }
    
    public bool TryDowngrade(RestrictionFailureInfo failureInfo)
    {
        if (Level <= 0) return false;
        var upgrade = upgrades[Level - 1].Info;
        if (!RestrictionHelper.TryPassRestrictions(upgrade.DowngradeRestrictions, GenerateRestrictionInfo(upgrade), failureInfo)) return false;

        Level--;
        OnDowngrade.Invoke();
        OnRefresh?.Invoke();
        return true;
    }

    private BuildingRestrictionInfo GenerateRestrictionInfo(UpgradeInfo upgrade)
    {
        return new BuildingRestrictionInfo(building, upgrade.Price, upgrade.SaleMultiplier);
    }

    public void Save(BuildingSaveData data, AssetRefCollection _)
    {
        data.UpgradeSaveData = new UpgradeSaveData(Level);
    }

    public LoadingInfo Load(UpgradeSaveData data, AssetRefCollection _)
    {
        Level = data.Level;

        return LoadingInfo.Completed(data, ELoadCompletionStatus.Succeeded);
    }
}