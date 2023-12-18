using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility.Scripts;

public class BuildingInfoTransmitter : MonoBehaviour, ILoadable<PlacedBuildingSaveData> //or maybe pbi save data
{
    [SerializeField, Tooltip("This prefab")] private AssetReference prefabReference;
    [SerializeField] private BuildingPlacementInfo optionalStartInfo;
    [SerializeField] private float optionalStartVolume;
    [SerializeField] private Building building;

    private LoadingInfo _loadingInfo;
    
    private void Awake()
    {
        if (optionalStartInfo.Label != string.Empty)
            Init(new PlacedBuildingData(optionalStartInfo, optionalStartVolume, new TransformData(transform)));
    }

    public void Init(PlacedBuildingData data)
    {
        transform.position = data.TransformData.WorldPosition;
        transform.rotation = data.TransformData.WorldRotation;
        building.Init(data);
    }

    public LoadingInfo Load(PlacedBuildingSaveData data)
    {
        var loadingInfo = data.LoadGameReadyData();
        
        _loadingInfo = new LoadingInfo(() => loadingInfo.Percentage);
        loadingInfo.OnComplete += HandleLoadComplete;
        
        return _loadingInfo;
    }

    private void HandleLoadComplete(LoadingInfo info)
    {
        if (info.Result is not PlacedBuildingData data)
        {
            Debug.LogError($"Error converting save data, result was: {info.Result}");
        }
        else Init(data);
        
        _loadingInfo.Result = info.Result;
        _loadingInfo.Status = info.Status;
        _loadingInfo.Exception = info.Exception;
        _loadingInfo.Complete();
    }
}