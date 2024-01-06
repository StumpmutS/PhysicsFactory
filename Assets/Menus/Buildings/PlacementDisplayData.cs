using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PlacementDisplayData
{
    [FormerlySerializedAs("buildingContainer")] [FormerlySerializedAs("buildingReference")] [SerializeField] private AssetRefContainer<PlacementSO> placementContainer;
    public AssetRefContainer<PlacementSO> PlacementContainer => placementContainer;
}