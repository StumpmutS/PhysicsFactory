using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Building")]
public class BuildingPlacementSO : ScriptableObject
{
    [FormerlySerializedAs("buildingPlacementInfo")] [SerializeField] private BuildingPlacementData buildingPlacementData;
    public BuildingPlacementData Data => buildingPlacementData;
}