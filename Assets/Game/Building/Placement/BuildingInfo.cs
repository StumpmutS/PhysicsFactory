﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class BuildingInfo
{
    [SerializeField] private string label;
    public string Label => label;
    [SerializeField] private List<Restriction<PlacementRestrictionInfo>> placementRestrictions;
    public List<Restriction<PlacementRestrictionInfo>> PlacementRestrictions => placementRestrictions;
    [SerializeField] private List<Restriction<BuildingRestrictionInfo>> saleRestrictions;
    public List<Restriction<BuildingRestrictionInfo>> SaleRestrictions => saleRestrictions;
    [SerializeField] private BuildingPreview previewPrefab;
    public BuildingPreview PreviewPrefab => previewPrefab;
    [SerializeField] private int anchorCellAmount;
    public int AnchorCellAmount => anchorCellAmount;
    [SerializeField] private bool restrictToAxes;
    public bool RestrictToAxes => restrictToAxes;
    [SerializeField] private float price;
    public float Price => price;
}