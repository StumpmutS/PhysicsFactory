using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Placement/Data")]
public class PlacementSO : ScriptableObject
{
    [FormerlySerializedAs("buildingPlacementData")] [FormerlySerializedAs("buildingPlacementInfo")] [SerializeField] private PlacementData placementData;
    public PlacementData Data => placementData;
}