using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BuildingInfo
{
    [SerializeField] private string label;
    public string Label => label;
    [SerializeField] private List<Restriction> restrictions;
    public List<Restriction> Restrictions => restrictions;
    [SerializeField] private GameObject buildingPrefab;
    public GameObject BuildingPrefab => buildingPrefab;
    [SerializeField] private BuildingPreview previewPrefab;
    public BuildingPreview PreviewPrefab => previewPrefab;
    [SerializeField] private EBuildStyle buildStyle;
    public EBuildStyle BuildStyle => buildStyle;
    [SerializeField] private Vector3 dimensions;
    public Vector3 Dimensions => dimensions;
}