using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlacedBuildingInfo
{
    [SerializeField] private string label;
    public string Label => label;
    [SerializeField] private float volume;
    public float Volume => volume;
    [SerializeField] private float price;
    public float Price => price;
    [SerializeField] private List<Restriction<BuildingRestrictionInfo>> saleRestrictions;
    public List<Restriction<BuildingRestrictionInfo>> SaleRestrictions => saleRestrictions;
    [SerializeField] private float saleMultiplier;
    public float SaleMultiplier => saleMultiplier;

    public PlacedBuildingInfo(string label, float volume, float price, List<Restriction<BuildingRestrictionInfo>> saleRestrictions, float saleMultiplier)
    {
        this.label = label;
        this.volume = volume;
        this.price = price;
        this.saleRestrictions = saleRestrictions;
        this.saleMultiplier = saleMultiplier;
    }

    public PlacedBuildingInfo(BuildingInfo info, float volume) : this(info.Label, volume, info.Price,
        info.SaleRestrictions, info.SaleMultiplier) { }
}
