using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Serialization;
using Utility.Scripts;

public class PlaceableTransmitter : MonoBehaviour, ISaveable<LevelData>, ILoadable<PlacedSaveData>
{
    [FormerlySerializedAs("prefabReference")] [SerializeField] private AssetReference sessionPrefabReference;
    [FormerlySerializedAs("optionalStartInfo")] [SerializeField] private PlacementData optionalStartData;
    [SerializeField] private float optionalStartVolume;
    [FormerlySerializedAs("building")] [SerializeField] private Placeable placeable;

    private LoadingInfo _loadingInfo;
    
    private void Awake()
    {
        if (optionalStartData.Label != string.Empty)
            Init(new PlacedData(optionalStartData, optionalStartVolume, new TransformData(transform)));
    }

    public void Init(PlacedData data)
    {
        transform.position = data.TransformData.WorldPosition;
        transform.rotation = data.TransformData.WorldRotation;
        placeable.Init(data);
    }

    public void Save(LevelData data, AssetRefCollection assetRefCollection)
    {
        var placeableSaveData = new PlaceableSaveData();
        SaveData(placeableSaveData, assetRefCollection);
        SaveHelpers.GroupSave(GetComponentsInChildren<ISaveable<PlaceableSaveData>>(), placeableSaveData, assetRefCollection);
        
        data.PlaceableSaveData ??= new List<PlaceableSaveData>();
        data.PlaceableSaveData.Add(placeableSaveData);
    }

    protected virtual void SaveData(PlaceableSaveData placeableSaveData, AssetRefCollection assetRefCollection)
    {
        placeableSaveData.SessionPlaceablePrefabReferenceId = assetRefCollection.Add(sessionPrefabReference);
    }

    public LoadingInfo Load(PlacedSaveData data, AssetRefCollection _)
    {
        Init(data.Data);
        
        return LoadingInfo.Completed(data.Data, ELoadCompletionStatus.Succeeded);
    }
}