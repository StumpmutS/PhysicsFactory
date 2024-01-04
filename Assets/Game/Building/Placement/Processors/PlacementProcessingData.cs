using System;
using System.Collections.Generic;
using UnityEngine;

public class PlacementProcessingData
{
    public Grid3D Grid;
    public Vector3 Position;
    public Stack<Vector3> SelectedPositions = new();
    public BuildingPreview Preview { get; }
    public List<Restriction<PlacementRestrictionInfo>> PlacementRestrictions;
    public Func<PlacementRestrictionInfo> InfoGetter;

    public PlacementProcessingData(Grid3D grid, BuildingPreview preview, List<Restriction<PlacementRestrictionInfo>> restrictions, Func<PlacementRestrictionInfo> restrictionInfoGetter)
    {
        Grid = grid;
        Preview = preview;
        PlacementRestrictions = restrictions;
        InfoGetter = restrictionInfoGetter;
    }
}