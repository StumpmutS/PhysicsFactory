using UnityEngine;

public class BuildingInfoTransmitter : MonoBehaviour
{
    [SerializeField] private BuildingInfo optionalStartInfo;
    [SerializeField] private float optionalStartVolume;
    [SerializeField] private Building building;

    private void Awake()
    {
        if (optionalStartInfo.Label != string.Empty) Init(new PlacedBuildingInfo(optionalStartInfo, optionalStartVolume), transform);
    }

    public void Init(PlacedBuildingInfo info, Transform transformToCopy)
    {
        transform.position = transformToCopy.position;
        transform.rotation = transformToCopy.rotation;
        building.Init(info, transformToCopy);
    }
}