using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlacementPersistenceHandler : MonoBehaviour, ISaveable<LevelData>, ILoadable<LevelData>
{
    public List<AssetRefContainer<BuildingPlacementSO>> BuildingContainers { get; private set; } = new();

    public UnityEvent OnBuildingsLoaded = new();

    public void SetContainers(IEnumerable<AssetRefContainer<BuildingPlacementSO>> containers)
    {
        BuildingContainers = containers.ToList();
    }
    
    public void Save(LevelData data, AssetRefCollection assetRefCollection)
    {
        data.PlacementSaveData.BuildingAssetRefIds =
            BuildingContainers.Select(c => assetRefCollection.Add(c.Reference)).ToList();
    }

    public LoadingInfo Load(LevelData data, AssetRefCollection assetRefCollection)
    {
        BuildingContainers.Clear();
        
        foreach (var id in data.PlacementSaveData.BuildingAssetRefIds)
        {
            var building = assetRefCollection.GetContainerized<BuildingPlacementSO>(id);
            BuildingContainers.Add(building);
        }
        
        OnBuildingsLoaded.Invoke();

        return LoadingInfo.Completed(BuildingContainers, ELoadCompletionStatus.Succeeded);
    }
}