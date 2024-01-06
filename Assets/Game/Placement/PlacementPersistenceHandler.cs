using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlacementPersistenceHandler : MonoBehaviour, ISaveable<LevelData>, ILoadable<LevelData>
{
    public List<AssetRefContainer<PlacementSO>> PlacementContainers { get; private set; } = new();

    [FormerlySerializedAs("OnPlaceablesLoaded")] [FormerlySerializedAs("OnBuildingsLoaded")] public UnityEvent OnPlacementSosLoaded = new();

    public void SetContainers(IEnumerable<AssetRefContainer<PlacementSO>> containers)
    {
        PlacementContainers = containers.ToList();
    }
    
    public void Save(LevelData data, AssetRefCollection assetRefCollection)
    {
        data.PlacementSaveData.PlacementAssetRefIds =
            PlacementContainers.Select(c => assetRefCollection.Add(c.Reference)).ToList();
    }

    public LoadingInfo Load(LevelData data, AssetRefCollection assetRefCollection)
    {
        PlacementContainers.Clear();
        
        foreach (var id in data.PlacementSaveData.PlacementAssetRefIds)
        {
            var placementContainer = assetRefCollection.GetContainerized<PlacementSO>(id);
            PlacementContainers.Add(placementContainer);
        }
        
        OnPlacementSosLoaded.Invoke();

        return LoadingInfo.Completed(PlacementContainers, ELoadCompletionStatus.Succeeded);
    }
}