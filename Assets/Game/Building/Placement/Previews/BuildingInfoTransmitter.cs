using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility.Scripts;

public class BuildingInfoTransmitter : MonoBehaviour
{
    [SerializeField, Tooltip("This prefab")] private AssetReference prefabReference;
    [SerializeField] private BuildingPlacementInfo optionalStartInfo;
    [SerializeField] private float optionalStartVolume;
    [SerializeField] private Building building;

    private void Awake()
    {
        if (optionalStartInfo.Label != string.Empty)
            Init(new PlacedBuildingInfo(optionalStartInfo, optionalStartVolume, new TransformData(transform)));
    }

    public void Init(PlacedBuildingInfo info)
    {
        transform.position = info.TransformData.WorldPosition;
        transform.rotation = info.TransformData.WorldRotation;
        building.Init(info);
    }
}