using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility.Scripts;

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
    [SerializeField] private TransformData transformData;
    public TransformData TransformData => transformData;
    
    public PlacedBuildingInfo(string label, float volume, float price, List<Restriction<BuildingRestrictionInfo>> saleRestrictions, float saleMultiplier, TransformData transformData)
    {
        this.label = label;
        this.volume = volume;
        this.price = price;
        this.saleRestrictions = saleRestrictions;
        this.saleMultiplier = saleMultiplier;
        this.transformData = transformData;
    }

    public PlacedBuildingInfo(BuildingPlacementInfo info, float volume, TransformData transformData) : this(info.Label, volume, info.Price,
        info.SaleRestrictions, info.SaleMultiplier, transformData) { }
}
