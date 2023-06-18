using UnityEngine;

[CreateAssetMenu(menuName = "Building")]
public class BuildingData : ScriptableObject
{
    [SerializeField] private BuildingInfo buildingInfo;
    public BuildingInfo Info => buildingInfo;
}