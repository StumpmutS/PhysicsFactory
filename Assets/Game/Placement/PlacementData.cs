using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlacementData
{
    [SerializeField] private ContextData context;
    public ContextData Context => context;
    [SerializeField] private List<PlacementProcessor> placementProcessors;
    public List<PlacementProcessor> PlacementProcessors => placementProcessors;
    [SerializeField] private List<Restriction<PlacementRestrictionInfo>> placementRestrictions;
    public List<Restriction<PlacementRestrictionInfo>> PlacementRestrictions => placementRestrictions;
    [SerializeField] private PlaceablePreview previewPrefab;
    public PlaceablePreview PreviewPrefab => previewPrefab;
    [SerializeField] private int anchorCellAmount;
    public int AnchorCellAmount => anchorCellAmount;
    [SerializeField] private float price;
    public float Price => price;
    [SerializeField, Range(0, 1)] private float saleMultiplier = 1;
    public float SaleMultiplier => saleMultiplier;
}