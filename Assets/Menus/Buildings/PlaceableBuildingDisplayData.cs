using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PlaceableBuildingDisplayData
{
    [FormerlySerializedAs("buildingReference")] [SerializeField] private AssetRefContainer<BuildingPlacementSO> buildingContainer;

    public AssetRefContainer<BuildingPlacementSO> BuildingContainer => buildingContainer;
}