using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Building")]
public class BuildingData : ScriptableObject
{
    [FormerlySerializedAs("buildingInfo")] [SerializeField] private BuildingPlacementInfo buildingPlacementInfo;
    public BuildingPlacementInfo Info => buildingPlacementInfo;
}