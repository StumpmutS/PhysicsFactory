using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UpgradeInfo
{
    [SerializeField] private List<Restriction<PlaceableRestrictionData>> upgradeRestrictions;
    public List<Restriction<PlaceableRestrictionData>> UpgradeRestrictions => upgradeRestrictions;
    [SerializeField] private List<Restriction<PlaceableRestrictionData>> downgradeRestrictions;
    public List<Restriction<PlaceableRestrictionData>> DowngradeRestrictions => downgradeRestrictions;
    [SerializeField] private float price;
    public float Price => price;
    [SerializeField] private float saleMultiplier;
    public float SaleMultiplier => saleMultiplier;
}