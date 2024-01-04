using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingPlacementData
{
    [SerializeField] private string label;
    public string Label => label;
    [SerializeField] private List<PlacementProcessor> placementProcessors;
    public List<PlacementProcessor> PlacementProcessors => placementProcessors;
    [SerializeField] private List<Restriction<PlacementRestrictionInfo>> placementRestrictions;
    public List<Restriction<PlacementRestrictionInfo>> PlacementRestrictions => placementRestrictions;
    [SerializeField] private List<AssetRefContainer<Restriction<BuildingRestrictionInfo>>> saleRestrictionRefs;
    public List<AssetRefContainer<Restriction<BuildingRestrictionInfo>>> SaleRestrictionRefs => saleRestrictionRefs;
    [SerializeField] private BuildingPreview previewPrefab;
    public BuildingPreview PreviewPrefab => previewPrefab;
    [SerializeField] private int anchorCellAmount;
    public int AnchorCellAmount => anchorCellAmount;
    [SerializeField] private float price;
    public float Price => price;
    [SerializeField, Range(0, 1)] private float saleMultiplier = 1;
    public float SaleMultiplier => saleMultiplier;
}