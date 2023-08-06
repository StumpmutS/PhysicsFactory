using System.Collections.Generic;
using UnityEngine;

public class PlacedBuildingInfo
{
    public string Label { get; private set; }
    public float Volume { get; private set; }
    public float Price { get; private set; }
    public List<Restriction<BuildingRestrictionInfo>> SaleRestrictions { get; private set; }
    public float SaleMultiplier { get; private set; }

    public PlacedBuildingInfo(string label, float volume, float price, List<Restriction<BuildingRestrictionInfo>> saleRestrictions, float saleMultiplier)
    {
        Label = label;
        Volume = volume;
        Price = price;
        SaleRestrictions = saleRestrictions;
        SaleMultiplier = saleMultiplier;
    }
}