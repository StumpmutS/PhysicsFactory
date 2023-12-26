using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Utility.Scripts;

public class BuildingInfoTransmitter : MonoBehaviour, ISaveable<LevelData>, ILoadable<PlacedBuildingSaveData>
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

    public void Save(LevelData data, AssetRefCollection assetRefCollection)
    {
        var buildingSaveData = new BuildingSaveData
        {
            BuildingPrefabReferenceIndex = assetRefCollection.Add(prefabReference)
        };
        SaveHelpers.GroupSave(GetComponentsInChildren<ISaveable<BuildingSaveData>>(), buildingSaveData, assetRefCollection);
        
        data.BuildingSaveData ??= new List<BuildingSaveData>();
        data.BuildingSaveData.Add(buildingSaveData);
    }

    public LoadingInfo Load(PlacedBuildingSaveData data, AssetRefCollection _)
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
            Debug.LogError($"Problem converting save data, result was: {info.Result}");
        }
        else Init(data);
        
        _loadingInfo.Result = info.Result;
        _loadingInfo.Status = info.Status;
        _loadingInfo.Exception = info.Exception;
        _loadingInfo.Complete();
    }
}