using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingInfo
{
    [SerializeField] private string label;
    public string Label => label;
    [SerializeField] private List<BuildingRestriction> restrictions;
    public List<BuildingRestriction> Restrictions => restrictions;
    [SerializeField] private BuildingPreview previewPrefab;
    public BuildingPreview PreviewPrefab => previewPrefab;
    [SerializeField] private Vector3 dimensions;
    public Vector3 Dimensions => dimensions;
    [SerializeField] private int anchorCellAmount;
    public int AnchorCellAmount => anchorCellAmount;
}