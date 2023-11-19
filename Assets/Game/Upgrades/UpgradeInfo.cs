using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeInfo
{
    [SerializeField] private List<Restriction<BuildingRestrictionInfo>> upgradeRestrictions;
    public List<Restriction<BuildingRestrictionInfo>> UpgradeRestrictions => upgradeRestrictions;
    [SerializeField] private List<Restriction<BuildingRestrictionInfo>> downgradeRestrictions;
    public List<Restriction<BuildingRestrictionInfo>> DowngradeRestrictions => downgradeRestrictions;
    [SerializeField] private float price;
    public float Price => price;
    [SerializeField] private float saleMultiplier;
    public float SaleMultiplier => saleMultiplier;
}